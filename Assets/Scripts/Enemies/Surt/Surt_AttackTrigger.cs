using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;

namespace CallOfValhalla.Enemy {
    public class Surt_AttackTrigger : MonoBehaviour {

        private Surt_Attack _attack;

        // Use this for initialization
        void Start() {
            _attack = GetComponentInParent<Surt_Attack>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<Player_HP>().TakeDamage(_attack.Damage);
            }
        }
    }
}