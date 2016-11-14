using UnityEngine;
using System.Collections;
using CallOfValhalla;

namespace CallOfValhalla.Player
{
    public class Player_HP : MonoBehaviour
    {

        [SerializeField]
        private int _OriginalHP;
        [SerializeField]
        private float _deathDelay;

        private Player_HP _instance;
        private Animator _animator;
        private float _delayTimer;
        private int _hp;

        private Player_Movement _movement;
        private Player_InputController _input;
        private Transform _transform;
        private bool _respawning;

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
            _delayTimer = _deathDelay;
            _hp = _OriginalHP;
            _transform = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            if(_hp <= 0 && !_respawning)
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

            if(_delayTimer <= 0)
            {
                GameManager.Instance.GameOver();
            }

            _delayTimer -= Time.deltaTime;
        }

        public void Respawn(Transform spawnPoint)
        {
            _respawning = true;
            _movement.enabled = true;
            _input.enabled = true;
            _animator.SetInteger("animState", 0);
            _delayTimer = _deathDelay;
            _hp = _OriginalHP;
            _transform.position = spawnPoint.position;
            _respawning = false;

        }

    }
}