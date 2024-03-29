﻿using UnityEngine;
using System.Collections;
using CallOfValhalla;
using CallOfValhalla.UI;

namespace CallOfValhalla.Player
{
    public class Player_HP : MonoBehaviour
    {

        [SerializeField]
        private int _OriginalHP;
        [SerializeField]
        private float _deathDelay;

        private Player_CameraFollow _cameraFollow;
        private Player_HP _instance;
        private Animator _animator;
        public int _hp;
        private Weapon_Hammer _hammer;

        private Player_Movement _movement;
        private Player_InputController _input;
        private Transform _transform;
        private HPBarController _hpBar;
        private bool _respawning;
        private bool _dead;

        public Player_HP Instance
        {
            get { return _instance; }
        }

        public int HP
        {
            get { return _hp; }
            set { _hp = value; }
        }

        // Use this for initialization
        void Awake()
        {
            _instance = this;
            _animator = GetComponent<Animator>();
            _movement = GetComponent<Player_Movement>();
            _input = GetComponent<Player_InputController>();
            _hp = _OriginalHP;
            _transform = GetComponent<Transform>();
            _cameraFollow = FindObjectOfType<Player_CameraFollow>();
            _hpBar = FindObjectOfType<HPBarController>();
            _hammer = GetComponent<Weapon_Hammer>();
        }

        public void TakeDamage(int damage)
        {
            if (_hp < damage)
            {
                _hp = 0;
            }            
            if (_hp > 0)
            {
                _hp -= damage;
                _hpBar.SecondaryProgress += ((float)damage) / ((float)_OriginalHP);
            }                                             

            _hpBar.Progress = ((float)_hp) / ((float)_OriginalHP);                       

            if (_hp <= 0 && !_dead)
            {
                SoundManager.instance.PlaySound("death_3", _movement.Source, false);

                Time.timeScale = 0.5f;
                Die();
            }else
            {
                if(HP > 0)
                SoundManager.instance.PlaySound("ouch", _movement.Source, false);
            }
        }

        public void Die()
        {
            if (!_respawning)
            {
                _hammer.LightningSource.loop = false;
                StartCoroutine(_hammer.ResetAfterSpecial(0));
                GameManager.Instance.CameraFollow.Sepia.enabled = true;
                _dead = true;
                _movement.enabled = false;
                _input.enabled = false;
                _animator.SetInteger("animState", 9);
                StartCoroutine(GameOverTimer(_deathDelay));
               
            }
        }

        private IEnumerator GameOverTimer(float howLong)
        {
            yield return new WaitForSeconds(howLong);
            GameManager.Instance.GameOver();
        }

        public void Respawn(Transform spawnPoint)
        {
            Time.timeScale = 1;
            GameManager.Instance.CameraFollow.Sepia.enabled = false;
            _respawning = true;
            _dead = false;
            _movement.enabled = true;
            _input.enabled = true;
            _animator.SetInteger("animState", 0);
            _hp = _OriginalHP;
            _transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, _transform.position.z);
            _respawning = false;
            _cameraFollow.DecreaseCamera();
            _hpBar.Progress = 1f;

        }

    }
}