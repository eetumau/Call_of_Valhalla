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

        private Transform _transform;
        private Surt_Attack _attack;
        private Player_HP _player;


        // Use this for initialization
        void Awake()
        {
            _transform = GetComponent<Transform>();
            _attack = FindObjectOfType<Surt_Attack>();
            _player = FindObjectOfType<Player_HP>();
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
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.tag != "Enemy")
            {
                if(other.gameObject.tag == "Player")
                {
                    _player.TakeDamage(_damage);
                    gameObject.SetActive(false);
                }else if(other.gameObject.tag == "Ground")
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}