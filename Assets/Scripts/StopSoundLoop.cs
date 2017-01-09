using UnityEngine;
using System.Collections;

public class StopSoundLoop : MonoBehaviour {

    private AudioSource _source;

	// Use this for initialization
	void Start () {

        _source = GetComponent<AudioSource>();
	}
	
	public void StopLoop()
    {
        _source.loop = false;
    }
}
