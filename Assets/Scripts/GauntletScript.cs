using UnityEngine;
using System.Collections;
using CallOfValhalla.Enemy;
using System;

public class GauntletScript : MonoBehaviour {
        
    [SerializeField]
    private GameObject[] _enemies;
    [SerializeField]
    private GameObject[] _objects;

    private ArrayList _hp;
    private bool _gauntletActive;
    private float _enemiesLeft;

	// Use this for initialization
	private void Awake () {        

        CheckLists();
	    
	}

    private void CheckLists()
    {
        _hp = new ArrayList();        

        foreach (GameObject enemy in _enemies)
        {
            Enemy_HP tmp = enemy.GetComponent<Enemy_HP>();
            _hp.Add(tmp);
        }

        foreach (GameObject ob in _objects)
        {
            ob.SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update () {
	    
        if (_gauntletActive)
        {
            CheckEnemiesLeft();

            if (_enemiesLeft == 0)
            {
                DeactivateGauntlet();
            }
            _enemiesLeft = 0;
        }
	}

    private void DeactivateGauntlet()
    {
        _gauntletActive = false;

        foreach (GameObject ob in _objects)
        {
            ob.SetActive(false);
        }
    }

    private void CheckEnemiesLeft()
    {
        foreach (Enemy_HP hp in _hp)
        {

            if (hp.HP > 0)
            {
                _enemiesLeft += 1;
            }
        }
    }

    // Activates the objects relating to the gauntlet and starts checking remaining enemies
    public void ActivateGauntlet()
    {
        _gauntletActive = true;

        foreach (GameObject ob in _objects)
        {
            ob.SetActive(true);
        }
    }
}
