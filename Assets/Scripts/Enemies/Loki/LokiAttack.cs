using UnityEngine;
using System.Collections.Generic;
using System;

public class LokiAttack : MonoBehaviour {

    [SerializeField]
    private GameObject _projectile;
    [SerializeField]
    private Transform _projectileStartingPoint;

    private LokiAnimationController _animator;
    private bool _normalAttack;
    private bool _stopAttack;
    private bool _attackOnce;

    private int _pooledProjectiles = 20;
    private int _arrayIndex;

    private float _shootingTime;
    
    public List<GameObject> _projectiles;

    // Use this for initialization
    void Awake() {

        _projectiles = new List<GameObject>();
        _animator = GetComponent<LokiAnimationController>();
        SetupProjectiles();
	}

    private void SetupProjectiles()
    {
        for (int i = 0; i < _pooledProjectiles; i++)
        {
            GameObject tmpGO = Instantiate(_projectile, _projectileStartingPoint.position, Quaternion.identity) as GameObject;
            tmpGO.SetActive(false);
            _projectiles.Add(tmpGO);
        }

    }

    // Update is called once per frame
    void Update () {
	    

	}

    public void Attack()
    {

        StartCoroutine(NextShot(5));
        if (_normalAttack)
        {

        }
        else
        {

        }
    }

    public void SetNormalAttack(bool state)
    {
        _normalAttack = state;
        StartCoroutine(NextShot(0f));
    }

    public void SetStopAttack(bool state)
    {
        _stopAttack = state;
        StopAllCoroutines();
    }

    /*
    public System.Collections.IEnumerator FireDelay(float howLong)
    {
        yield return new WaitForSeconds(howLong);
        Fire(6);
    }
    */

    public System.Collections.IEnumerator NextShot(float howLong)
    {
        yield return new WaitForSeconds(howLong);        
        _animator.SetAttackAnimation();
        StartCoroutine(NextShot(3));
        
    }


    public void Fire(int speed)
    {
        for (int i = 0; i < _projectiles.Count; i++)
        {
            if (!_projectiles[i].activeInHierarchy)
            {
                _projectiles[i].SetActive(true);
                break;
            }
        }
    }
}
