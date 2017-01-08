using UnityEngine;
using System.Collections;
using System;
using CallOfValhalla.Player;

public class LokiTriggerScript : MonoBehaviour {

    private Player_CameraFollow _cameraFollow;
    private LokiController _lokiController;
    

	// Use this for initialization
	void Awake () {
        _cameraFollow = FindObjectOfType<Player_CameraFollow>();
        _lokiController = FindObjectOfType<LokiController>();
	}
	


    

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _cameraFollow.IncreaseCamera();
            _lokiController.StartBossFight();
        }


    }
}

