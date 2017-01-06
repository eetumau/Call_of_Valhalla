using UnityEngine;
using System.Collections;
using CallOfValhalla;

public class GauntletTrigger : MonoBehaviour {

    private GauntletScript _gauntlet;
    private GameObject _trigger;


    public GameObject Trigger
    {
        get { return _trigger; }
    }
	// Use this for initialization
	private void Awake () {
        _gauntlet = FindObjectOfType<GauntletScript>();
        _trigger = GameObject.Find("GauntletTrigger");
        Debug.Log(_trigger);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _gauntlet.ActivateGauntlet();
            _trigger.SetActive(false);
            SoundManager.instance.SetMusic("boss_music_2");
            
        }
    }
}
