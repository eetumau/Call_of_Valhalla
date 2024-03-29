﻿using UnityEngine;
using System.Collections;
using CallOfValhalla;

public class LokiMovement : MonoBehaviour {

    private Transform _lokiTransform;
    private Transform _pointToMove;
    private Rigidbody2D _rigidBody;
    private Transform _currentMovePoint;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private GameObject _pointGameObject1;
    [SerializeField]
    private GameObject _pointGameObject2;
    [SerializeField]
    private GameObject _pointGameObject3;
    [SerializeField]
    private GameObject _pointGameObject4;
	[SerializeField]
	private GameObject _pointGameObject5;
    [SerializeField]
    private GameObject _stunPointGameObject;


    private Transform _firstMovePoint;
    private Transform _secondMovePoint;
    private Transform _thirdMovePoint;
    private Transform _fourthMovePoint;
	private Transform _fifthMovePoint;
    private Transform _stunPoint;

    [SerializeField]
    private GameObject[] _teleportGameObjects;

    private Transform _playerTransform;

    private bool _moving;
    [HideInInspector]
    public bool _teleporting;
    private LokiAnimationController _animator;
    private AudioSource _source;

    // Use this for initialization
    void Awake () {

        _lokiTransform = GetComponent<Transform>();
        _animator = GetComponent<LokiAnimationController>();
		_currentMovePoint = _lokiTransform;

        _firstMovePoint = _pointGameObject1.GetComponent<Transform>();
        _secondMovePoint = _pointGameObject2.GetComponent<Transform>();
        _thirdMovePoint = _pointGameObject3.GetComponent<Transform>();
        _fourthMovePoint = _pointGameObject4.GetComponent<Transform>();
        _fifthMovePoint = _pointGameObject5.GetComponent<Transform>();
        _stunPoint = _stunPointGameObject.GetComponent<Transform>();


        _source = GetComponent<AudioSource>();
        _playerTransform = GameObject.Find("HeroSword_0").GetComponent<Transform>();

    }
	
	// Update is called once per frame
	void Update () {

        if (_moving)
            Move();
        
        
	}

    public void StartMovement()
    {
        _animator.SetIdleAnimation();
        Transform tmpPoint = GetRandomMovePoint();
        StartCoroutine(Movement(0, tmpPoint));
    }

    // Sets up a new movement sequence
    public void SetMovement()
    {
        //float tmpTime = GetRandomMoveTime();
        _animator.SetIdleAnimation();
        Transform tmpPoint = GetRandomMovePoint();
        StartCoroutine(Movement(1, tmpPoint));
    }

    private IEnumerator Movement(float time, Transform tf)
    {
        yield return new WaitForSeconds(time);
        MoveToPoint(tf);
        
    }

    public void MoveToPoint(Transform tf)
    {
        _moving = true;
        _pointToMove = tf;
    }

    // Moves Loki to the destination
    private void Move()
    {
        if (_lokiTransform.position.x < _pointToMove.position.x && _moving)
        {
            _lokiTransform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_moving)
        {
            _lokiTransform.localScale = new Vector3(1, 1, 1);
        }

        float step = _speed * Time.deltaTime;
        _lokiTransform.position = Vector3.MoveTowards(_lokiTransform.position, _pointToMove.position, step);

        if (_lokiTransform.position == _pointToMove.position)
        {
            _moving = false;
            FaceThePlayer();
            SetMovement();
        }

    }

    private Vector3 GetRandomTeleportPoint()
    {
        float tmp = _teleportGameObjects.Length;
        int index = (int)Random.Range(0f, tmp);

        Vector3 tmpVector3 = _teleportGameObjects[index].transform.position;
        return tmpVector3;
    }

    private void FaceThePlayer()
    {
        if (_lokiTransform.position.x < _playerTransform.position.x)
        {
            _lokiTransform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            _lokiTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    // Returns random time for the movement timer
    private float GetRandomMoveTime()
    {
        return Random.Range(3f, 5f);
    }

    // Returns random movepoint which is not the current movepoint
    private Transform GetRandomMovePoint()
    {
		
		Transform tmptf = _currentMovePoint;
		while (true)
        {
			int tmp = (int)Random.Range(0f, 5f) + 1;

            if (tmp == 1)
            {
                if (_firstMovePoint != _currentMovePoint)
                {
                    tmptf = _firstMovePoint;
                    break;
                }
            }
            else if (tmp == 2)
            {
                if (_secondMovePoint != _currentMovePoint)
                {
                    tmptf = _secondMovePoint;
                    break;
                }
            }
            else if (tmp == 3)
            {
                if (_thirdMovePoint != _currentMovePoint)
                {
                    tmptf = _thirdMovePoint;
                    break;
                }
            }
			else if (tmp == 4)
			{
				if (_fourthMovePoint != _currentMovePoint)
				{
					tmptf = _fourthMovePoint;
					break;
				}
			}
            else
            {
                if (_fifthMovePoint != _currentMovePoint)
                {
                    tmptf = _fifthMovePoint;
                    break;
                }
            }
			
        }

        _currentMovePoint = tmptf;
        return tmptf;
    }

    // Stops movement and all coroutines
    public void StopAllMovementSequences()
    {
        _moving = false;
        StopAllCoroutines();
        Debug.Log("STOPALLMOVEMENT");
    }

    // Starts a flying movement sequence immediatly
    public void StartFlyingMovement()
    {
        Transform tmpPoint = GetRandomMovePoint();
        StartCoroutine(Movement(0f, tmpPoint));
    }

    public void StartTeleportingSequence()
    {
        StopAllMovementSequences();
        _teleporting = true;
        _animator.SetTeleportAnimation();
    }

    public void Teleport()
    {
        transform.position = GetRandomTeleportPoint();
        FaceThePlayer();
        if (!_teleporting)
        {
            StopAllMovementSequences();
            transform.position = _stunPoint.position;
            _animator.SetStunAnimation();
            
        }
    }

    public void Die()
    {
        StopAllMovementSequences();
        _animator.SetDeathAnimation();
    }

    public void PlaySound()
    {
        SoundManager.instance.PlaySound("loki_teleport", _source, false, true);

    }
}
