using UnityEngine;
using System.Collections;


public class LokiController : MonoBehaviour {

    private LokiMovement _lokiMovement;
    private LokiAttack _lokiAttack;

    // Use this for initialization
    void Awake () {

        _lokiMovement = GetComponent<LokiMovement>();
        _lokiAttack = GetComponent<LokiAttack>();
        StartBossFight();
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void StartBossFight()
    {
        _lokiMovement.SetMovement();
        _lokiAttack.SetNormalAttack(true);
        _lokiAttack.SetStopAttack(false);

        _lokiAttack.Attack();
    }

    

    

    
}
