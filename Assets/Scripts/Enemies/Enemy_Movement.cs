using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy
{
    public class Enemy_Movement : MonoBehaviour
    {

        [SerializeField]
        private float _passiveMovementSpeed;
        [SerializeField]
        private float _aggressiveMovementSpeed;
        [SerializeField]
        private float _timeBetweenActions;
        [SerializeField]
        private Transform _player;
        [SerializeField]
        private float _minDistanceFromPlayer;

        private Transform _transform;
        private bool _isFacingRight;
        private Enemy_Controller _enemyController;
        private float _movingTimer;
        private float _actionTimer;
        private bool _isIdle = true;
        private Animator _animator;


        // Use this for initialization
        void Start()
        {

            _transform = GetComponent<Transform>();
            _enemyController = GetComponent<Enemy_Controller>();
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
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
                    _transform.position += new Vector3(_passiveMovementSpeed * Time.deltaTime, 0, 0);
                    _movingTimer -= Time.deltaTime;
                }
                else
                {
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
            var Distance = _transform.position - _player.position;

            if (Distance.magnitude >= _minDistanceFromPlayer)
            {
                _animator.SetInteger("animState", 1);
                _transform.position = Vector3.Lerp(_transform.position, new Vector3(_player.position.x, _transform.position.y, _transform.position.z), Time.deltaTime);
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

        private void MoveToLastSeenPos()
        {
            var Distance = _transform.position.x - _enemyController.LastSeenPlayerPos.x;

            if (Distance >= 2)
            {
                _animator.SetInteger("animState", 1);
                _transform.position = Vector3.Lerp(_transform.position, new Vector3(_enemyController.LastSeenPlayerPos.x, _transform.position.y, _transform.position.z), Time.deltaTime);
            }
        }

        private void ChangeDirection()
        {
            _isFacingRight = !_isFacingRight;

            _transform.localScale = new Vector3(-1 * _transform.localScale.x, _transform.localScale.y, _transform.localScale.z);

        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "Wall")
            {
                ChangeDirection();
            }
        }
    }
}