﻿using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Player
{
    public class Player_CameraFollow : MonoBehaviour
    {

        [SerializeField]
        private float _rightBorderOffset;
        [SerializeField]
        private float _leftBorderOffset;
        [SerializeField]
        private float _topBorderOffset;
        [SerializeField]
        private float _bottomBorderOffset;
        [SerializeField]
        private Transform _playerTransform;

        private Transform _transform;

        // Use this for initialization
        void Start()
        {

            _transform = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {

            MoveCamera();

        }

        private void MoveCamera()
        {
            if (_playerTransform.position.x >= _transform.position.x + _rightBorderOffset)
            {
                _transform.position = new Vector3(_playerTransform.position.x - _rightBorderOffset, _transform.position.y, _transform.position.z);
            }

            if (_playerTransform.position.x <= _transform.position.x - _leftBorderOffset)
            {
                _transform.position = new Vector3(_playerTransform.position.x + _leftBorderOffset, _transform.position.y, _transform.position.z);
            }

            if (_playerTransform.position.y >= _transform.position.y + _topBorderOffset)
            {
                _transform.position = new Vector3(_transform.position.x, _playerTransform.position.y - _topBorderOffset, _transform.position.z);
            }

            if (_playerTransform.position.y <= _transform.position.y - _bottomBorderOffset)
            {
                _transform.position = new Vector3(_transform.position.x, _playerTransform.position.y + _bottomBorderOffset, _transform.position.z);
            }
        }
    }
}