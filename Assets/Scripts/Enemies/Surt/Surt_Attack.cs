using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CallOfValhalla;


namespace CallOfValhalla.Enemy {
    public class Surt_Attack : MonoBehaviour {

        [SerializeField]
        private GameObject _fireSpawnObj;
        [SerializeField]
        private GameObject _projectile;
        [SerializeField]
        private int _damage;
        [SerializeField]
        private float _attackCooldown;
        [SerializeField]
        private float _attackChance;
        [SerializeField]
        private float _shootDuration;
        [SerializeField]
        private float _rainDuration;
        [SerializeField]
        private float _rainInterval;
        

        private Surt_Movement _movement;
        private Surt_AttackTrigger _trigger;
        private Surt_AnimationController _surtAC;
        private Enemy_HP _hp;
        private ParticleSystem[] _ps;
        private GameObject _specialPoint;
        private Transform _transform;
        private AudioSource _source;
        private Surt_Death _death;
        private float _cooldownTimer;
        private bool _attacking = false;
        private bool _specialAttacking = false;
        private bool _specialDone;
        private float _rainIntervalTimer;
        private CameraShake _camShake;

        private int _pooledProjectiles = 30;
        private int index;
        private Transform[] _fireSpawns;


        private List<GameObject> _projectiles;

        public bool Attacking
        {
            get { return _attacking; }
        }

        public int Damage
        {
            get { return _damage; }
        }

        public bool SpecialAttacking
        {
            get { return _specialAttacking; }
        }

        public Transform[] FireSpawns
        {
            get { return _fireSpawns; }
        }

        // Use this for initialization
        void Start() {
            _movement = GetComponentInParent<Surt_Movement>();
            _trigger = GetComponentInChildren<Surt_AttackTrigger>();
            _surtAC = GetComponentInParent<Surt_AnimationController>();
            _hp = GetComponentInParent<Enemy_HP>();
            _specialPoint = GameObject.Find("SurtSpecialPoint");
            _transform = _movement.GetComponent<Transform>();
            _cooldownTimer = _attackCooldown;
            _ps = GetComponentsInChildren<ParticleSystem>();
            _source = GetComponent<AudioSource>();
            _camShake = FindObjectOfType<CameraShake>();
            _fireSpawns = _fireSpawnObj.GetComponentsInChildren<Transform>();
            _projectiles = new List<GameObject>();
            _rainIntervalTimer = _rainInterval;
            SetupProjectiles();
        }

        // Update is called once per frame
        void Update() {

            if (_movement.InRange && !_attacking && _movement.StunTimer <= 0)
            {
                CheckNextAction();
            }

        }


        private void SetupProjectiles()
        {

            for (int i = 0; i < _pooledProjectiles; i++)
            {

                GameObject tmpGO = Instantiate(_projectile, _fireSpawnObj.transform.position, Quaternion.identity) as GameObject;
                tmpGO.SetActive(false);
                _projectiles.Add(tmpGO);
            }
        }

        private void CheckNextAction()
        {
            if(_hp.HP < _hp.OGHP / 2 && !_specialDone)
            {
                StartCoroutine(SpecialAttack());
            }

            if(_cooldownTimer <= 0 && !_specialAttacking)
            {
                int random = (int)Random.Range(0, _attackChance +1);

                if(random == _attackChance)
                {
                    Attack();
                }else
                {
                    _cooldownTimer = _attackCooldown;
                }
            }

            if (!_attacking && !_specialAttacking)
            {
                _cooldownTimer -= Time.deltaTime;
            }
        }

        private void Attack()
        {
            _attacking = true;
            _surtAC.SetAnimation(2);
            SoundManager.instance.PlaySound("goblin_swing", _source, false);
            
            _cooldownTimer = _attackCooldown;
        }

        private IEnumerator SpecialAttack()
        {
            _movement.SpecialMoving = true;
            _specialDone = true;
            Vector2 _distance = _transform.position - _specialPoint.transform.position;
            while(_distance.magnitude > 1)
            {
                if(_movement.StunTimer <= 0)
                {
                    _surtAC.SetAnimation(1);

                    CheckDirection();
                    _distance = _transform.position - _specialPoint.transform.position;
                    _transform.position = Vector2.MoveTowards(_transform.position, new Vector2(_specialPoint.transform.position.x, _transform.position.y), 5 * Time.deltaTime);

                }

                yield return null;
            }
            _movement.SpecialMoving = false;
            SoundManager.instance.PlaySound("surt_death", _source, false);
            _surtAC.SetAnimation(6);
            StartCoroutine(_camShake.Shake(_shootDuration));
            _specialAttacking = true;
            _attacking = true;
            StartParticles();
            StartCoroutine(RainTimer());



            yield return new WaitForSeconds(_shootDuration);

            _surtAC.SetAnimation(0);
            StopParticles();
            _attacking = false;
            _specialAttacking = false;
            

        }

        private IEnumerator RainTimer()
        {
            while (_rainDuration > 0)
            {
                if (_rainIntervalTimer < 0)
                {
                    RainFire();
                    _rainIntervalTimer = _rainInterval;
                }

                _rainIntervalTimer -= Time.deltaTime;
                _rainDuration -= Time.deltaTime;
                yield return null;
            }

        }

        private void RainFire()
        {

                for (int i = 0; i < _projectiles.Count; i++)
                {
                    if (!_projectiles[i].activeInHierarchy)
                    {
                        _projectiles[i].SetActive(true);
                        break;
                    }

                }
            
        }

        private void CheckDirection()
        {
            if(_movement.IsFacingRight && _movement.transform.position.x > _specialPoint.transform.position.x)
            {
                _movement.IsFacingRight = false;
                _movement.transform.localScale =
    new Vector2(-1 * _movement.transform.localScale.x, _movement.transform.localScale.y);

            }
            else if(_movement.IsFacingRight && _movement.transform.position.x < _specialPoint.transform.position.x)
            {
                _movement.IsFacingRight = true;
                _movement.transform.localScale =
    new Vector2(-1 * _movement.transform.localScale.x, _movement.transform.localScale.y);

            }

        }

        public void StartParticles()
        {
            foreach(ParticleSystem ps in _ps)
            {
                ps.Play();
            }
        }

        public void StopParticles()
        {
            foreach(ParticleSystem ps in _ps)
            {
                ps.Stop();
            }
        }

        public void EnableAttackHitBox()
        {
            //_surtAC.SetAnimation(0);
            _trigger.GetComponent<BoxCollider2D>().enabled = true;
        }

        public void DisableAttackHitBox()
        {
            _cooldownTimer = _attackCooldown;
            //_attacking = false;
            _trigger.GetComponent<BoxCollider2D>().enabled = false;
        }

        public void EnableAttacking()
        {
            _attacking = true;
        }
        public void DisableAttacking()
        {
            _attacking = false;
        }
    }
}