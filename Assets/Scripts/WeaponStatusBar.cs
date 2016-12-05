using UnityEngine;
using System.Collections;
using CallOfValhalla;
using System;
using CallOfValhalla.UI;

public class WeaponStatusBar : MonoBehaviour {

    private Sprite _statusBarNotReady;
    private Sprite _statusBarReady;
    private Sprite _statusBarNotReadySword;
    private Sprite _statusBarReadySword;
    private Sprite _currentWeaponSword;
    private Sprite _currentWeaponHammer;

    private GameObject _weaponStatusBar;
    private GameObject _currentWeaponImage;
    private WeaponStatusBarController _controller;
    private float _completionPercent;
    private float _baseCompletion = 0.15f;
    private float _endCompletion = 0.90f;
    private float _scaledCompletion;
    
    

	// Use this for initialization
	private void Awake () {
        _weaponStatusBar = GameObject.Find("WeaponStatusBar");
        _currentWeaponImage = GameObject.Find("WeaponStatusImage");    
        _statusBarReady = Resources.Load("WeaponFillBarReady", typeof(Sprite)) as Sprite;
        _statusBarNotReady = Resources.Load("WeaponFillBar", typeof(Sprite)) as Sprite;

        _statusBarReadySword = Resources.Load("WeaponFillBarReadySword", typeof(Sprite)) as Sprite;
        _statusBarNotReadySword = Resources.Load("WeaponFillBarSword", typeof(Sprite)) as Sprite;

        _currentWeaponSword = Resources.Load("Sword", typeof(Sprite)) as Sprite;
        _currentWeaponHammer = Resources.Load("Mjölnir", typeof(Sprite)) as Sprite;

	    _controller = GetComponent<WeaponStatusBarController>();
        _weaponStatusBar.GetComponent<UnityEngine.UI.Image>().sprite = _statusBarReady;
    }

    // Update is called once per frame
    private void Update () {
        UpdateCompletionPercent();
        UpdateStatusBar();
        UpdateStatusImage();

	}

    private void UpdateStatusImage()
    {
        if (GameManager.Instance.Player.WeaponController._weapon1Current) { 
            _currentWeaponImage.GetComponent<UnityEngine.UI.Image>().sprite = _currentWeaponSword;            
        }
        else
            _currentWeaponImage.GetComponent<UnityEngine.UI.Image>().sprite = _currentWeaponHammer;
    }

    private void UpdateStatusBar()
    {
        _controller.Progress = _baseCompletion + _scaledCompletion;       

        if (_completionPercent >= _endCompletion)
        {
            
            if (GameManager.Instance.Player.WeaponController._weapon1Current)
                _weaponStatusBar.GetComponent<UnityEngine.UI.Image>().sprite = _statusBarReadySword;
            else
            {

                _weaponStatusBar.GetComponent<UnityEngine.UI.Image>().sprite = _statusBarReady;
            }
        }
        else
        {
            if (GameManager.Instance.Player.WeaponController._weapon1Current)
                _weaponStatusBar.GetComponent<UnityEngine.UI.Image>().sprite = _statusBarNotReadySword;
            else
                _weaponStatusBar.GetComponent<UnityEngine.UI.Image>().sprite = _statusBarNotReady;
        }       
    }

    private void UpdateCompletionPercent()
    {
        float tmp = GameManager.Instance.Player.WeaponController.GetCompletion();

        _scaledCompletion = (1 - _baseCompletion - (1 - _endCompletion)) * tmp;

        _completionPercent = _baseCompletion + _scaledCompletion;

    }
}
