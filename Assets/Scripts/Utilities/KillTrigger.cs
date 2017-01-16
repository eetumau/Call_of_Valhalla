using System;
using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;
using CallOfValhalla.Enemy;

namespace CallOfValhalla
{
    public class KillTrigger : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D other)
        {

            if (other.transform.tag == "Player")
                GameManager.Instance.Player.HP.TakeDamage(GameManager.Instance.Player.HP._hp);
            else if(other.gameObject.tag == "Enemy")
            {
                var enemy = other.gameObject.GetComponent<Enemy_HP>();

                if (enemy != null)
                {
                    enemy.TakeDamage(enemy.HP);
                }else
                {
                    enemy = other.gameObject.GetComponentInParent<Enemy_HP>();

                    enemy.TakeDamage(enemy.HP);
                }
            }
                
        }
    }
}