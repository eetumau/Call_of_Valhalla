using UnityEngine;
using System.Collections;

public class Troll_Decapitation : MonoBehaviour {

	public GameObject head;



	void Decapitation () {
		var t = transform;

		Instantiate(head, t.position + Vector3.up * 2F,Quaternion.identity);
	}

}
