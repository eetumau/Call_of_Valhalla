using UnityEngine;
using System.Collections;

namespace CallOfValhalla
{
    public class Player_HP : MonoBehaviour
    {

        [SerializeField]
        private int _hp;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TakeDamage(int damage)
        {
            _hp -= damage;
            Debug.Log(_hp);
        }
    }
}