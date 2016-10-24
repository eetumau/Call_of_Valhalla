using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy
{
    public class Enemy_Attack : MonoBehaviour
    {

        [SerializeField]
        private int _damage;

        private BoxCollider2D _attackHitBox;
        private Enemy_Attack _instance;
        private Enemy_Controller _enemyController;
        private Player_HP _player;
        private Animator _animator;

        public Enemy_Attack Instance
        {
            get { return _instance; }
        }

        // Use this for initialization
        void Start()
        {
            _instance = this;
            _attackHitBox = GetComponentInChildren<BoxCollider2D>();
            _attackHitBox.enabled = false;
            _player = FindObjectOfType<Player_HP>();
            _enemyController = GetComponentInParent<Enemy_Controller>();
            _animator = GetComponentInParent<Animator>();
            
        }

        // Update is called once per frame
        void Update()
        {

            if (_enemyController.Instance.InAttackRange)
            {
                Attack();
            }


        }

        public void DealDamage()
        {
            _player.Instance.TakeDamage(_damage);
        }

        private void Attack()
        {
            _animator.SetInteger("animState", 2);
            _attackHitBox.enabled = true;

            
        }
    }
}