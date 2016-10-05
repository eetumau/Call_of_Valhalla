using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy {
    public class BasicEnemy_Attack : MonoBehaviour {

        [SerializeField]
        private int _chanceOfAttack;
        private Transform _transform;
        [SerializeField]
        private GameObject _attackArea;
        [SerializeField]
        private float _minAttackCoolDown;
        [SerializeField]
        private float _maxAttackCoolDown;
        private float _timeBeforeNextAttack;


        // Use this for initialization
        void Start() {

            _transform = GetComponent<Transform>();            
        }

        // Update is called once per frame
        void Update() {

        }

        public void Attack(Vector3 targetPosition)
        {
            //_attackArea.SetActive(false);

            Vector3 distance = _transform.position - targetPosition;

            if (distance.magnitude <= BasicEnemy_AI.Instance.Movement.MinDistance)
            {

                if (_timeBeforeNextAttack <= 0)
                {
                    //Debug.Log("Pitäs lyödä");
                    int random = Random.Range(0, _chanceOfAttack);

                    if (random == _chanceOfAttack - 1)
                    {
                        _attackArea.SetActive(true);
                        Debug.Log("BAMM");
                    }
                    _timeBeforeNextAttack = Random.Range(_minAttackCoolDown, _maxAttackCoolDown + 1);

                }
                _timeBeforeNextAttack -= Time.deltaTime;
            }
        }



    }
}