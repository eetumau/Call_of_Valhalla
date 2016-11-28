﻿using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy {
    public class Enemy_HP : MonoBehaviour {

        [SerializeField]
        private int _hitPoints;
        [SerializeField]
        private GameObject _blood;

        private Animator _animator;
        private Enemy_Controller _enemyController;
        private Transform _transform;

        public int HP
        {
            get { return _hitPoints; }
        }
        // Use this for initialization
        void Start() {

            _enemyController = GetComponent<Enemy_Controller>();
            _animator = GetComponent<Animator>();
            _transform = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update() {

            if (_hitPoints <= 0)
            {
                _animator.SetInteger("animState", 3);

                _enemyController.Instance.Die();
            }

        }

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            Instantiate(_blood, _transform.position+Vector3.up, _transform.rotation );
        }
    }
}