using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Player
{
    public class Player_HP : MonoBehaviour
    {

        [SerializeField]
        private int _hp;

        private Player_HP _instance;
        private Animator _animator;

        private Player_Movement _movement;

        public Player_HP Instance
        {
            get { return _instance; }
        }

        // Use this for initialization
        void Start()
        {
            _instance = this;
            _animator = GetComponent<Animator>();
            _movement = GetComponent<Player_Movement>();
        }

        // Update is called once per frame
        void Update()
        {
            if(_hp <= 0)
            {
                
            }

        }

        public void TakeDamage(int damage)
        {
            _hp -= damage;
            Debug.Log(_hp);
        }
    }
}