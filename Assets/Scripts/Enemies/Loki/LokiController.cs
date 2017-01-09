using UnityEngine;
using System.Collections;
using CallOfValhalla.Enemy;


public class LokiController : MonoBehaviour {

    private LokiMovement _lokiMovement;
    private LokiAttack _lokiAttack;
    private Enemy_HP _hp;
    private bool _dead;


    // Use this for initialization
    void Awake () {

        _lokiMovement = GetComponent<LokiMovement>();
        _lokiAttack = GetComponent<LokiAttack>();
        _hp = GetComponent<Enemy_HP>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (_hp.hitPoints <= 0 && !_dead)
	    {
            Debug.Log("DEATH");
	        Die();
	    }
	        
	}

    private void Die()
    {
        _dead = true;
        _lokiMovement.Die();        

        _lokiAttack.SetStopAttack(true);

        StopAllCoroutines();
        
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
