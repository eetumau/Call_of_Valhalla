﻿using UnityEngine;
using System.Collections.Generic;
using System;

public class LokiAttack : MonoBehaviour {

    [SerializeField]
    private GameObject _projectile;

    [SerializeField]
    private Transform _projectileStartingPoint;

    private bool _normalAttack;
    private bool _stopAttack;
    private bool _attackOnce;

    private int _pooledProjectiles = 20;
    private int _arrayIndex;
    
    
    public List<GameObject> _projectiles;

    // Use this for initialization
    void Awake() {

        _projectiles = new List<GameObject>();
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

        StartCoroutine(FireDelay(5));
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
    }

    public void SetStopAttack(bool state)
    {
        _stopAttack = state;
    }

    public System.Collections.IEnumerator FireDelay(float howLong)
    {
        yield return new WaitForSeconds(howLong);
        Fire(6);
        StartCoroutine(FireDelay(5));
    }


    public void Fire(int speed)
    {
        for (int i = 0; i < _projectiles.Count; i++)
        {
            if (!_projectiles[i].activeInHierarchy)
            {
                Debug.Log("Fire");
                _projectiles[i].SetActive(true);
                break;
            }
        }
    }
}
