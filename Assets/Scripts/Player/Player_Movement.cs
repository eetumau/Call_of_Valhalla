using UnityEngine;
using System.Collections;
using System;

namespace CallOfValhalla.Player
{
    public class Player_Movement : MonoBehaviour
    {

        public Transform _playerTransform;
        private Rigidbody2D _playerRigidbody2D;
        public Animator animator;

        [SerializeField]
        private float _playerMoveSpeed;
        [SerializeField]
        private float _playerJumpForce;
        [SerializeField]
        private float _groundCheckRadius;
        [SerializeField]
        private Transform _groundCheckTransform;
        [SerializeField]
        private float _soundDelay;

        private bool _cantMove;
        public bool _isGrounded;
        private bool _attacking;
        private bool _swordDashing;
        private float _swordAttackTimer;
        private float _swordDashTimer;
        public bool _hammerBasicActive;
        public bool _hammerSpecialActive;
        private Player_HP _hp;
        private WeaponController _weaponController;
        private AudioSource _source;
        private AudioSource _groundSource;
        private float _soundDelayTimer;

        public Player_HP HP
        {
            get { return _hp; }
        }

        public WeaponController WeaponController
        {
            get { return _weaponController; }
        }

        public AudioSource Source
        {
            get { return _source; }
        }

        // Use this for initialization
        void Start()
        {
            _playerTransform = GetComponent<Transform>();
            _playerRigidbody2D = GetComponent<Rigidbody2D>();
            _weaponController = GetComponent<WeaponController>();
            _hp = GetComponent<Player_HP>();
            _source = GetComponent<AudioSource>();
            _groundSource = _groundCheckTransform.gameObject.GetComponent<AudioSource>();
            _soundDelayTimer = _soundDelay;
        }


        // Update is called once per frame
        void Update()
        {
            CheckIfGrounded();
                      
            if (!_hammerSpecialActive && !_hammerBasicActive)
                CheckAnimations();

            SwordSpecialAttack();
            RunTimers();
            CheckTimers();
        }        

        public void CheckAnimations()
        {   
            // Determines which way the player looks
            if (Input.GetAxis("Horizontal") > 0 && !_cantMove)
                _playerTransform.localScale = new Vector3(1, 1, 1);
            else if (Input.GetAxis("Horizontal") < 0 && !_cantMove)
                _playerTransform.localScale = new Vector3(-1, 1, 1);

            // Sets animations based on movement
            if (_swordAttackTimer <= 0)
            {
                if (_isGrounded == true && Input.GetAxis("Horizontal") != 0 && !_cantMove)                
                    animator.SetInteger("animState", 1);                                  
                else if (_isGrounded == false && _playerRigidbody2D.velocity.y > 0.5)
                    animator.SetInteger("animState", 2);
                else if (_isGrounded == false && _playerRigidbody2D.velocity.y < 0.5)
                    animator.SetInteger("animState", 3);
                else
                    animator.SetInteger("animState", 0);
            }      
        }

        public void SwordSpecialAttack()
        {
            if (_swordDashing && _swordDashTimer > 0 && !_cantMove)
                if (_playerTransform.localScale.x == 1)
                    _playerRigidbody2D.velocity = new Vector2(25, 0);
                else
                    _playerRigidbody2D.velocity = new Vector2(-25, 0);
            else if (_swordDashing && _swordDashTimer <= 0)
            {
                _swordDashing = false;
                _playerRigidbody2D.velocity = new Vector2(0, 0);
            }
                
        }

        public void ResetAttackTimer()
        {
            _swordAttackTimer = 0;
        }


        public void SetAttackAnimation(string animation, float timer = 0)
        {

            if (animation.Equals("swordbasic1"))
            {
                animator.SetInteger("animState", 4);
                _swordAttackTimer = timer;
            }
            else if (animation.Equals("swordbasic2"))
            {
                animator.SetInteger("animState", 5);
                _swordAttackTimer = timer;
            }
            else if (animation.Equals("swordspecial"))
            {
                animator.SetInteger("animState", 6);
                _swordAttackTimer = timer;
            }
            else if (animation.Equals("hammerbasic"))
            {
                animator.SetInteger("animState", 4);
                _hammerBasicActive = true;
            }
            else if (animation.Equals("Charge1"))
            {
                animator.SetInteger("animState", 6);
                _hammerSpecialActive = true;
            }
            else if (animation.Equals("Charge2"))
            {
                animator.SetInteger("animState", 7);
                _hammerSpecialActive = true;
            }
            else if (animation.Equals("Charge3"))
            {
                animator.SetInteger("animState", 8);
                _hammerSpecialActive = true;
            }
            else if (animation.Equals("HammerSpecial"))
            {
                animator.SetInteger("animState", 10);
                _hammerSpecialActive = true;
            }
            else if (animation.Equals("HammerSpecialFull"))
            {
                animator.SetInteger("animState", 11);
                _hammerSpecialActive = true;
            }
        }

        public void Move(float inputX)
        {

            if (_swordDashTimer <= 0 && !_hammerSpecialActive && !_hammerBasicActive)
            _playerRigidbody2D.velocity = new Vector2(inputX * _playerMoveSpeed, _playerRigidbody2D.velocity.y);

            PlaySound();

        }
        
        public void SwordDash()
        {
            if (_playerTransform.localScale.x == 1)
            {
                _swordAttackTimer = 0.3f;
                _swordDashTimer = 0.3f;
                _swordDashing = true;                
            } else
            {                                
                _swordAttackTimer = 0.3f;
                _swordDashTimer = 0.3f;
                _swordDashing = true;
            }
        }

        public void Jump(bool _jump)
        {
            if (_isGrounded && _jump)
            {
                SoundManager.instance.PlaySound("jump", _groundSource, false);
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
                if (colliders[i].gameObject.tag == "Ground")
                {
                        _isGrounded = true;
                }
            }

        }

        public void StopCharacter()
        {
            _cantMove = true;
            _playerRigidbody2D.velocity = new Vector2(0, 0);
        }

        public void ReleaseCharacter()
        {
            _cantMove = false;
        }

        private void RunTimers()
        {
            if (_swordAttackTimer >= 0)            
                _swordAttackTimer -= Time.deltaTime;      
            if (_swordDashTimer >= 0)
                _swordDashTimer -= Time.deltaTime;
        }

        private void CheckTimers()
        {
            /*
            if (_swordDashing && _swordDashTimer <= 0)
            {
                _playerRigidbody2D.velocity = new Vector2(0, 0);
                _swordDashing = false;
            }
                */
        }

        private void PlaySound()
        {
            if (_isGrounded && _soundDelayTimer <= 0)
            {
                float random = UnityEngine.Random.Range(0, 3);

                string sound;

                if (random == 0)
                {
                    sound = "footsteps - Step1";
                }
                else if (random == 1)
                {
                    sound = "footsteps - Step2";
                }
                else if (random == 2)
                {
                    sound = "footsteps - Step3";
                }
                else
                {
                    sound = "footsteps - Step4";
                }

                SoundManager.instance.PlaySound(sound, _groundSource, false);
                _soundDelayTimer = _soundDelay;
            }

            _soundDelayTimer -= Time.deltaTime;
        }
    }
}