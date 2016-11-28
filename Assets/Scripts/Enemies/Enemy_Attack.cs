using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;

namespace CallOfValhalla.Enemy
{
    public class Enemy_Attack : MonoBehaviour
    {

        //This is just for checking if the enemy is a wolf so the attack includes leaping.
        [SerializeField]
        private bool _leaping;

        [SerializeField]
        private int _damage;
        [SerializeField]
        private float _attackCoolDown;
        [SerializeField]
        private int _attackChance;
        [SerializeField]
        private float _attackTime;
        [SerializeField]
        private float _attackDelay;

        private BoxCollider2D _attackHitBox;
        private Enemy_Attack _instance;
        private Enemy_Controller _enemyController;
        private Player_HP _player;
        private Animator _animator;
        private float _coolDownTimer;
        private float _attackTimer;
        private int _random;
        private Transform _transform;
        private bool _attacking = false;
        private float _delayTimer;
        private Rigidbody2D _rigidBody;
        private Enemy_Movement _movement;
        private bool _damageDealt;

        public Enemy_Attack Instance
        {
            get { return _instance; }
        }

        public BoxCollider2D AttackHitBox
        {
            get { return _attackHitBox; }
        }

        public bool Attacking
        {
            get { return _attacking; }
        }

        public int Damage
        {
            get { return _damage; }
        }

        public bool DamageDealt
        {
            get { return _damageDealt; }
            set { _damageDealt = value; }
        }

        // Use this for initialization
        void Start()
        {
            _transform = GetComponent<Transform>();
            _instance = this;
            _attackHitBox = GetComponentInChildren<BoxCollider2D>();
            _attackHitBox.enabled = false;
            _player = FindObjectOfType<Player_HP>();
            _enemyController = GetComponentInParent<Enemy_Controller>();
            _animator = GetComponentInParent<Animator>();
            _rigidBody = GetComponentInParent<Rigidbody2D>();
            _movement = GetComponentInParent<Enemy_Movement>();

            _coolDownTimer = _attackCoolDown;
            _attackTimer = _attackTime;
            _delayTimer = _attackDelay;
        }

        // Update is called once per frame
        void Update()
        {
            RunTimers();
        }

        //Does all the calculations whether to attack or not
        private void RunTimers()
        {
            if (_enemyController.Instance.InAttackRange)
            {
                if (_coolDownTimer <= 0)
                {
                    if (!_attacking)
                    {
                        _random = Random.Range(0, _attackChance + 1);
                    }

                    if (_random == _attackChance)
                    {
                        _attacking = true;
                        Attack();
                    }
                    else
                    {
                        _coolDownTimer = _attackCoolDown;
                    }
                }
                else
                {
                    _coolDownTimer -= Time.deltaTime;
                }
            }

            if (_attacking)
            {
                if (_attackTimer <= 0)
                {
                    _attackHitBox.enabled = false;
                    _animator.SetInteger("animState", 0);
                    _attacking = false;
                    _attackTimer = _attackTime;
                    _delayTimer = _attackDelay;
                    _coolDownTimer = _attackCoolDown;
                    _damageDealt = false;

                }

                _attackTimer -= Time.deltaTime;
            }

        }

        //public void DealDamage()
        //{
        //    if (!_damageDealt)
        //    {
        //        _player.Instance.TakeDamage(_damage);

        //    }
        //}

        private void Attack()
        {

            _animator.SetInteger("animState", 2);

            if (_delayTimer <= 0)
            {
                _attackHitBox.enabled = true;
            }

            if (_delayTimer == _attackDelay && _leaping)
            {
                //This is for the wolf only
                Leap();
            }

            _delayTimer -= Time.deltaTime;

        }

        private void Leap()
        {
            if (_movement.IsFacingRight)
            {
                _rigidBody.AddForce(new Vector2(500, 500));
            }
            else
            {
                _rigidBody.AddForce(new Vector2(-500, 500));
            }
        }
    }
}