using UnityEngine;
using System.Collections;
using CallOfValhalla.Enemy;
using System;
using CallOfValhalla;
using CallOfValhalla.States;

public class GauntletScript : MonoBehaviour {
        
    [SerializeField]
    private GameObject[] _enemies;
    [SerializeField]
    private GameObject[] _objects;

	public GameObject _door;
	private Animator animator;

    private ArrayList _hp;
    private bool _gauntletActive;
    private float _enemiesLeft;
    private GauntletTrigger _gauntletTrigger;

	// Use this for initialization
	private void Awake () {
        _gauntletTrigger = GetComponentInChildren<GauntletTrigger>();

        if (_door != null)
		    animator = _door.GetComponent<Animator> ();

        CheckLists();
	    
	}

    private void CheckLists()
    {
        _hp = new ArrayList();        

        foreach (GameObject enemy in _enemies)
        {
            Enemy_HP tmp = enemy.GetComponent<Enemy_HP>();

            if(tmp == null)
            {
                Fenrir_HP fenrirHP = enemy.GetComponent<Fenrir_HP>();
                _hp.Add(fenrirHP);

            }else
            {
                _hp.Add(tmp);

            }

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
                if (_door != null)
                    animator.SetBool ("Door_Open", true);
            }
            _enemiesLeft = 0;

            if(GameManager.Instance.Player.HP._hp <= 0)
            {
                ResetMusic();
                _gauntletTrigger.Trigger.SetActive(true);
            }
        }
	}

    private void DeactivateGauntlet()
    {
        _gauntletActive = false;

        foreach (GameObject ob in _objects)
        {
            ob.SetActive(false);
        }

        ResetMusic();
    }

    private void CheckEnemiesLeft()
    {
        if (GameManager.Instance.Level != 9)
        {
            foreach (Enemy_HP hp in _hp)
            {

                if (hp.HP > 0)
                {
                    _enemiesLeft += 1;
                }
            }
        }else
        {
            foreach(Fenrir_HP hp in _hp)
            {
                if(hp.HP > 0)
                {
                    _enemiesLeft += 1;
                }
            }
        }
    }

    // Activates the objects relating to the gauntlet and starts checking remaining enemies
    public void ActivateGauntlet()
    {
        if (!_gauntletActive)
        {
            _gauntletActive = true;

            foreach (GameObject ob in _objects)
            {
                ob.SetActive(true);
            }
        }
       
    }

    private void ResetMusic()
    {
        if (GameManager.StateManager.CurrentStateType == CallOfValhalla.States.StateType.MainMenu)
        {
            SoundManager.instance.SetMusic("menu_music_1");
        }
        else if(GameManager.Instance.Level < 4)
        {
            SoundManager.instance.SetMusic("level_music_4");
        }else if(GameManager.Instance.Level > 3 && GameManager.Instance.Level < 10)
        {
            SoundManager.instance.SetMusic("level_music_5");
        }else
        {
            SoundManager.instance.SetMusic("level_music_3");
        }
    }

}
