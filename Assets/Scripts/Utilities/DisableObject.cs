using UnityEngine;
using System.Collections;

public class DisableObject : MonoBehaviour {

	[SerializeField] private GameObject[] _objects;
	
	private void OnTriggerEnter2D(Collider2D other)
    {
     	foreach (GameObject ob in _objects)
        {
            ob.SetActive(false);
        }
    }
}
