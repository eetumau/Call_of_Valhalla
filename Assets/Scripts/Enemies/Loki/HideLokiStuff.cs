using UnityEngine;
using System.Collections;

public class HideLokiStuff : MonoBehaviour
{

    private LokiController _lokiController;

	// Use this for initialization
	void Start ()
	{
	    _lokiController = FindObjectOfType<LokiController>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _lokiController.HideStuff();
        }

    }
}
