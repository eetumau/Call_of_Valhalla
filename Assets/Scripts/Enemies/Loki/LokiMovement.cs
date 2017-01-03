using UnityEngine;
using System.Collections;

public class LokiMovement : MonoBehaviour {

    private Transform _lokiTransform;
    private Transform _pointToMove;
    private Rigidbody2D _rigidBody;
    [SerializeField] private float _speed;

    private bool _moving;

	// Use this for initialization
	void Awake () {

        _lokiTransform = GetComponent<Transform>();
        
	}
	
	// Update is called once per frame
	void Update () {

        if (_moving)
            Move();
        
	}

    public void MoveToPoint(Transform tf)
    {
        _moving = true;
        _pointToMove = tf;
    }

    private void Move()
    {

        float step = _speed * Time.deltaTime;
        _lokiTransform.position = Vector3.MoveTowards(_lokiTransform.position, _pointToMove.position, step);

        if (_lokiTransform.position == _pointToMove.position)
        {
            _moving = false;
        }
    }
}
