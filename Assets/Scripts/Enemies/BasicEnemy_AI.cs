using UnityEngine;
using System.Collections;

public class BasicEnemy_AI : MonoBehaviour {

    [SerializeField]
    private float _aggroRadius;
    [SerializeField]
    private float _timeBeforeNextAction;
    [SerializeField]
    private float _timeBeforePassive;
    [SerializeField]
    private Vector3 _linecastOffset;
    


    private BasicEnemy_Movement _enemyMovement;

    private Transform _transform;
    private bool _playerOnSight = false;
    private bool _passive = false;
    private bool _aggressive = false;
    private bool _idle = false;
    private bool _moveRight = false;
    private float _actionTimer;
    private float _passiveTimer;
    private Vector3 _playerPosition;
    private Vector3 _lastSeenPosition;
    private BoxCollider2D _boxCollider;

    public static BasicEnemy_AI Instance;

    public bool Aggressive { get { return _aggressive; } }

    // Use this for initialization
    void Start()
    {
        Instance = this;

        _passive = true;

        //_transform = GetComponent<Transform>();

        _enemyMovement = GetComponent<BasicEnemy_Movement>();

        _transform = GetComponent<Transform>();

        _actionTimer = _timeBeforeNextAction;


    }

    // Update is called once per frame
    void Update()
    {

        if (_passive)
        {
            PassiveState();
        }else if (_aggressive)
        {
            AggressiveState();
        }



    }

    private void FixedUpdate()
    {


        SearchForPlayer();

    }


    /*SearchForPlayer creates an OverlapCircle around the gameobject and searches for other gameobjects with a tag "Player".
     * If such an object is found it shoots a linecast to it
    */
    private void SearchForPlayer()
    {
        var allButIgnoreLinecast = ~(1 << 8);

        var colliders = Physics2D.OverlapCircleAll(_transform.position, _aggroRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Player")
            {

                if (!Physics2D.Linecast(_transform.position + _linecastOffset, colliders[i].transform.position + colliders[i].transform.up, allButIgnoreLinecast))
                {
                    Debug.DrawLine(_transform.position + _linecastOffset, colliders[i].transform.position + colliders[i].transform.up);

                    _passive = false;
                    _aggressive = true;
                    _playerOnSight = true;

                }else
                {
                    _playerOnSight = false;
                    _lastSeenPosition = new Vector3(colliders[i].transform.position.x, _transform.position.y, _transform.position.z);
                }

                _playerPosition = colliders[i].gameObject.transform.position;
            }
        }


    }


    /*When _idleTimer reaches 0, randoms the next action (either move or stay idle) then randoms a direction for movement and calls
     * the PassiveMovement method in Goblin_Move if need be.
     */
    private void PassiveState()
    {
        if (_actionTimer <= 0)
        {
            int random = Random.Range(0, 4);

            if (random == 0)
            {
                _enemyMovement.RandomDirection();

                _idle = false;
                _actionTimer = _timeBeforeNextAction;
            }
            else
            {
                _idle = true;
                _actionTimer = _timeBeforeNextAction;
            }
        }

        if (!_idle)
        {
            _enemyMovement.PassiveMovement();
        }

        _actionTimer -= Time.deltaTime;
    }

    private void AggressiveState()
    {
        if (_playerOnSight)
        {
            _enemyMovement.AggressiveMovement(_playerPosition);
        }
        else
        {
            var allButIgnoreLinecast = ~(1 << 8);

            if (!Physics2D.Raycast(_transform.position, _transform.right, 1, allButIgnoreLinecast) &&
                !Physics2D.Raycast(_transform.position, -_transform.right, 1, allButIgnoreLinecast))
            {
                _enemyMovement.AggressiveMovement(_lastSeenPosition);
            }
            else
            {
                _passiveTimer -= Time.deltaTime;
                
                if(_passiveTimer <= 0)
                {
                    _aggressive = false;
                    _passive = true;
                    _passiveTimer = _timeBeforePassive;
                }
            }
        }
    }


}