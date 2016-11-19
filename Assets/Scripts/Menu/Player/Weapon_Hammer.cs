using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;
using System;

public class Weapon_Hammer : Weapon {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override float GetCooldown()
    {
        return 0f;
    }

    public override void BasicAttack(bool attack)
    {
        throw new NotImplementedException();
    }

    public override void SpecialAttack(bool attack)
    {
        throw new NotImplementedException();
    }
}
