using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;

namespace CallOfValhalla
{
    public class KillTrigger : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameManager.Instance.Player.HP.HP = 0;
        }
    }
}