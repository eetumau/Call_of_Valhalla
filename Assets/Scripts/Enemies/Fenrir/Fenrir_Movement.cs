using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;

namespace CallOfValhalla.Enemy
{
    public class Fenrir_Movement : MonoBehaviour
    {

        [SerializeField]
        private float _passiveMovementSpeed;
        [SerializeField]
        private float _aggressiveMovementSpeed;
        [SerializeField]
        private float _timeBetweenActions;
        [SerializeField]
        private float _minDistanceFromPlayer;

        private Transform _transform;
        private bool _isFacingRight;
        private Fenrir_Controller _enemyController;
        private float _movingTimer;
        private float _stunTimer;
        private float _actionTimer;
        private float _knockbackTimer;
        private bool _isIdle = true;
        private Animator _animator;
        private Fenrir_Movement _instance;
        private Fenrir_WallCheck _wallCheck;
        private Rigidbody2D _rigidBody2D;
        private Fenrir_Attack _enemyAttack;
        private Player_Movement _playerMovement;
        private Transform _player;

        public Fenrir_Movement Instance
        {
            get { return _instance; }
        }

        public Transform Transform
        {
            get { return _transform; }
        }

        public bool IsFacingRight
        {
            get { return _isFacingRight; }
            set { _isFacingRight = value; }
        }

        // Use this for initialization
        void Start()
        {
            _playerMovement = FindObjectOfType<Player_Movement>();
            _transform = GetComponent<Transform>();
            _enemyController = GetComponent<Fenrir_Controller>();
            _animator = GetComponent<Animator>();
            _instance = this;
            _wallCheck = GetComponentInChildren<Fenrir_WallCheck>();
            _rigidBody2D = GetComponent<Rigidbody2D>();
            _enemyAttack = GetComponentInChildren<Fenrir_Attack>();
            _player = _playerMovement.GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            _enemyController.Instance.InAttackRange = false;

            //if (_knockbackTimer <= 0 && _stunTimer <= 0)
            if (_stunTimer <= 0)
                CheckStates();

            RunTimers();

        }

        private void RunTimers()
        {
            if (_knockbackTimer > 0)
                _knockbackTimer -= Time.deltaTime;
            if (_stunTimer > 0)
            {
                _animator.SetInteger("animState", 4);
                _stunTimer -= Time.deltaTime;
            }
        }

        public void Stun(float time)
        {
            _stunTimer = time;
        }

        private void CheckStates()
        {
            if (_enemyController.Instance.IsPassive)
            {
                PassiveMove();
            }
            else if (_enemyController.Instance.IsAggressive)
            {
                AggressiveMove();
            }
            else if (_enemyController.Instance.IsSearchingForPlayer)
            {
                MoveToLastSeenPos();
            }
        }

        private void PassiveMove()
        {
            if (_isIdle)
            {
                if (_actionTimer <= 0)
                {
                    float random = Random.Range(0, 101);

                    if (random <= 20)
                    {
                        if (!_isFacingRight)
                        {
                            ChangeDirection();
                        }

                        _animator.SetInteger("animState", 1);
                        _isIdle = false;
                        _isFacingRight = true;
                        _movingTimer = Random.Range(1, 4);
                    }
                    else if (random <= 40)
                    {
                        if (_isFacingRight)
                        {
                            ChangeDirection();
                        }

                        _animator.SetInteger("animState", 1);
                        _isIdle = false;
                        _isFacingRight = false;
                        _movingTimer = Random.Range(1, 4);
                    }
                    else
                    {
                        _animator.SetInteger("animState", 0);
                        _isIdle = true;
                    }

                    _actionTimer = _timeBetweenActions;
                }

                _actionTimer -= Time.deltaTime;
            }
            else
            {

                if (_isFacingRight)
                {
                    if (_wallCheck.CheckRight())
                    {
                        ChangeDirection();
                    }
                    _transform.position += new Vector3(_passiveMovementSpeed * Time.deltaTime, 0, 0);
                    _movingTimer -= Time.deltaTime;
                }
                else
                {
                    if (_wallCheck.CheckLeft())
                    {
                        ChangeDirection();
                    }
                    _transform.position -= new Vector3(_passiveMovementSpeed * Time.deltaTime, 0, 0);
                    _movingTimer -= Time.deltaTime;
                }

                if (_movingTimer <= 0)
                {
                    _animator.SetInteger("animState", 0);
                    _isIdle = true;
                }


            }
        }

        private void AggressiveMove()
        {
            if (!_enemyAttack.SpecialAttacking)
            {
                _enemyController.Instance.InAttackRange = false;

                var Distance = _transform.position.x - _player.position.x;

                if (Distance >= _minDistanceFromPlayer && !_wallCheck.CheckLeft() || Distance <= -1 * _minDistanceFromPlayer && !_wallCheck.CheckRight())
                {

                    _animator.SetInteger("animState", 1);

                    if (!_enemyAttack.Attacking)
                    {
                        _transform.position = Vector2.MoveTowards(_transform.position, new Vector2(_player.position.x, _transform.position.y), Time.deltaTime * _aggressiveMovementSpeed);
                    }

                }
                else if (Distance <= _minDistanceFromPlayer && Distance > 0 || Distance >= -1 * _minDistanceFromPlayer && Distance <= 0)
                {
                    _enemyController.Instance.InAttackRange = true;

                    if (!_enemyAttack.Attacking)
                    {
                        _animator.SetInteger("animState", 0);

                    }

                }
                else if (Distance >= _minDistanceFromPlayer && _wallCheck.CheckLeft() || Distance <= -1 * _minDistanceFromPlayer && _wallCheck.CheckRight())
                {
                    _animator.SetInteger("animState", 0);
                }


                if (_transform.position.x < _player.transform.position.x && !_isFacingRight)
                {
                    ChangeDirection();
                }
                else if (_transform.position.x > _player.transform.position.x && _isFacingRight)
                {
                    ChangeDirection();
                }
            }
        }

        private void MoveToLastSeenPos()
        {
            var Distance = _transform.position.x - _enemyController.LastSeenPlayerPos.x;

            if (Distance >= _minDistanceFromPlayer && !_wallCheck.CheckLeft() || Distance <= -1 * _minDistanceFromPlayer && !_wallCheck.CheckRight())
            {
                if (Distance >= _minDistanceFromPlayer && _isFacingRight)
                {
                    ChangeDirection();
                }
                else if (Distance <= -1 * _minDistanceFromPlayer && !_isFacingRight)
                {
                    ChangeDirection();
                }

                _animator.SetInteger("animState", 1);
                _transform.position = Vector2.MoveTowards(_transform.position, new Vector2(_enemyController.Instance.LastSeenPlayerPos.x, _transform.position.y), Time.deltaTime * _aggressiveMovementSpeed);
            }
            else
            {
                _animator.SetInteger("animState", 0);
            }
        }

        public void ChangeDirection()
        {
            if (!_enemyAttack.Attacking)
            {
                _isFacingRight = !_isFacingRight;

                _transform.localScale = new Vector3(-1 * _transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
            }
        }

        public void Knockback(float horizontalForce, float verticalForce)
        {

            if (!_isFacingRight && _knockbackTimer <= 0)
            {
                _rigidBody2D.AddForce(_transform.right * horizontalForce);
                _rigidBody2D.AddForce(_transform.up * verticalForce);
                _knockbackTimer = 0.5f;
            }
            else if (_knockbackTimer <= 0)
            {
                _rigidBody2D.AddForce(_transform.right * -horizontalForce);
                _rigidBody2D.AddForce(_transform.up * verticalForce);
                _knockbackTimer = 0.5f;
            }
        }

    }
}