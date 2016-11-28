using UnityEngine;
using System.Collections;
using CallOfValhalla;
using System;

public class WeaponStatusBar : MonoBehaviour {

    private Sprite _statusBarNotReady;
    private Sprite _statusBarReady;
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

        _currentWeaponSword = Resources.Load("Sword", typeof(Sprite)) as Sprite;
        _currentWeaponHammer = Resources.Load("Mjölnir", typeof(Sprite)) as Sprite;

        _weaponStatusBar.GetComponent<UnityEngine.UI.Image>().sprite = _statusBarReady;
    }

    // Update is called once per frame
    private void Update () {
        UpdateCooldown();
        UpdateCompletionPercent();
        UpdateStatusBar();
        UpdateStatusImage();

	}

    private void UpdateStatusImage()
    {
        if (GameManager.Instance.Player.WeaponController._weapon1Current)
            _currentWeaponImage.GetComponent<UnityEngine.UI.Image>().sprite = _currentWeaponSword;
        else
            _currentWeaponImage.GetComponent<UnityEngine.UI.Image>().sprite = _currentWeaponHammer;
    }

    private void UpdateStatusBar()
    {
        _transformScale.x = (_baseScale + (_cooldownScale/100f));
        _transformScale.y = (_baseScale + (_cooldownScale / 100f));
        _weaponStatusBar.transform.localScale = _transformScale;

        if (_completionPercent >= 100f)
            _weaponStatusBar.GetComponent<UnityEngine.UI.Image>().sprite = _statusBarReady;
        else
            _weaponStatusBar.GetComponent<UnityEngine.UI.Image>().sprite = _statusBarNotReady;        
    }

    private void UpdateCompletionPercent()
    {
        float tmp = ((_weaponCooldown / _weaponMaxCooldown) * 100f) ;
        _completionPercent = 100 - tmp;

        _cooldownScale = (1 - _baseScale)*_completionPercent;

    }

    private void UpdateCooldown()
    {
        _weaponCooldown = GameManager.Instance.Player.WeaponController.GetCooldown();
        _weaponMaxCooldown = GameManager.Instance.Player.WeaponController.GetMaxCooldown();
    }
}
