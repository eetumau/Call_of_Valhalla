using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Vector3))]
public class TorchLight : MonoBehaviour {

	private Vector3 start = new Vector3 (0.8F,0.8F,0.8F);
	private Vector3 end = new Vector3 (1.2F, 1.2F, 1.2F);
	private float random;

	// Use this for initialization
	void Start () {
		random = Random.Range (0.0F, 65535.0F);
	}
	
	// Update is called once per frame
	void Update () {
		float noise = Mathf.PerlinNoise (random, Time.time);
		gameObject.transform.localScale = Vector3.Lerp(start, end, noise);
	}
}
