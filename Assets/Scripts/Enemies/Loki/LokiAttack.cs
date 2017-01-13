using UnityEngine;
using System.Collections.Generic;
using System;
using CallOfValhalla;

public class LokiAttack : MonoBehaviour {

    [SerializeField]
    private GameObject _projectile;
    [SerializeField]
    private Transform _projectileStartingPoint;

    private LokiAnimationController _animator;
    private AudioSource _source;
    private bool _normalAttack;
    private bool _stopAttack;
    private bool _attackOnce;

    private int _pooledProjectiles = 15;
    private int _arrayIndex;

    private float _shootingTime;
    
    public List<GameObject> _projectiles;

    // Use this for initialization
    void Awake() {

        _projectiles = new List<GameObject>();
        _animator = GetComponent<LokiAnimationController>();
        _source = GetComponent<AudioSource>();
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


    public void Attack()
    {

        StartCoroutine(NextShot(2));

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
        StartCoroutine(NextShot(2.5f));
        
    }


    public void Fire(int speed)
    {
        for (int i = 0; i < _projectiles.Count; i++)
        {
            if (!_projectiles[i].activeInHierarchy)
            {
                _projectiles[i].SetActive(true);
                SoundManager.instance.PlaySound("loki_attack1", _source, false);
                break;
            }
        }
    }
}
