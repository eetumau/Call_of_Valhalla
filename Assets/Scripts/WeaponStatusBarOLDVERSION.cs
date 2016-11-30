using UnityEngine;
using System.Collections;
using CallOfValhalla;
using System;

public class WeaponStatusBarOLDVERSION : MonoBehaviour {

    private Sprite _statusBarNotReady;
    private Sprite _statusBarReady;
    private Sprite _statusBarNotReadySword;
    private Sprite _statusBarReadySword;
    private Sprite _currentWeaponSword;
    private Sprite _currentWeaponHammer;

    private GameObject _weaponStatusBar;
    private GameObject _currentWeaponImage;
    private float _weaponCooldown;
    private float _weaponMaxCooldown;
    private float _completionPercent;
    private float _baseScale = 0.5f;
    private float _cooldownScale;
    private Vector3 _transformScale;
    
    

	// Use this for initialization
	private void Awake () {
        _weaponStatusBar = GameObject.Find("WeaponStatusBar");
        _currentWeaponImage = GameObject.Find("WeaponStatusImage");
        _transformScale = new Vector3(1, 1, 0);      
        _statusBarReady = Resources.Load("WeaponFillBarReady", typeof(Sprite)) as Sprite;
        _statusBarNotReady = Resources.Load("WeaponFillBar", typeof(Sprite)) as Sprite;

        _statusBarReadySword = Resources.Load("WeaponFillBarReadySword", typeof(Sprite)) as Sprite;
        _statusBarNotReadySword = Resources.Load("WeaponFillBarSword", typeof(Sprite)) as Sprite;

        _currentWeaponSword = Resources.Load("Sword", typeof(Sprite)) as Sprite;
        _currentWeaponHammer = Resources.Load("Mjölnir", typeof(Sprite)) as Sprite;

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
        _transformScale.x = _completionPercent;
        _transformScale.y = _completionPercent;
        _weaponStatusBar.transform.localScale = _transformScale;
        Debug.Log(_completionPercent);

        if (_completionPercent >= 1f)
        {
            if (GameManager.Instance.Player.WeaponController._weapon1Current)
                _weaponStatusBar.GetComponent<UnityEngine.UI.Image>().sprite = _statusBarReadySword;
            else
                _weaponStatusBar.GetComponent<UnityEngine.UI.Image>().sprite = _statusBarReady;
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
        _completionPercent = GameManager.Instance.Player.WeaponController.GetCompletion();

    }
}
