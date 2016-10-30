using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;

namespace CallOfValhalla.Enemy
{
    public class Enemy_Attack : MonoBehaviour
    {

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

        public Enemy_Attack Instance
        {
            get { return _instance; }
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

            _coolDownTimer = _attackCoolDown;
            _attackTimer = _attackTime;
            _delayTimer = _attackDelay;
        }

        // Update is called once per frame
        void Update()
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
                    }else
                    {
                        _coolDownTimer = _attackCoolDown;
                    }
                }
                else
                {
                    _coolDownTimer -= Time.deltaTime;
                }
            }


        }

        public void DealDamage()
        {
            _player.Instance.TakeDamage(_damage);
        }

        private void Attack()
        {

            _animator.SetInteger("animState", 2);

            if (_delayTimer <= 0)
            {
                _attackHitBox.enabled = true;
                _transform.position = _transform.position + Vector3.zero;   
            }
            


            if(_attackTimer <= 0)
            {
                _attackHitBox.enabled = false;
                _animator.SetInteger("animState", 0);
                _attacking = false;
                _attackTimer = _attackTime;
                _delayTimer = _attackDelay;

            }

            _attackTimer -= Time.deltaTime;
            _delayTimer -= Time.deltaTime;

        }
    }
}