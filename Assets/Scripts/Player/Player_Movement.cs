using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Player
{
    public class Player_Movement : MonoBehaviour
    {

        private Transform _playerTransform;
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

        public bool _isGrounded;
        private bool _attacking;
        private bool _swordDashing;
        private float _attackTimer;
        private float _swordDashTimer;
        private Player_HP _hp;
        private WeaponController _weaponController;

        public Player_HP HP
        {
            get { return _hp; }
        }

        public WeaponController WeaponController
        {
            get { return _weaponController; }
        }

        // Use this for initialization
        void Start()
        {
            _playerTransform = GetComponent<Transform>();
            _playerRigidbody2D = GetComponent<Rigidbody2D>();
            _weaponController = GetComponent<WeaponController>();
            _hp = GetComponent<Player_HP>();
        }


        // Update is called once per frame
        void Update()
        {
            CheckIfGrounded();

            
            CheckAnimations();

            SpecialAttacks();
            RunTimers();
            CheckTimers();
        }

        public void CheckAnimations()
        {   
            // Determines which way the player looks
            if (Input.GetAxis("Horizontal") > 0)
                _playerTransform.localScale = new Vector3(1, 1, 1);
            else if (Input.GetAxis("Horizontal") < 0)
                _playerTransform.localScale = new Vector3(-1, 1, 1);

            // Sets animations based on movement
            if (_attackTimer <= 0)
            {
                if (_isGrounded == true && Input.GetAxis("Horizontal") != 0)
                    animator.SetInteger("animState", 1);
                else if (_isGrounded == false && _playerRigidbody2D.velocity.y > 0.5)
                    animator.SetInteger("animState", 2);
                else if (_isGrounded == false && _playerRigidbody2D.velocity.y < 0.5)
                    animator.SetInteger("animState", 3);
                else
                    animator.SetInteger("animState", 0);
            }      
        }

        public void SpecialAttacks()
        {
            if (_swordDashing && _swordDashTimer > 0)
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
            _attackTimer = 0;
        }

        public void SetAttackAnimation(string animation, float timer)
        {
            
            if (animation.Equals("swordbasic1"))
            {                
                animator.SetInteger("animState", 4);
                _attackTimer = timer;
            } else if (animation.Equals("swordbasic2"))
            {
                animator.SetInteger("animState", 5);
                _attackTimer = timer;
            }
            else if (animation.Equals("swordspecial"))
            {   
                animator.SetInteger("animState", 6);
                _attackTimer = timer;
            }
        }

        public void Move(float inputX)
        {
            if (_swordDashTimer <= 0)
            _playerRigidbody2D.velocity = new Vector2(inputX * _playerMoveSpeed, _playerRigidbody2D.velocity.y);

        }
        
        public void SwordDash()
        {
            if (_playerTransform.localScale.x == 1)
            {
                //_playerRigidbody2D.velocity = new Vector2(0, _playerRigidbody2D.velocity.y);
                //_playerRigidbody2D.AddForce(_playerTransform.right * 900);
                _attackTimer = 0.3f;
                _swordDashTimer = 0.3f;
                _swordDashing = true;                
            } else
            {                
                //_playerRigidbody2D.velocity = new Vector2(0, _playerRigidbody2D.velocity.y);
                //_playerRigidbody2D.AddForce(_playerTransform.right * -900);
                _attackTimer = 0.3f;
                _swordDashTimer = 0.3f;
                _swordDashing = true;
            }
        }

        public void Jump(bool _jump)
        {
            if (_isGrounded && _jump)
            {
                //_playerRigidbody2D.AddForce(new Vector2(0, _playerJumpForce));
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

        private void RunTimers()
        {
            if (_attackTimer >= 0)            
                _attackTimer -= Time.deltaTime;      
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
    }
}