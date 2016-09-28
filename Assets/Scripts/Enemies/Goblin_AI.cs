using UnityEngine;
using System.Collections;

public class Goblin_AI : MonoBehaviour {

    private Goblin_Move Goblin_Move;

    private bool _aggro = false;

	// Use this for initialization
	void Start () {

        Goblin_Move = GetComponent<Goblin_Move>();
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Goblin_Move.MoveToPlayer(other.gameObject.transform.position);
        }
    }
}
