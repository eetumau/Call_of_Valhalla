using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy
{
    public class Enemy_SearchForPlayer : MonoBehaviour
    {

        [SerializeField]
        private float _aggroRadius;
        [SerializeField]
        private Transform _lineCastStartingPoint;
        [SerializeField]
        private Transform _lineCastEndingPoint;
        [SerializeField]
        private float _timeBeforeTurningPassive;

        private Transform _transform;
        private Enemy_Controller _enemyController;
        private float _turnToPassiveTimer;
        private Vector3 _playerLastSeenPos;
        private Animator _animator;
        private int allButIgnoreLinecast = ~(1 << 8);



        // Use this for initialization
        void Start()
        {
            _animator = GetComponent<Animator>();
            _transform = GetComponent<Transform>();
            _enemyController = GetComponent<Enemy_Controller>();
            _turnToPassiveTimer = _timeBeforeTurningPassive;
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
                        _enemyController.Instance.TurnToAggressive();

                        _turnToPassiveTimer = _timeBeforeTurningPassive;
                        _enemyController.Instance.LastSeenPlayerPos = colliders[i].gameObject.transform.position;
                    }
                    else
                    {
                        if (_enemyController.Instance.IsAggressive)
                        {

                            _enemyController.Instance.TurnToSearchingForPlayer();

                        }
                    }

                    var distance = _lineCastStartingPoint.position - _lineCastEndingPoint.position;
                    if(distance.magnitude >= _aggroRadius)
                    {
                        if (_enemyController.Instance.IsAggressive)
                        {
                            _enemyController.Instance.TurnToSearchingForPlayer();
                        }
                    }
                }
            }

            if (_enemyController.Instance.IsSearchingForPlayer)
            {

                if (_turnToPassiveTimer <= 0)
                {

                    _enemyController.Instance.TurnToPassive();
                    _turnToPassiveTimer = _timeBeforeTurningPassive;
                }

                _turnToPassiveTimer -= Time.deltaTime;

            }
        }


    }
}