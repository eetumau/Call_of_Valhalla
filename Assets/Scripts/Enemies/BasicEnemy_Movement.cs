using UnityEngine;
using System.Collections;

public class BasicEnemy_Movement : MonoBehaviour {

    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private float _minDistance;
    private Transform _transform;
    private Rigidbody2D _rigidBody;
    private bool _movingRight;
    private bool _idle;
    private bool _holdPosition;
    private SpriteRenderer _spriteRenderer;
    private BasicEnemy_WallCheck _wallCheck;

    public bool HoldPosition
    {
        get { return _holdPosition; }
        set { _holdPosition = value; }
    }

    // Use this for initialization
    void Start()
    {
        var allButIgnoreLinecast = ~(1 << 8);
        Physics2D.IgnoreLayerCollision(8, 8);

        _transform = GetComponent<Transform>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _wallCheck = GetComponentInChildren<BasicEnemy_WallCheck>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveToPlayer(Vector3 playerPos)
    {
        Vector3 moveTo = playerPos - _transform.position;

        _transform.position += new Vector3(Mathf.Lerp(_transform.position.x, moveTo.x, Time.deltaTime * 0.5f), _transform.position.y, _transform.position.z);
    }

    public void PassiveMovement()
    {

        if (_movingRight)
        {
            _transform.position += new Vector3(_movementSpeed * Time.deltaTime, 0, 0);
        }
        else if (!_movingRight)
        {
            _transform.position -= new Vector3(_movementSpeed * Time.deltaTime, 0, 0);
        }
    }

    public void AggressiveMovement(Vector3 targetPosition)
    {
        Vector3 targetPos = new Vector3(targetPosition.x, _transform.position.y, _transform.position.z);
        Vector3 distance = _transform.position - targetPosition;
        
        if(distance.x < 0 && !_movingRight)
        {
            ChangeDirection();
        }else if(distance.x > 0 && _movingRight)
        {
            ChangeDirection();
        }

        if(_minDistance <= distance.magnitude)
        {
            _transform.position = Vector3.Lerp(_transform.position, targetPos, Time.deltaTime * 0.5f * _movementSpeed);
        }
    }


    public void ChangeDirection()
    {
        _movingRight = !_movingRight;

        if (_movingRight)
        {
            _transform.rotation = Quaternion.Euler(_transform.rotation.x, -180, _transform.rotation.z);
        }else
        {
            _transform.rotation = Quaternion.Euler(_transform.rotation.x, 0, _transform.rotation.z);
        }

        //Debug.Log("ENEMY CHANGING DIRECTION");
     
    }

    public void RandomDirection()
    {

        if(_wallCheck.CheckRight() || _wallCheck.CheckLeft())
        {
            if (_wallCheck.CheckRight())
            {
                _movingRight = false;
                ChangeDirection();
            }
            else if (_wallCheck.CheckLeft())
            {
                _movingRight = true;
                ChangeDirection();
            }
        }else
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                if (!_movingRight)
                {
                    ChangeDirection();
                }
                _movingRight = true;
            }
            else
            {
                if (_movingRight)
                {
                    ChangeDirection();
                }
                _movingRight = false;
            }
        }
   
    }
}
