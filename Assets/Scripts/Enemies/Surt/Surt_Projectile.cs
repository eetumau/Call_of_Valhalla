using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;

namespace CallOfValhalla.Enemy
{
    public class Surt_Projectile : MonoBehaviour
    {


        [SerializeField]
        private int _speed;
        [SerializeField]
        private int _damage;
        [SerializeField]
        private float _disableTime;


        private Transform _transform;
        private Surt_Attack _attack;
        private Player_HP _player;
        private GameObject _surt;
        private Enemy_HP _surtHP;

        private float _disableTimer;


        // Use this for initialization
        void Awake()
        {
            _transform = GetComponent<Transform>();
            _attack = FindObjectOfType<Surt_Attack>();
            _player = FindObjectOfType<Player_HP>();
            _disableTimer = _disableTime;
            _surt = GameObject.Find("Surt");
            _surtHP = _surt.GetComponent<Enemy_HP>();
        }

        void OnEnable()
        {
            int random = (int)Random.Range(0, _attack.FireSpawns.Length);
            _transform.position = _attack.FireSpawns[random].position;
        }

        void OnDisable()
        {
        }

        // Update is called once per frame
        void Update()
        {
            float newY = _transform.position.y - _speed * Time.deltaTime;
            _transform.position = new Vector2(_transform.position.x, newY);

            DisableTimer();
        }

        private void DisableTimer()
        {

            if(_disableTimer < 0)
            {

                gameObject.SetActive(false);
                _disableTimer = _disableTime;

            }
            _disableTimer -= Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.tag != "Enemy")
            {
                if (_surtHP.HP > 0) {
                    if (other.gameObject.tag == "Player")
                    {
                        _player.TakeDamage(_damage);
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}