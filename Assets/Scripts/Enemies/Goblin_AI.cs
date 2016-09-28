using UnityEngine;
using System.Collections;

public class Goblin_AI : MonoBehaviour
{

    [SerializeField]
    private float _aggroRadius;
    [SerializeField]
    private Vector2 _playerLookAt;
    [SerializeField]
    private float _nextIdleActionTime;


    private Goblin_Move Goblin_Move;

    private Transform _transform;
    private bool _aggro = false;
    private bool _idle = true;
    private bool _moveRight = false;
    private float _idleTimer;

    // Use this for initialization
    void Start()
    {

        _transform = GetComponent<Transform>();

        Goblin_Move = GetComponent<Goblin_Move>();

        _idleTimer = _nextIdleActionTime;


    }

    // Update is called once per frame
    void Update()
    {
        PassiveState();

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
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        _aggro = false;

        var colliders = Physics2D.OverlapCircleAll(_transform.position, _aggroRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Player")
            {


                if (!Physics2D.Linecast(_transform.position + transform.up, colliders[i].transform.position, layerMask))
                {
                    Debug.DrawLine(_transform.position + transform.up, colliders[i].transform.position + colliders[i].transform.up);

                    _aggro = true;
                }


            }
        }
    }


    /*When _idleTimer reaches 0, randoms the next action (either move or stay idle) then randoms a direction for movement and calls
     * the PassiveMovement method in Goblin_Move if need be.
     */
    private void PassiveState()
    {
        if (_idleTimer <= 0)
        {
            int random = Random.Range(0, 3);

            if (random == 0)
            {
                random = Random.Range(0, 2);

                if (random == 0)
                {
                    _moveRight = true;
                }

                else
                {
                    _moveRight = false;
                }

                _idle = false;
                _idleTimer = _nextIdleActionTime;
            }
            else
            {
                _idle = true;
                _idleTimer = _nextIdleActionTime;
            }
        }

        if (!_idle)
        {
            Goblin_Move.PassiveMovement(_moveRight);
        }

        _idleTimer -= Time.deltaTime;
    }





}
