using UnityEngine;
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


        void OnTriggerEnter2D(Collider2D other)
        {
            _damage = _enemyAttack.Instance.Damage;

            if (other.gameObject.tag == "Player")
            {
                if (!_enemyAttack.DamageDealt)
                {
                    _player.TakeDamage(_damage);
                    _enemyAttack.DamageDealt = true;
                }

                
            }
        }

    }
}