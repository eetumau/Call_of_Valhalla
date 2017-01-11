using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy {
    public class Surt_Death : MonoBehaviour {

        private Surt_Movement _movement;
        private Surt_Attack _attack;
        private Enemy_HP _hp;
        private Animator _animator;

        private bool _dead;

        public bool Dead
        {
            get { return _dead; }
        }

        // Use this for initialization
        void Start() {
            _movement = GetComponent<Surt_Movement>();
            _attack = GetComponentInChildren<Surt_Attack>();
            _hp = GetComponent<Enemy_HP>();
            _animator = GetComponent<Animator>();
        }

        public void Die()
        {
            _dead = true;
            _attack.StopAllCoroutines();
            _animator.SetInteger("animState", 3);
            _movement.enabled = false;
            _attack.enabled = false;
            _hp.enabled = false;

        }
    }
}