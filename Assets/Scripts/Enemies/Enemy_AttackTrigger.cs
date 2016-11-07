﻿using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;

namespace CallOfValhalla.Enemy {
    public class Enemy_AttackTrigger : MonoBehaviour {

        private Enemy_Attack _enemyAttack;
        private int _damage;

        private Player_HP _player;


        // Use this for initialization
        void Start() {

            _enemyAttack = GetComponentInParent<Enemy_Attack>();
            _player = FindObjectOfType<Player_HP>();

        }

        // Update is called once per frame
        void Update() {
            _damage = _enemyAttack.Instance.Damage;

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                _player.TakeDamage(_damage);
            }
        }

    }
}