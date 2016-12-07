using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;
using System;
using CallOfValhalla;

public class Weapon_Hammer : Weapon
{

    private GameObject _basicCollider;
    private GameObject _specialCollider;
    private GameObject _hero;
    private Player_Movement _movement;
    private Rigidbody2D _rigidBody2D;
    private HammerSpecialCollision _specialCollisionScript;
    private BoxCollider2D _specialBoxCollider;

    [SerializeField]
    GameObject _basic;
    [SerializeField]
    GameObject _special;

    // Next huge bunch of booleans are mostly used to control the special attacks order of execution.
    // Hammers special attack is basically 2 different attacks and therefore this looks really messy...
    private bool _basicActive;
    private bool _moveSpecialCollider;
    private bool _specialCharging;

    // Timers to control the attack cooldowns and movement of the collider in special attack
    private float _timer1;
    private float _specialAttackMoveTimer;
    public float _specialChargeTimer;
    private float _specialCompletion = 100f;
    private float _SpecialColliderSize;
    private Vector3 _colliderSizeVector;


    private void Awake()
    {
        _hero = GameObject.Find("HeroSword_0");
        _movement = GetComponent<Player_Movement>();

        _basicCollider = Instantiate(_basic, transform.position, Quaternion.identity) as GameObject;
        _basicCollider.transform.parent = _hero.transform;
        _basicCollider.transform.position = new Vector2(transform.position.x + 1, transform.position.y + 1);
        _basicCollider.SetActive(false);

        // Asshole collider had a problem with being a child object, fixed by releasing the collider to roam the scene free from his parents shackles
        _specialCollider = Instantiate(_special, transform.position, Quaternion.identity) as GameObject;
        _specialCollider.SetActive(false);

        _rigidBody2D = _specialCollider.GetComponent<Rigidbody2D>();
        _specialCollisionScript = _specialCollider.GetComponent<HammerSpecialCollision>();
        _specialBoxCollider = _specialCollider.GetComponent<BoxCollider2D>();
        _colliderSizeVector = _specialBoxCollider.size;

        if (_specialCollisionScript == null)
            Debug.Log("NULL");
    }


    // Update is called once per frame
    void Update()
    {                        

        MoveColliders();
        UpdateSpecialCompletion();
        RunTimers();  
        CheckTimers();
    }

    // Moves the hit collider during the special attack
    private void MoveColliders()
    {                               
        if (_moveSpecialCollider)
        {
            _SpecialColliderSize += Time.deltaTime * 10;
            _colliderSizeVector.x = _SpecialColliderSize;
            _specialBoxCollider.size = (_colliderSizeVector);
        }
    }

    // Returns special attack cooldown to display in the UI
    public override float GetCompletion()
    {
        return _specialCompletion / 100f;
    }


    // Executed when player releases the special attack key. Ends charging and starts the attack
    public void SpecialAttackRelease()
    {
        if (_specialCharging)
        {
            SetColliderPositionToHero();
            _specialCharging = false;
            _specialCollider.SetActive(true);
            StopAllCoroutines();
            _moveSpecialCollider = true;
            _movement.SetAttackAnimation("HammerSpecial");
            StartCoroutine(ResetAfterSpecial(0.5f));
            _SpecialColliderSize = 1;
        }
    }

    // Adds percents to completion when player hits enemy with a basic attack
    public void AddCompletionByDamage(float completionPercent)
    {
        if (_specialCompletion < 100f)
            _specialCompletion += completionPercent;        
    }

    private void UpdateSpecialCompletion()
    {
        if (_specialCompletion < 100)
            _specialCompletion += Time.deltaTime;

        if (_specialCompletion > 100f)
            _specialCompletion = 100f;
    }

    // Sets the basic attack collider active and sets the cooldown timer
    public override void BasicAttack(bool attack)
    {
        if (!_moveSpecialCollider && !_specialCharging && _timer1 <= 0)
        {            
            _timer1 = 0.6f;
            _basicCollider.SetActive(true);
            _movement.SetAttackAnimation("hammerbasic", 0.6f);
            SoundManager.instance.PlaySound("hammer_swing", _movement.Source);
        }
    }

    // Base operations for hammer special attack, separate air and ground functions
    public override void SpecialAttack(bool attack)
    {

        if (_specialCompletion >= 100f)
        {
            Debug.Log("STEP1");
            _specialCompletion = 0f;
            _specialCharging = true;

            _specialCollisionScript.SetStunAndDamage("Small");
            _movement.SetAttackAnimation("Charge1");
            
            StartCoroutine(SetSecondChargeStep( 0.5f));
            StartCoroutine(SetThirdChargeStep(1f));
            

        }
    }

    // Count down all timers
    private void RunTimers()
    {
        if (_timer1 > 0)
            _timer1 -= Time.deltaTime;
        if (_specialAttackMoveTimer > 0)        
            _specialAttackMoveTimer -= Time.deltaTime;
        if (_specialCharging)
            _specialChargeTimer += Time.deltaTime;
        }

    // Returns special attack collider position after fixing it next to player to the direction he's facing
    private void SetColliderPositionToHero()
    {
        float tempX = _hero.transform.position.x;
        float tempY = _hero.transform.position.y + 1f;
        float tempZ = _hero.transform.position.z;

        _specialCollider.transform.position = new Vector3(tempX, tempY, tempZ);
        
    }

    /*
    // Controls the order of execution when doing special from air. --> Move forward only after character lands.
    private void SpecialInAir()
    {
        
        if (_specialInAirActive && _specialInAirButNotLanded)
        {
            
            if (_movement._isGrounded)
            {
                _specialCollider.SetActive(true);
                _specialColliderPosition = SnapColliderPosition();
                _specialCollider.transform.position = _specialColliderPosition;   
                _movement.SetAttackAnimation("hammerAirFinish", 0.8f);
                SoundManager.instance.PlaySound("hammer_super", _movement.Source);
                _specialInAirButNotLanded = false;
                _airSpecialCollision = true;
                _specialAttackMoveTimer = 0.8f;
                StartCoroutine(ResetAfterSpecial(0.8f));
                _moveSpecialCollider = true;                              
            }
        }
    }
    */

    private IEnumerator ResetAfterSpecial(float howLong)
    {
        yield return new WaitForSeconds(howLong);
        _specialCollider.SetActive(false);
        _moveSpecialCollider = false;
        _specialCharging = false;
        _movement._hammerSpecialActive = false;
    }

    private IEnumerator SetSecondChargeStep(float howLong)
    {
        yield return new WaitForSeconds(howLong);
        _specialCollisionScript.SetStunAndDamage("Medium");
        _movement.SetAttackAnimation("Charge2");
        Debug.Log("STEP2");
    }

    private IEnumerator SetThirdChargeStep(float howLong)
    {   
            yield return new WaitForSeconds(howLong);
            _specialCollisionScript.SetStunAndDamage("Large");
            _movement.SetAttackAnimation("Charge3");
            Debug.Log("STEP3");               
    }



    private void CheckTimers()
    {
        if (_timer1 <= 0.4f)
            _basicCollider.SetActive(false);
        if (_timer1 <= 0)                    
            _basicActive = false;               
    }
}
