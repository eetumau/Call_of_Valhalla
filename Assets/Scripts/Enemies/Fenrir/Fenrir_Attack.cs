using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;

namespace CallOfValhalla.Enemy
{
    public class Fenrir_Attack : MonoBehaviour
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
        private int _specialChance;
        [SerializeField]
        private float _attackTime;
        [SerializeField]
        private float _attackDelay;
        [SerializeField] private float _specialMovementSpeed;
        [SerializeField]
        private int _chargeTimes;

        private BoxCollider2D _attackHitBox;
        private Fenrir_Controller _enemyController;
        private Player_HP _player;
        private Animator _animator;
        private float _coolDownTimer;
        private float _attackTimer;
        private int _random;
        private int _randomSpecial;
        private Transform _transform;
        private bool _attacking = false;
        private bool _specialAttacking = false;
        private float _delayTimer;
        private Rigidbody2D _rigidBody;
        private Fenrir_Movement _movement;
        private bool _damageDealt;
        private GameObject _specialPointL;
        private GameObject _specialPointR;
        private float _distanceFromPointL;
        private float _distanceFromPointR;
        private bool _goRight;
        private int _chargeCounter;


        public Transform _bodyTransform;

        public BoxCollider2D AttackHitBox
        {
            get { return _attackHitBox; }
        }

        public bool Attacking
        {
            get { return _attacking; }
        }

        public bool SpecialAttacking
        {
            get { return _specialAttacking; }
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
            _attackHitBox = GetComponentInChildren<BoxCollider2D>();
            _attackHitBox.enabled = false;
            _player = FindObjectOfType<Player_HP>();
            _enemyController = GetComponentInParent<Fenrir_Controller>();
            _animator = GetComponentInParent<Animator>();
            _rigidBody = GetComponentInParent<Rigidbody2D>();
            _movement = GetComponentInParent<Fenrir_Movement>();

            _coolDownTimer = _attackCoolDown;
            _attackTimer = _attackTime;
            _delayTimer = _attackDelay;
            _specialPointL = GameObject.Find("SpecialPointL");
            _specialPointR = GameObject.Find("SpecialPointR");
            //_bodyTransform = GetComponentInParent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            RunTimers();
        }

        //Does all the calculations whether to attack or not
        private void RunTimers()
        {
            if (!_specialAttacking)
            {
                if (_enemyController.Instance.InAttackRange)
                {
                    if (_coolDownTimer <= 0)
                    {
                        if (!_attacking && !_specialAttacking)
                        {
                            _random = Random.Range(0, _attackChance + 1);
                            _randomSpecial = Random.Range(0, _specialChance + 1);
                        }

                        if (_randomSpecial == _specialChance)
                        {
                            _specialAttacking = true;
                            CheckSpecialPoints();
                            StartCoroutine(SpecialAttack());

                        }
                        else if (_random == _attackChance)
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
                    return;

                }

                _attackTimer -= Time.deltaTime;
            }

        }

        private void Attack()
        {
            PlaySound();

            _animator.SetInteger("animState", 2);

            if (_delayTimer <= 0)
            {
                _attackHitBox.enabled = true;
            }

            if (_delayTimer == _attackDelay && _leaping)
            {
                Leap();
            }

            _delayTimer -= Time.deltaTime;

        }

        private void PlaySound()
        {
            if (_attackTimer == _attackTime)
            {

                SoundManager.instance.PlaySound("wolf_bark_2", _enemyController.WeaponSource, false);
            }
        }

        private void Leap()
        {
            if (_movement.IsFacingRight)
            {
                _rigidBody.AddForce(new Vector2(600, 500));
            }
            else
            {
                _rigidBody.AddForce(new Vector2(-600, 500));
            }
        }

        private void CheckSpecialPoints()
        {
            _distanceFromPointL = _bodyTransform.position.x - _specialPointL.transform.position.x;
            _distanceFromPointR = _specialPointR.transform.position.x - _bodyTransform.position.x;

            if (_distanceFromPointL >= _distanceFromPointR)
            {
                _goRight = true;
            }else
            {
                _goRight = false;
            }

        }

        private IEnumerator SpecialAttack()
        {

            Vector3 faceRight = new Vector3(-2, _bodyTransform.localScale.y, _bodyTransform.localScale.z);
            Vector3 faceLeft = new Vector3(2, _bodyTransform.localScale.y, _bodyTransform.localScale.z);

            _animator.SetInteger("animState", 1);

            if (_goRight)
            {
                _bodyTransform.localScale = faceRight;
                _movement.IsFacingRight = true;
                while (_bodyTransform.position.x < _specialPointR.transform.position.x)
                {
                    _bodyTransform.position = Vector2.MoveTowards(_bodyTransform.position,
                    new Vector2(_specialPointR.transform.position.x, _bodyTransform.position.y),
                    Time.deltaTime * _specialMovementSpeed);

                    yield return null;
                }
                _goRight = false;

            }else
            {
                _bodyTransform.localScale = faceLeft;
                _movement.IsFacingRight = false;

                while (_bodyTransform.position.x > _specialPointL.transform.position.x)
                {
                    _bodyTransform.position = Vector2.MoveTowards(_bodyTransform.position,
                    new Vector2(_specialPointL.transform.position.x, _bodyTransform.position.y),
                    Time.deltaTime * _specialMovementSpeed);

                    yield return null;
                }
                _goRight = true;
            }


            _animator.SetInteger("animState", 0);
            yield return new WaitForSeconds(3);

            _attackHitBox.enabled = true;

            for (int i = 0; i <= _chargeTimes; i++)
            {
                _animator.SetInteger("animState", 1);
                if (_goRight)
                {
                    _bodyTransform.localScale = faceRight;
                    _movement.IsFacingRight = true;

                    while (_bodyTransform.position.x < _specialPointR.transform.position.x)
                    {
                        _bodyTransform.position = Vector2.MoveTowards(_bodyTransform.position,
                        new Vector2(_specialPointR.transform.position.x, _bodyTransform.position.y),
                        Time.deltaTime * _specialMovementSpeed);

                        yield return null;
                    }

                    _damageDealt = false;
                    _goRight = false;
                }else
                {
                    _bodyTransform.localScale = faceLeft;
                    _movement.IsFacingRight = false;

                    while (_bodyTransform.position.x > _specialPointL.transform.position.x)
                    {
                        _bodyTransform.position = Vector2.MoveTowards(_bodyTransform.position,
                        new Vector2(_specialPointL.transform.position.x, _bodyTransform.position.y),
                        Time.deltaTime * _specialMovementSpeed);

                        yield return null;
                    }
                    _damageDealt = false;
                    _goRight = true;
                }

                _animator.SetInteger("animState", 0);
                
                yield return new WaitForSeconds(2);
            }

            _attackHitBox.enabled = false;
            _specialAttacking = false;
        }
    }
}