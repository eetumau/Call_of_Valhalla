﻿using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy
{
    public class Surt_AnimationController : MonoBehaviour
    {

        private Animator _animator;
        private Surt_Movement _movement;
        private Surt_Attack _attack;


        // Use this for initialization
        void Start()
        {
            _animator = GetComponent<Animator>();
            _movement = GetComponent<Surt_Movement>();
            _attack = GetComponentInChildren<Surt_Attack>();
        }


        public void SetAnimation(int animation)
        {
            _animator.SetInteger("animState", animation);
        }

        public void EnableAttackHitBox()
        {
            _attack.EnableAttackHitBox();
        }

        public void DisableAttackHitBox()
        {
            _attack.DisableAttackHitBox();
        }

        public void EnableAttacking()
        {
            _attack.EnableAttacking();
        }

        public void DisableAttacking()
        {
            _attack.DisableAttacking();
        }
    }
}