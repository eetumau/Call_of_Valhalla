using UnityEngine;
using System.Collections;

public class BasicEnemy_Movement : MonoBehaviour {

    [SerializeField]
    private float _movementSpeed;
    private Transform _transform;
    private Rigidbody2D _rigidBody;

    private bool _idle;

    // Use this for initialization
    void Start()
    {

        _transform = GetComponent<Transform>();
        _rigidBody = GetComponent<Rigidbody2D>();

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

    public void PassiveMovement(bool moveRight)
    {

        if (moveRight)
        {
            _transform.position += new Vector3(_movementSpeed * Time.deltaTime, 0, 0);
        }
        else if (!moveRight)
        {
            _transform.position -= new Vector3(_movementSpeed * Time.deltaTime, 0, 0);
        }


    }

    private void OnCollisionEnter2D(Collision2D col)
    {



    }
}
