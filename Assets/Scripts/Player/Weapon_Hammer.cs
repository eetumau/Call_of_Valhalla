using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;
using System;

public class Weapon_Hammer : Weapon
{

    private GameObject _basicCollider;
    private GameObject _specialCollider;
    private GameObject _hero;
    private Player_Movement _movement;
    private Rigidbody2D _rigidBody2D;

    [SerializeField]
    GameObject _basic;
    [SerializeField]
    GameObject _special;

    // Next huge bunch of booleans are mostly used to control the special attacks order of execution.
    // Hammers special attack is basically 2 different attacks and therefore this looks really messy...
    private bool _basicActive;
    private bool _moveGroundSpecialCollider;
    private bool _moveAirSpecialCollider;
    private bool _specialInAirActive;
    private bool _specialInAirButNotLanded;

    // Timers to control the attack cooldowns and movement of the collider in special attack
    private float _specialAttackCooldown;
    private float _timer1;
    private float _specialAttackMoveTimer;

    // Vector3 for the original position of the special attack collider before moving it
    private Vector3 _specialColliderPosition;

    private void Awake()
    {
        _hero = GameObject.Find("HeroSword_0");
        _movement = GetComponent<Player_Movement>();

        _basicCollider = Instantiate(_basic, transform.position, Quaternion.identity) as GameObject;
        _basicCollider.transform.parent = _hero.transform;
        _basicCollider.transform.position = new Vector2(transform.position.x + 1, transform.position.y + 1);
        _basicCollider.SetActive(false);

        _specialCollider = Instantiate(_special, transform.position, Quaternion.identity) as GameObject;
        _specialCollider.transform.parent = _hero.transform;
        _specialCollider.transform.position = new Vector2(transform.position.x + 1, transform.position.y + 1);
        _specialCollider.SetActive(false);

        _rigidBody2D = _specialCollider.GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        RunTimers();
        CheckTimers();

        
        MoveColliders();

        SpecialInAir();
        
    }

    // Moves the hit collider during the special attack
    private void MoveColliders()
    {    
        if (_moveAirSpecialCollider)
        {
            if (_movement._playerTransform.localScale.x == 1f)
                _rigidBody2D.velocity = new Vector2(4.5f, 0f);
            else
                _rigidBody2D.velocity = new Vector2(-4.5f, 0f);
        }
        if (_moveGroundSpecialCollider)
        {
            if (_movement._playerTransform.localScale.x == 1f)
                _rigidBody2D.velocity = new Vector2(4.5f, 0f);
            else
                _rigidBody2D.velocity = new Vector2(-4.5f, 0f);
        }
    }

    // Returns special attack cooldown to display in the UI
    public override float GetCooldown()
    {
        return _specialAttackCooldown;
    }

    // Sets the basic attack collider active and sets the cooldown timer
    public override void BasicAttack(bool attack)
    {
        if (!_moveGroundSpecialCollider && !_specialInAirActive && _timer1 <= 0)
        {            
            _timer1 = 0.8f;
            _basicCollider.SetActive(true);
            _movement.SetAttackAnimation("hammerbasic", 0.8f);
        }
    }

    // Base operations for hammer special attack, separate air and ground functions
    public override void SpecialAttack(bool attack)
    {

        if (_specialAttackCooldown <= 0)
        {
            _specialAttackCooldown = 8f;                                              

            if (!_movement._isGrounded)
            {                              
                
                _specialInAirActive = true;
                _movement.SetAttackAnimation("hammerAirSpecial", 0.8f);
                _specialInAirButNotLanded = true;
            }
            else 
            {
                _specialColliderPosition = SnapColliderPosition();
                _specialCollider.transform.position = _specialColliderPosition;
                _specialCollider.SetActive(true);             
                _movement.SetAttackAnimation("hammerGroundSpecial", 0.8f);                
                _moveGroundSpecialCollider = true;
                _specialAttackMoveTimer = 0.8f;
            }
        }
    }

    private void RunTimers()
    {
        if (_timer1 > 0)
            _timer1 -= Time.deltaTime;
        if (_specialAttackCooldown > 0)
            _specialAttackCooldown -= Time.deltaTime;
        if (_specialAttackMoveTimer > 0)        
            _specialAttackMoveTimer -= Time.deltaTime;
        
    }

    // Returns special attack collider position after fixing it next to player to the direction he's facing
    private Vector3 SnapColliderPosition()
    {
        float tempX = _hero.transform.position.x;
        float tempY = _hero.transform.position.y + 1;
        float tempZ = _hero.transform.position.z;

        // Fix colliders X position based on which way the character is facing
        if (_movement._playerTransform.localScale.x == 1f)
            tempX += 1;
        else
            tempX -= 1;

        return new Vector3(tempX, tempY, tempZ);
        
    }

    // Controls the order of execution when doing special from air. --> Move forward only after character lands.
    private void SpecialInAir()
    {
        
        if (_specialInAirActive && _specialInAirButNotLanded)
        {
            
            if (_movement._isGrounded)
            {
                Debug.Log("MAASSA");
                _specialColliderPosition = SnapColliderPosition();
                _specialCollider.transform.position = _specialColliderPosition;
                _movement.SetAttackAnimation("hammerAirFinish", 0.8f);
                _specialInAirButNotLanded = false;
                _specialAttackMoveTimer = 0.8f;
                _specialCollider.SetActive(true);
                _moveAirSpecialCollider = true;
            }
        }
    }

    private void CheckTimers()
    {
        if (_timer1 <= 0.6f)
            _basicCollider.SetActive(false);
        if (_timer1 <= 0)                    
            _basicActive = false;           
        if (_specialAttackMoveTimer <= 0)
        {
            _specialCollider.SetActive(false);
            _specialCollider.transform.position = _specialColliderPosition;            
            _moveGroundSpecialCollider = false;
            _moveAirSpecialCollider = false;     
            _movement._hammerSpecialActive = false;
        }        
    }
}
