﻿using UnityEngine;
using System.Collections;
using CallOfValhalla.Enemy;
using CallOfValhalla.Player;


public class LokiController : MonoBehaviour {

    private LokiMovement _lokiMovement;
    private LokiAttack _lokiAttack;
    private LokiAnimationController _animationController;
    private Enemy_HP _hp;
    private Player_HP _playerHP;
    private bool _dead;

    [SerializeField]
    private GameObject[] _gameObjects;

    // Use this for initialization
    void Awake () {

        _lokiMovement = GetComponent<LokiMovement>();
        _lokiAttack = GetComponent<LokiAttack>();
        _hp = GetComponent<Enemy_HP>();
        _playerHP = FindObjectOfType<Player_HP>();
        _animationController = GetComponent<LokiAnimationController>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (_hp.hitPoints <= 0 && !_dead)
	    {
            Debug.Log("DEATH");
	        Die();
	    }

	    if (_playerHP.HP <= 0)
	    {
            Debug.Log("DÖD");
	        StopBossFight();
	    }
	        
	}

    private void StopBossFight()
    {
        _lokiAttack.SetStopAttack(true);
        _lokiMovement.StopAllMovementSequences();
        _animationController.StopFight();
    }

    public void HideStuff()
    {
        foreach (GameObject tmpGameObject in _gameObjects)
        {
            tmpGameObject.SetActive(false);
        }
    }

    private void ShowStuff()
    {
        foreach (GameObject tmpGameObject in _gameObjects)
        {
            tmpGameObject.SetActive(true);
        }
    }

    private void Die()
    {
        _dead = true;
        _lokiMovement.Die();        

        _lokiAttack.SetStopAttack(true);

        StopAllCoroutines();
        ShowStuff();
        

    }

    public void DisableLoki()
    {
        Debug.Log("DISABLE");
        gameObject.SetActive(false);
    }

    public void StartBossFight()
    {
        _lokiMovement.SetMovement();
        _lokiAttack.SetNormalAttack(true);
        _lokiAttack.SetStopAttack(false);

        _lokiAttack.Attack();

        StartCoroutine(StartTeleportingSequence(10));
    }

    private IEnumerator StartTeleportingSequence(float howLong)
    {
        yield return new WaitForSeconds(howLong);
        _lokiMovement.StartTeleportingSequence();
        _lokiAttack.SetStopAttack(true);
        StartCoroutine(StopTeleportingSequence(8));
    }

    private IEnumerator StopTeleportingSequence(float howLong)
    {
        yield return new WaitForSeconds(howLong);
        _lokiMovement._teleporting = false;
        _lokiAttack.SetNormalAttack(false);
        _lokiAttack.SetStopAttack(true);
        StartCoroutine(ResetBossfight(10));
    }

    private IEnumerator ResetBossfight(float howLong)
    {
        yield return new WaitForSeconds(howLong);
        StartBossFight();
    }
    

    
}
