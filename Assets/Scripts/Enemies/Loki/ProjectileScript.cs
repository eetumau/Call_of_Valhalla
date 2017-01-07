﻿using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

    private Rigidbody2D _rigidBody;
    private GameObject _player;
    private Transform _playerTransform;
    private Transform _startingPointTransform;
    private int _speed = 6;

	// Use this for initialization
	void Awake () {

        _startingPointTransform = GameObject.Find("ProjectileStartingPoint").GetComponent<Transform>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("HeroSword_0");
        _playerTransform = _player.GetComponent<Transform>();
	}
	
	void OnEnable()
    {
        transform.position = _startingPointTransform.position;
        transform.LookAt(_playerTransform);
        _rigidBody.velocity = transform.forward * _speed;
        StartCoroutine(Disable(5));        
    }

    public void SetSpeed(int speed)
    {
        _speed = speed;
    }

    private IEnumerator Disable(float howLong)
    {
        yield return new WaitForSeconds(howLong);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _rigidBody.velocity = new Vector3(0, 0, 0);
    }
}