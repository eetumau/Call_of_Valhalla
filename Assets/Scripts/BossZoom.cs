using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;

public class BossZoom : MonoBehaviour {

	private Player_CameraFollow _camera;

	// Use this for initialization
	void Awake () {
		_camera = FindObjectOfType<Player_CameraFollow>();
	}
	
	// Update is called once per frame
	void OnCollisionEnter2D (Collider2D other) {
		if (other.tag == "Player")
			_camera.IncreaseCamera();
	}
}
