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

    private bool _basicActive;
    private bool _specialActive;
    private bool _specialInAir;
    private float _specialAttackCooldown;
    private Vector3 _specialColliderPosition;
    private float _timer1;
    private float _specialAttackMoveTimer;

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

        if (_specialActive)
            MoveColliders();
    }

    private void MoveColliders()
    {
        if (_specialInAir)
        {
            
        } else
        {
            if (_movement._playerTransform.localScale.x == 1f)
                _rigidBody2D.velocity = new Vector2(4.5f, 0f);
            else
                _rigidBody2D.velocity = new Vector2(-4.5f, 0f);
        }
    }

    // Returns special attack cooldown to display in UI
    public override float GetCooldown()
    {
        return _specialAttackCooldown;
    }

    // Sets the basic attack collider active and sets the cooldown timer
    public override void BasicAttack(bool attack)
    {
        if (!_specialActive && _timer1 <= 0)
        {            
            _timer1 = 0.8f;
            _basicCollider.SetActive(true);
            _movement.SetAttackAnimation("hammerbasic", 0.8f);
        }
    }

    public override void SpecialAttack(bool attack)
    {
        if (_specialAttackCooldown <= 0 && !_movement._isGrounded)
        {
            _specialAttackCooldown = 8f;
            _movement.HammerSpecialInAir();
            _specialInAir = true;
            
        } else if (_specialAttackCooldown <= 0 && _movement._isGrounded)
        {
            _movement.HammerSpecialInGround();
            _specialAttackCooldown = 8f;
            //SpecialInGround();
            _specialCollider.SetActive(true);
            float tempX = _hero.transform.position.x;
            float tempY = _hero.transform.position.y + 1;
            float tempZ = _hero.transform.position.z;

            // Fix collider position based on which way the character is facing
            if (_movement._playerTransform.localScale.x == 1f)
                tempX += 1;
            else
                tempX -= 1;                
            
            _movement.SetAttackAnimation("hammerGroundSpecial", 0.8f);
            _specialColliderPosition = new Vector3(tempX, tempY, tempZ);
            _specialCollider.transform.position = _specialColliderPosition;
            _specialActive = true;
            _specialAttackMoveTimer = 0.8f;
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

    private void SpecialInGround()
    {
        _specialCollider.SetActive(true);
        _specialColliderPosition = _specialCollider.transform.position;
        
    }

    private void SpecialInAir()
    {
        _specialCollider.SetActive(true);
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
            _specialActive = false;
            _specialInAir = false;
            _movement._hammerSpecialActive = false;
        }
        
    }
}
