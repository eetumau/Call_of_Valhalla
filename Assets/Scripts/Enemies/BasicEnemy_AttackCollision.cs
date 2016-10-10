using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy
{
    public class BasicEnemy_AttackCollision : MonoBehaviour
    {

        [SerializeField]
        private int _damage;

        private bool _damageDealt;

        // Use this for initialization
        void Awake()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player" && !_damageDealt)
            {
                GameManager.Instance.Player.HP.TakeDamage(_damage);
                Debug.Log("DAMAGE TAKEN");
                gameObject.SetActive(false);
            }

        }

    }
}