using UnityEngine;
using System.Collections;
using CallOfValhalla;
using CallOfValhalla.UI;

namespace CallOfValhalla.Enemy
{
    public class Fenrir_HP : MonoBehaviour
    {

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
        private Fenrir_Controller _enemyController;
        private BossHPBarController _BossHP;
        private Transform _transform;
        private AudioSource _audioSource;

        public int HP
        {
            get { return hitPoints; }
        }
        // Use this for initialization
        void Start()
        {
            _enemyController = GetComponent<Fenrir_Controller>();

            _animator = GetComponent<Animator>();
            _transform = GetComponent<Transform>();
            _audioSource = GetComponent<AudioSource>();

            _originalHP = hitPoints;
            if (thisIsABoss)
                _BossHP = FindObjectOfType<BossHPBarController>();
        }

        // Update is called once per frame
        void Update()
        {

            if (hitPoints <= 0)
            {
                _enemyController.Die();
                _animator.SetInteger("animState", 3);
                SoundManager.instance.PlaySound("fenrir_death", _enemyController.Source, false);
                
            }

        }

        public void TakeDamage(int damage)
        {
            hitPoints -= damage;
            Instantiate(_blood, _transform.position + Vector3.up, _transform.rotation);

            if (thisIsABoss)
            {
                float tmp = hitPoints / _originalHP;
                _BossHP.Progress = tmp;
            }
        }
    }
}