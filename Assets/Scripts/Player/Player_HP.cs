using UnityEngine;
using System.Collections;
using CallOfValhalla;

namespace CallOfValhalla.Player
{
    public class Player_HP : MonoBehaviour
    {

        [SerializeField]
        private int _hp;
        [SerializeField]
        private float _deathDelay;

        private Player_HP _instance;
        private Animator _animator;

        private Player_Movement _movement;
        private Player_InputController _input;

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
            _input = GetComponent<Player_InputController>();
        }

        // Update is called once per frame
        void Update()
        {
            if(_hp <= 0)
            {
                Die();
            }

        }

        public void TakeDamage(int damage)
        {
            _hp -= damage;
            Debug.Log(_hp);
        }

        private void Die()
        {
            _movement.enabled = false;
            _input.enabled = false;
            _animator.SetInteger("animState", 9);

            if(_deathDelay <= 0)
            {
                GameManager.Instance.GameOver();
            }

            _deathDelay -= Time.deltaTime;
        }

    }
}