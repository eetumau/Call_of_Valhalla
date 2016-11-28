using System;
using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;

namespace CallOfValhalla
{
    public class KillTrigger : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D other)
        {

            if (other.transform.tag == "Player")
                GameManager.Instance.Player.HP.TakeDamage(GameManager.Instance.Player.HP._hp);
        }
    }
}