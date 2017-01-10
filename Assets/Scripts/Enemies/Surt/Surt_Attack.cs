﻿using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy {
    public class Surt_Attack : MonoBehaviour {

        [SerializeField]
        private int _damage;
        [SerializeField]
        private float _attackCooldown;
        [SerializeField]
        private float _attackChance;
        [SerializeField]
        private float _shootDuration;
        [SerializeField]
        private float _rainDuration;

        private Surt_Movement _movement;
        private Surt_AttackTrigger _trigger;
        private Surt_AnimationController _surtAC;
        private Enemy_HP _hp;
        public ParticleSystem[] _ps;
        private float _cooldownTimer;
        private bool _attacking;
        private bool _specialAttacking;
        private bool _specialDone;



        public bool Attacking
        {
            get { return _attacking; }
        }

        public int Damage
        {
            get { return _damage; }
        }

        // Use this for initialization
        void Start() {
            _movement = GetComponentInParent<Surt_Movement>();
            _trigger = GetComponentInChildren<Surt_AttackTrigger>();
            _surtAC = GetComponentInParent<Surt_AnimationController>();
            _hp = GetComponentInParent<Enemy_HP>();
            _cooldownTimer = _attackCooldown;

            _ps = GetComponentsInChildren<ParticleSystem>();
        }

        // Update is called once per frame
        void Update() {

            if (_movement.InRange && !_attacking)
            {
                CheckNextAction();
            }

        }

        private void CheckNextAction()
        {
            if(_hp.HP < _hp.OGHP / 0.5 && !_specialDone)
            {
                StartCoroutine(SpecialAttack());
            }

            if(_cooldownTimer <= 0)
            {
                int random = (int)Random.Range(0, _attackChance +1);

                Debug.Log(random);
                if(random == _attackChance)
                {
                    Attack();
                }else
                {
                    _cooldownTimer = _attackCooldown;
                }
            }

            if (!_attacking)
            {
                _cooldownTimer -= Time.deltaTime;
            }
        }

        private void Attack()
        {
            Debug.Log("Attack");
            _attacking = true;
            _surtAC.SetAnimation(2);
        }

        private IEnumerator SpecialAttack()
        {
            _specialDone = true;
            StartParticles();
            _surtAC.SetAnimation(6);
            _specialAttacking = true;
            _attacking = true;

            yield return new WaitForSeconds(_shootDuration);

            StopParticles();
            _attacking = false;
            _specialAttacking = false;
        }

        private void StartParticles()
        {
            foreach(ParticleSystem ps in _ps)
            {
                ps.Play();
            }
        }

        private void StopParticles()
        {
            foreach(ParticleSystem ps in _ps)
            {
                ps.Stop();
            }
        }

        public void EnableAttackHitBox()
        {
            Debug.Log("HitBox");
            _surtAC.SetAnimation(0);
            _trigger.GetComponent<BoxCollider2D>().enabled = true;
        }

        public void DisableAttackHitBox()
        {
            _cooldownTimer = _attackCooldown;
            _attacking = false;
            _trigger.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}