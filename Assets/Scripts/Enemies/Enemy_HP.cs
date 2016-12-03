using UnityEngine;
using System.Collections;
using CallOfValhalla;

namespace CallOfValhalla.Enemy {
    public class Enemy_HP : MonoBehaviour {

        [SerializeField]
        public int _hitPoints;
        [SerializeField]
        private GameObject _blood;

        private Animator _animator;
        private Enemy_Controller _enemyController;
        private Transform _transform;
        private AudioSource _audioSource;

        public int HP
        {
            get { return _hitPoints; }
        }
        // Use this for initialization
        void Start() {

            _enemyController = GetComponent<Enemy_Controller>();
            _animator = GetComponent<Animator>();
            _transform = GetComponent<Transform>();
            _audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update() {

            if (_hitPoints <= 0)
            {
                _animator.SetInteger("animState", 3);
                _enemyController.Instance.Die();

                if (gameObject.name.Contains("Goblin"))
                {
                    SoundManager.instance.PlaySound("goblin_death_1", _enemyController.Source);

                }else if (gameObject.name.Contains("Troll"))
                {
                    SoundManager.instance.PlaySound("troll_death", _enemyController.Source);

                }
                else if (gameObject.name.Contains("Wolf"))
                {
                    SoundManager.instance.PlaySound("wolf_death", _enemyController.Source);

                }
            }

        }

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            Instantiate(_blood, _transform.position+Vector3.up, _transform.rotation );
        }
    }
}