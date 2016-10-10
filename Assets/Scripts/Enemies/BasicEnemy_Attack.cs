using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy
{
    public class BasicEnemy_Attack : MonoBehaviour
    {
        [SerializeField]
        private float _attackCoolDown;
        [SerializeField]
        private float _chanceOfAttack;
        [SerializeField]
        GameObject _attackArea;

        private GameObject _player;

        private float _timeBeforeAttacking;

        // Use this for initialization
        void Start()
        {
            _timeBeforeAttacking = _attackCoolDown;
            _attackArea.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Attack()
        {
            if(_timeBeforeAttacking <= 0)
            {
                float random = Random.Range(0, 100);

                if(random <= _chanceOfAttack)
                {
                    _attackArea.SetActive(true);
                }

                _timeBeforeAttacking = _attackCoolDown; 
            }


            _timeBeforeAttacking -= Time.deltaTime;
        }
    }
}