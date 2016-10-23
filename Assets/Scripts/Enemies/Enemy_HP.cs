using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy {
    public class Enemy_HP : MonoBehaviour {

        [SerializeField]
        private float _hitPoints;


        private Animator _animator;
        private Enemy_Controller _enemyController;

        // Use this for initialization
        void Start() {

            _enemyController = GetComponent<Enemy_Controller>();
            _animator = GetComponent<Animator>();

        }

        // Update is called once per frame
        void Update() {

            if (Input.GetKeyDown(KeyCode.K))
            {
                _hitPoints--;
            }

            if (_hitPoints <= 0)
            {
                _animator.SetInteger("animState", 3);

                _enemyController.Instance.Die();
            }

        }
    }
}