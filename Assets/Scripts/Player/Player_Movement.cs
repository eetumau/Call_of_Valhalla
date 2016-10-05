﻿using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Player
{
    public class Player_Movement : MonoBehaviour
    {

        private Transform _playerTransform;
        private Rigidbody2D _playerRigidbody2D;
        private WeaponController _weaponController;
        

        [SerializeField]
        private float _playerMoveSpeed;
        [SerializeField]
        private float _playerJumpForce;
        [SerializeField]
        private float _groundCheckRadius;
        [SerializeField]
        private Transform _groundCheckTransform;

        private bool _isGrounded;

        public WeaponController WeaponController
        {
            get { return _weaponController; }
        }

        // Use this for initialization
        void Start()
        {

            _playerTransform = GetComponent<Transform>();
            _playerRigidbody2D = GetComponent<Rigidbody2D>();
            _weaponController = GetComponent<WeaponController>();

        }

        // Update is called once per frame
        void Update()
        {

            CheckIfGrounded();

        }

        public void Move(float inputX)
        {

            _playerRigidbody2D.velocity = new Vector2(inputX * _playerMoveSpeed, _playerRigidbody2D.velocity.y);

        }

        public void Jump(bool _jump)
        {
            if (_isGrounded && _jump)
            {
                //_playerRigidbody2D.AddForce(new Vector2(0, _playerJumpForce));
                _playerRigidbody2D.velocity = new Vector2(_playerRigidbody2D.velocity.x, _playerJumpForce);
                _isGrounded = false;

            }

        }


        private void CheckIfGrounded()
        {
            _isGrounded = false;

            var colliders = Physics2D.OverlapCircleAll(_groundCheckTransform.position, _groundCheckRadius);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].tag == "Ground")
                {

                    _isGrounded = true;
                }
            }

        }

    }
}