using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy
{
    public class Surt_Movement : MonoBehaviour
    {

        [SerializeField]
        private float _movementSpeed;
        [SerializeField]
        private Transform _playerTransform;
        [SerializeField]
        private float _minDistanceFromPlayer;

        private Surt_AnimationController _surtAC;
        private Surt_Attack _attack;

        private Transform _transform;
        private Rigidbody2D _rb;
        private bool _playerOnSight;
        private bool _inRange;
        private bool _isFacingRight;
        private bool _moving;
        private float _stunTimer;
        private bool _specialMoving;



        public bool PlayerOnSight
        {
            get { return _playerOnSight; }
            set { _playerOnSight = value; }
        }

        public bool InRange
        {
            get { return _inRange; }
        }

        public bool Moving
        {
            get { return _moving; }
        }

        public bool IsFacingRight
        {
            get { return _isFacingRight; }
            set { _isFacingRight = value; }
        }

        public float StunTimer
        {
            get { return _stunTimer; }
        }

        public bool SpecialMoving
        {
            set { _specialMoving = value; }
        }
        // Use this for initialization
        void Start()
        {
            _surtAC = GetComponent<Surt_AnimationController>();
            _transform = GetComponent<Transform>();
            _rb = GetComponent<Rigidbody2D>();
            _attack = GetComponentInChildren<Surt_Attack>();
        }

        // Update is called once per frame
        void Update()
        {

            if (_stunTimer > 0)
            {
                RunStunTimer();
            }
            else
            {
                if (!_specialMoving)
                {
                    Move();
                }
            }

        }

        private void Move()
        {
            _inRange = CheckDistanceFromPlayer();

            if (_playerOnSight && !_attack.Attacking)
            {
                CheckDirection();

                if (!_inRange)
                {
                    _moving = true;
                    _surtAC.SetAnimation(1);
                    _transform.position = Vector2.MoveTowards(_transform.position, new Vector2(_playerTransform.position.x, _transform.position.y), _movementSpeed * Time.deltaTime);
                }else
                {
                    _surtAC.SetAnimation(0);
                    _moving = false;
                }
            }
        }

        private bool CheckDistanceFromPlayer()
        {
            Vector2 _distance = _transform.position - _playerTransform.position;

            if(_distance.magnitude < _minDistanceFromPlayer)
            {
                return true;
            }else
            {
                return false;
            }
        }

        public void CheckDirection()
        {
           if(_isFacingRight && _transform.position.x > _playerTransform.position.x)
            {
                ChangeDirection();
            }else if(!_isFacingRight && _transform.position.x <= _playerTransform.position.x)
            {
                ChangeDirection();
            }
        }

        private void ChangeDirection()
        {
            _isFacingRight = !_isFacingRight;

            _transform.localScale = new Vector2(-1 * _transform.localScale.x, _transform.localScale.y);
        }

        public void Stun(float duration)
        {
            if (!_attack.SpecialAttacking)
            {
                _stunTimer = duration;
                _attack.DisableAttackHitBox();
                _attack.DisableAttacking();
                _surtAC.SetAnimation(4);
                _moving = false;
            }
      
        }

        private void RunStunTimer()
        {
            _stunTimer -= Time.deltaTime;

        }
    }
}