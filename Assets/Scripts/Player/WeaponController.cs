using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    private string Weapon1;
    private string Weapon2;

    private void Awake()
    {
        Weapon1 = "Sword";
        Weapon2 = "Sword";
        SetWeapons();
    }

    public void SetWeapons()
    {
        if (Weapon1.Equals("Sword")) {

        }

        if (Weapon2.Equals("Sword"))
        {

        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
