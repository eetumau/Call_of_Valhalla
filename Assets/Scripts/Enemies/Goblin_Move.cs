using UnityEngine;
using System.Collections;

public class Goblin_Move : MonoBehaviour {

    [SerializeField]
    private float movementSpeed;
    private Transform _transform;
    private Rigidbody2D _rigidBody;

	// Use this for initialization
	void Start () {

        _transform = GetComponent<Transform>();
        _rigidBody = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MoveToPlayer(Vector3 playerPos)
    {
        Vector3 moveTo = playerPos - _transform.position;

        _transform.position += new Vector3(Mathf.Lerp(_transform.position.x, playerPos.x, Time.deltaTime * _), _transform.position.y, _transform.position.z);
    }
}
