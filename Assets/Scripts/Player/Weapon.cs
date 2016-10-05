using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Player
{
    abstract public class Weapon : MonoBehaviour
    {

        abstract public void BasicAttack();
        abstract public void SpecialAttack();
    }
}