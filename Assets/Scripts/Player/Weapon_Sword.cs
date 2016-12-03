using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Player
{
    public class Weapon_Sword : Weapon
    {
        
        // Colliders for all the attacks
        private GameObject _basic1Collider;        
        private GameObject _basic2Collider;
        private GameObject _specialCollider;

        [SerializeField]
        GameObject _hero;
        private Player_Movement _movement;
        private float _timer1;
        private float _timer2;
        private float _specialAttackTimer;
        private float _specialAttackCooldown;
        private float _specialCompletion = 100f;
        [SerializeField]
        private float _specialAttackMaxCooldown;

        private bool _basic1Active;
        private bool _basic2Active;
        private bool _specialActive;

        // Prefabs for the attack colldiers
        [SerializeField]
        GameObject _basic1;
        [SerializeField]
        GameObject _basic2;
        [SerializeField]
        GameObject _special;

        // Instantiates the colliders for all the attacks, also moves them to be childs of the character object
        private void Awake()
        {                                 
            _basic1Collider = Instantiate(_basic1, transform.position, Quaternion.identity) as GameObject;
            _basic1Collider.transform.parent = _hero.transform;
            _basic1Collider.transform.position = new Vector2(transform.position.x + 1, transform.position.y + 1);
            _basic1Collider.SetActive(false);

            _basic2Collider = Instantiate(_basic2, transform.position, Quaternion.identity) as GameObject;
            _basic2Collider.transform.parent = _hero.transform;
            _basic2Collider.transform.position = new Vector2(transform.position.x + 1, transform.position.y + 1);
            _basic2Collider.SetActive(false);

            _specialCollider = Instantiate(_special, transform.position, Quaternion.identity) as GameObject;
            _specialCollider.transform.parent = _hero.transform;
            _specialCollider.transform.position = new Vector2(transform.position.x + 1, transform.position.y + 1);
            _specialCollider.SetActive(false);

            _movement = GetComponent<Player_Movement>();

        }

        // Basic operations for the basic attack
        public override void BasicAttack(bool attack)
        {

            if (attack && _basic1Active && !_specialActive)
            {                
                _timer1 = 0;
                _timer2 = 0.4f;
                _basic1Collider.SetActive(false);
                _basic2Collider.SetActive(true);
                _basic2Active = true;
                _movement.SetAttackAnimation("swordbasic2", 0.4f);
                SoundManager.instance.PlaySound("sword_1", _movement.Source);

            }
            if (attack && !_basic1Active && !_specialActive)
            {
                _basic1Collider.SetActive(true);
                _basic1Active = true;
                _timer1 = 0.4f;
                _movement.SetAttackAnimation("swordbasic1", 0.4f);
                SoundManager.instance.PlaySound("sword_1", _movement.Source);
            }
            
        }

        // Returns special attack cooldown to display in the UI
        public override float GetCompletion()
        {
            return _specialCompletion / 100f;
            
        }

        public void AddCompletionByDamage(float completionPercent)
        {
            if (_specialCompletion < 100f)
                _specialCompletion += completionPercent;

        }

        public override void SpecialAttack(bool attack)
        {
            if (attack && _specialCompletion >= 100 && _movement._isGrounded )
            {
                _movement.SwordDash();
                _specialCollider.SetActive(true);
                _specialActive = true;
                _specialAttackTimer = 0.4f;
                _movement.SetAttackAnimation("swordspecial", 0.4f);
				SoundManager.instance.PlaySound("sword_super", _movement.Source);
                _specialCompletion = 0f;
            }
        }

        private void UpdateSpecialCompletion()
        {
            if (_specialCompletion < 100)
                _specialCompletion += Time.deltaTime;
            if (_specialCompletion > 100f)
                _specialCompletion = 100f;
        }

        // Update is called once per frame
        void Update()
        {
            CheckTimers();
            RunTimers();            
            UpdateSpecialCompletion();
        }

        private void RunTimers()
        {                  
            if (_timer1 > 0)
                _timer1 -= Time.deltaTime;           
            if (_timer2 > 0)            
                _timer2 -= Time.deltaTime;
            if (_specialAttackTimer > 0)
                _specialAttackTimer -= Time.deltaTime;
            if (_specialAttackCooldown > 0)
                _specialAttackCooldown -= Time.deltaTime;
        }

        // Checks when timers reach zero, sets colliders to disabled
        private void CheckTimers()
        {
            if (_timer1 <= 0)
            {
                _basic1Collider.SetActive(false);
                _basic1Active = false;
            }
            if (_timer2 <= 0)
            {
                _basic2Collider.SetActive(false);
                _basic2Active = false;
            }
            if (_specialAttackTimer <= 0)
            {
                _specialCollider.SetActive(false);
                _specialActive = false;
            }
        }
    }
}
