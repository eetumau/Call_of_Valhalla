using UnityEngine;
using System.Collections;
using CallOfValhalla;
using CallOfValhalla.UI;

namespace CallOfValhalla.Enemy {
    public class Enemy_HP : MonoBehaviour {

        [SerializeField]
        public int hitPoints;
        [SerializeField]
        private GameObject _blood;
        [SerializeField]
        public bool thisIsABoss;
        [SerializeField]
        private bool _loki;

        private float _originalHP;
        private Animator _animator;
        private Enemy_Controller _enemyController;
        private BossHPBarController _BossHP;
        private Transform _transform;
        private AudioSource _audioSource;

        public int HP
        {
            get { return hitPoints; }
        }

        public float OGHP
        {
            get { return _originalHP; }
        }
        // Use this for initialization
        void Start() {

            if (!_loki)
                _enemyController = GetComponent<Enemy_Controller>();

            _animator = GetComponent<Animator>();
            _transform = GetComponent<Transform>();
            _audioSource = GetComponent<AudioSource>();

            _originalHP = hitPoints;

            if (thisIsABoss)
                _BossHP = FindObjectOfType<BossHPBarController>();
        }

        // Update is called once per frame
        void Update() {

            if (hitPoints <= 0)
            {


                if (!_loki)
                {
                    _animator.SetInteger("animState", 3);
                    _enemyController.Die();
                }
                    

                if (gameObject.name.Contains("BigAssGoblin"))
                {
                    SoundManager.instance.PlaySound("bigassgoblin_death", _enemyController.Source, false);
                }
                else if (gameObject.name.Contains("Troll"))
                {
                    SoundManager.instance.PlaySound("troll_death", _enemyController.Source, false);

                }
                else if (gameObject.name.Contains("Wolf"))
                {
                    SoundManager.instance.PlaySound("wolf_death", _enemyController.Source, false);

                }else if (gameObject.name.Contains("Surt"))
                {
                    SoundManager.instance.PlaySound("surt_death", _enemyController.Source, false);
                }
                else if(gameObject.name.Contains("Goblin"))
                {
                    SoundManager.instance.PlaySound("goblin_death_1", _enemyController.Source, false);

                } else if (gameObject.name.Contains("Loki"))
                {

                }
            }

        }

        public void TakeDamage(int damage)
        {
            hitPoints -= damage;
            Instantiate(_blood, _transform.position+Vector3.up, _transform.rotation );

            if (thisIsABoss)
            {
                float tmp = hitPoints / _originalHP;
                _BossHP.Progress = tmp;
            }
        }
    }
}