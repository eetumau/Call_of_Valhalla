﻿using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;

namespace CallOfValhalla.Enemy {
    public class Enemy_IgnoreCollision : MonoBehaviour {

        private Player_Movement _player;
        private BoxCollider2D _boxCollider;
        private CircleCollider2D _circleCollider;
        private BoxCollider2D _playerBoxCollider;
        private CircleCollider2D _playerCircleCollider;

        // Use this for initialization
        void Start() {
            _player = FindObjectOfType<Player_Movement>();
            _playerBoxCollider = _player.GetComponent<BoxCollider2D>();
            _playerCircleCollider = _player.GetComponentInChildren<CircleCollider2D>();
            _boxCollider = GetComponentInChildren<BoxCollider2D>();
            _circleCollider = GetComponentInChildren<CircleCollider2D>();

            Physics2D.IgnoreCollision(_boxCollider, _playerBoxCollider, true);
            Physics2D.IgnoreCollision(_boxCollider, _playerCircleCollider, true);
            Physics2D.IgnoreCollision(_circleCollider, _playerBoxCollider, true);
            Physics2D.IgnoreCollision(_circleCollider, _playerCircleCollider, true);

        }

        // Update is called once per frame
        void Update() {

        }
    }
}