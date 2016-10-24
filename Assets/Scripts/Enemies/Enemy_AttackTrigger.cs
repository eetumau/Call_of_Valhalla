using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy {
    public class Enemy_AttackTrigger : MonoBehaviour {

        private Enemy_Attack _enemyAttack;


        // Use this for initialization
        void Start() {

            _enemyAttack = GetComponentInParent<Enemy_Attack>();

        }

        // Update is called once per frame
        void Update() {

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                _enemyAttack.Instance.DealDamage();
            }
        }

    }
}