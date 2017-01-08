using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;

namespace CallOfValhalla.Enemy
{
    public class EnemyFlying_Attack : MonoBehaviour
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
        [SerializeField]
        private float _returnTime;

        private BoxCollider2D _attackHitBox;
        private EnemyFlying_Attack _instance;
        private Enemy_Controller _enemyController;
        private Enemy_Movement _enemyMovement;
        private Player_HP _player;
        private Animator _animator;
        private float _coolDownTimer;
        private float _attackTimer;
        private int _random;
        private Transform _transform;
        private bool _attacking = false;
        private float _delayTimer;
        private Transform _targetTransform;
        private Vector3 _returnPos;
        private float _returnTimer;
       

        public EnemyFlying_Attack Instance
        {
            get { return _instance; }
        }

        public int Damage
        {
            get { return _damage; }
        }

        // Use this for initialization
        void Start()
        {
            _enemyMovement = GetComponentInParent<Enemy_Movement>();

            _instance = this;
            _attackHitBox = GetComponentInChildren<BoxCollider2D>();
            _attackHitBox.enabled = false;
            _player = FindObjectOfType<Player_HP>();
            _enemyController = GetComponentInParent<Enemy_Controller>();
            _animator = GetComponentInParent<Animator>();
            _targetTransform = _player.GetComponent<Transform>();

            _coolDownTimer = _attackCoolDown;
            _attackTimer = _attackTime;
            _delayTimer = _attackDelay;
            _returnTimer = _returnTime;
        }

        // Update is called once per frame
        void Update()
        {
            _transform = _enemyMovement.Instance.Transform;

            if (_enemyController.InAttackRange)
            {
                if (_coolDownTimer <= 0)
                {
                    if (!_attacking)
                    {
                        _returnPos = _transform.position;
                        Debug.Log(_returnPos);
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


        }

        public void DealDamage()
        {
            _player.Instance.TakeDamage(_damage);
        }

        private void Attack()
        {

            //_animator.SetInteger("animState", 2);

            if (_delayTimer <= 0)
            {
                _attackHitBox.enabled = true;

                var _targetRotation = Quaternion.LookRotation(_targetTransform.position - _transform.position);
                _transform.rotation = Quaternion.Lerp(_transform.rotation, new Quaternion(_transform.rotation.x, _transform.rotation.y, _targetRotation.z, _transform.rotation.w), 2 * Time.deltaTime);
                _transform.position = Vector3.Lerp(_transform.position, _targetTransform.position, 2* Time.deltaTime);
                

            }



            if (_attackTimer <= 0)
            {
                var _targetRotation = Quaternion.LookRotation(_returnPos - _transform.position);
                _transform.rotation = Quaternion.Lerp(_transform.rotation, new Quaternion(_transform.rotation.x, _transform.rotation.y, _targetRotation.z, _transform.rotation.w), 2 * Time.deltaTime);
                _transform.position = Vector3.Lerp(_transform.position, _returnPos, Time.deltaTime);
                

                if(_returnTimer <= 0)
                {
                    _transform.rotation = _transform.rotation = new Quaternion(_transform.rotation.x, _transform.rotation.y, 0, _transform.rotation.w);

                    _attackHitBox.enabled = false;
                    //_animator.SetInteger("animState", 0);
                    _attacking = false;
                    _attackTimer = _attackTime;
                    _delayTimer = _attackDelay;
                    _coolDownTimer = _attackCoolDown;
                    _returnTimer = _returnTime;
                }

                _returnTimer -= Time.deltaTime;
            }

            _attackTimer -= Time.deltaTime;
            _delayTimer -= Time.deltaTime;

        }
    }
}