using UnityEngine;
using System.Collections;
using CallOfValhalla.Enemy;
using CallOfValhalla.Player;
using CallOfValhalla;

public class LokiController : MonoBehaviour {

    private LokiMovement _lokiMovement;
    private LokiAttack _lokiAttack;
    private LokiAnimationController _animationController;
    private Enemy_HP _hp;
    private Player_HP _playerHP;
    private bool _dead;
    private Player_CameraFollow _cameraFollow;

    [SerializeField]
    private GameObject[] _gameObjects;

    // Use this for initialization
    void Awake () {

        _lokiMovement = GetComponent<LokiMovement>();
        _lokiAttack = GetComponent<LokiAttack>();
        _hp = GetComponent<Enemy_HP>();
        _playerHP = FindObjectOfType<Player_HP>();
        _animationController = GetComponent<LokiAnimationController>();
        _cameraFollow = FindObjectOfType<Player_CameraFollow>();

    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (_hp.hitPoints <= 0 && !_dead)
	    {
	        Die();
	    }

	    if (_playerHP.HP <= 0)
	    {
	        StopBossFight();
	    }
	        
	}

    private void StopBossFight()
    {
        StopAllCoroutines();
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
        _cameraFollow.CameraDelayAfterLoki();

    }

    

    public void DisableLoki()
    {
        gameObject.SetActive(false);
    }

    public void StartBossFight()
    {
        _lokiMovement.SetMovement();
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
        _lokiAttack.SetStopAttack(true);
        StartCoroutine(ResetBossfight(8));
    }

    private IEnumerator ResetBossfight(float howLong)
    {
        yield return new WaitForSeconds(howLong);
        StartBossFight();
    }
    

    
}
