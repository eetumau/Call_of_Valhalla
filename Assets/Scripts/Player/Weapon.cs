using UnityEngine;
using System.Collections;

abstract public class Weapon : MonoBehaviour {

    abstract public void BasicAttack(bool attack);
    abstract public void SpecialAttack(bool _specialAttack);
}
