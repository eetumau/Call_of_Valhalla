﻿using UnityEngine;
using System.Collections;

public class GauntletTrigger : MonoBehaviour {

    private GauntletScript _gauntlet;
    private GameObject _trigger;

	// Use this for initialization
	private void Awake () {
        _gauntlet = FindObjectOfType<GauntletScript>();
        _trigger = GameObject.Find("GauntletTrigger");
        Debug.Log(_trigger);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _gauntlet.ActivateGauntlet();
            _trigger.SetActive(false);
            
        }
    }
}
