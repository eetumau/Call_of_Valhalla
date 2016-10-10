using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponSelection : MonoBehaviour {

    [SerializeField]
    private Toggle _swordToggle;
    [SerializeField]
    private Toggle _axeToggle;
    [SerializeField]
    private Toggle _hammerToggle;
    [SerializeField]
    private Button _confirmButton;

    private int _weaponsSelected = 0;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

        
       // _swordToggle.onValueChanged.AddListener((t) => { ChangeState(_swordToggle.isOn); });


    }

    private void ChangeState(bool selected)
    {
        if (selected)
        {
            _weaponsSelected += 1;
        } else
        {
            _weaponsSelected -= 1;
        }

        Debug.Log(_weaponsSelected);
    }
}
