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
            GameManager.Instance.Player.HP.HP = 0;
        }
    }
}