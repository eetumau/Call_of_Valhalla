using UnityEngine;
using System.Collections;
using CallOfValhalla;
using CallOfValhalla.UI;

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
        public int _hp;

        private Player_Movement _movement;
        private Player_InputController _input;
        private Transform _transform;
        private HPBarController _hpBar;
        private bool _respawning;
        private bool _dead;

        public Player_HP Instance
        {
            get { return _instance; }
        }

        public int HP
        {
            get { return _hp; }
            set { _hp = value; }
        }

        // Use this for initialization
        void Awake()
        {
            _instance = this;
            _animator = GetComponent<Animator>();
            _movement = GetComponent<Player_Movement>();
            _input = GetComponent<Player_InputController>();
            _delayTimer = _deathDelay;
            _hp = _OriginalHP;
            _transform = GetComponent<Transform>();

            _hpBar = FindObjectOfType<HPBarController>();
        }

        public void TakeDamage(int damage)
        {
            _hp -= damage;

            _hpBar.Progress = ((float)_hp)/ ((float)_OriginalHP);
            _hpBar.SecondaryProgress += ((float)damage) / ((float)_OriginalHP);

            if (_hp <= 0 && !_dead)
            {
                SoundManager.instance.PlaySound("death_3", _movement.Source);

                Time.timeScale = 0.5f;
                Die();
            }
        }

        public void Die()
        {
            if (!_respawning)
            {
                GameManager.Instance.CameraFollow.Sepia.enabled = true;
                _dead = true;
                _movement.enabled = false;
                _input.enabled = false;
                _animator.SetInteger("animState", 9);
                StartCoroutine(GameOverTimer(_deathDelay));
                /*
                if (_delayTimer <= 0)
                {
                    GameManager.Instance.GameOver();
                }

                _delayTimer -= Time.deltaTime;
                */
            }
        }

        private IEnumerator GameOverTimer(float howLong)
        {
            yield return new WaitForSeconds(howLong);
            GameManager.Instance.GameOver();
        }

        public void Respawn(Transform spawnPoint)
        {
            Time.timeScale = 1;
            GameManager.Instance.CameraFollow.Sepia.enabled = false;
            _respawning = true;
            _dead = false;
            _movement.enabled = true;
            _input.enabled = true;
            _animator.SetInteger("animState", 0);
            _delayTimer = _deathDelay;
            _hp = _OriginalHP;
            _transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, _transform.position.z);
            _respawning = false;
            _hpBar.Progress = 1f;

        }

    }
}