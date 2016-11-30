using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;

namespace CallOfValhalla.Enemy
{
    public class Enemy_SearchForPlayer : MonoBehaviour
    {

        [SerializeField]
        private float _aggroRadius;
        [SerializeField]
        private Transform _lineCastStartingPoint;
        [SerializeField]
        private float _timeBeforeTurningPassive;

        private Transform _transform;
        private Enemy_Controller _enemyController;
        private float _turnToPassiveTimer;
        private Vector3 _playerLastSeenPos;
        private Animator _animator;
        private int allButIgnoreLinecast = ~(1 << 8);
        private GameObject _lineCastObject;
        private Transform _lineCastEndingPoint;



        // Use this for initialization
        void Start()
        {
            _lineCastObject = GameObject.Find("LineCastEndingPoint");

            _animator = GetComponent<Animator>();
            _transform = GetComponent<Transform>();
            _enemyController = GetComponent<Enemy_Controller>();
            _turnToPassiveTimer = _timeBeforeTurningPassive;
            _lineCastEndingPoint = _lineCastObject.GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            SearchForPlayer();
        }

        private void SearchForPlayer()
        {
            var colliders = Physics2D.OverlapCircleAll(_transform.position, _aggroRadius);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.tag == "Player")
                {

                    if (!Physics2D.Linecast(new Vector2(_lineCastStartingPoint.position.x, _lineCastStartingPoint.position.y), new Vector2(_lineCastEndingPoint.position.x, _lineCastEndingPoint.position.y), allButIgnoreLinecast))
                    {
                        Debug.DrawLine(new Vector2(_lineCastStartingPoint.position.x, _lineCastStartingPoint.position.y), new Vector2(_lineCastEndingPoint.position.x, _lineCastEndingPoint.position.y));

                        if(_enemyController.IsPassive || _enemyController.IsSearchingForPlayer)
                        {
                            _enemyController.TurnToAggressive();

                        }

                        _turnToPassiveTimer = _timeBeforeTurningPassive;
                        _enemyController.LastSeenPlayerPos = colliders[i].gameObject.transform.position;
                    }
                    else
                    {
                        if (_enemyController.IsAggressive)
                        {

                            _enemyController.TurnToSearchingForPlayer();

                        }
                    }

                    var distance = _lineCastStartingPoint.position - _lineCastEndingPoint.position;
                    if(distance.magnitude >= _aggroRadius)
                    {
                        if (_enemyController.IsAggressive)
                        {
                            _enemyController.TurnToSearchingForPlayer();
                        }
                    }
                }
            }

            if (_enemyController.IsSearchingForPlayer)
            {

                if (_turnToPassiveTimer <= 0)
                {

                    _enemyController.TurnToPassive();
                    _turnToPassiveTimer = _timeBeforeTurningPassive;
                }

                _turnToPassiveTimer -= Time.deltaTime;

            }
        }


    }
}