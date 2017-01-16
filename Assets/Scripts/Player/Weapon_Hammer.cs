using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;
using System;
using CallOfValhalla;

public class Weapon_Hammer : MonoBehaviour
{

    private GameObject _basicCollider;
    private GameObject _specialCollider;
    private GameObject _hero;
    private Player_Movement _movement;
    private Rigidbody2D _rigidBody2D;
    private HammerSpecialCollision _specialCollisionScript;
    private BoxCollider2D _specialBoxCollider;
    private GameObject _lightning;
    private Animator _lightningAnimator;
    private AudioSource _lightningSource;
    private Camera _cam;
    private LightningFlash _flash;

    [SerializeField]
    GameObject _basic;
    [SerializeField]
    GameObject _special;
    [SerializeField]
    GameObject _sprite;

    
    private bool _basicActive;
    private bool _moveSpecialCollider;
    private bool _specialCharging;
    private bool _fullyCharged;

    public bool basicLocked;
    private float _timer1;
    private float _specialAttackMoveTimer;
    public float _specialChargeTimer;
    private float _specialCompletion = 100f;
    private float _SpecialColliderSize;
    private Vector3 _colliderSizeVector;


    public AudioSource LightningSource
    {
        get { return _lightningSource; }
    }

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

        _lightning = GameObject.Find("Lightning");
        _lightningAnimator = _lightning.GetComponent<Animator>();
        _lightningSource = _lightning.GetComponent<AudioSource>();
        _cam = FindObjectOfType<Camera>();
        _flash = _cam.GetComponent<LightningFlash>();
    }


    // Update is called once per frame
    void Update()
    {                        

        MoveColliders();
        UpdateSpecialCompletion();
        RunTimers();

        _timer1 += Time.deltaTime;
    }

    // Moves the hit collider during the special attack
    private void MoveColliders()
    {                               
        if (_moveSpecialCollider)
        {
            if (_fullyCharged)
            {
                _SpecialColliderSize += Time.deltaTime*13;
                _colliderSizeVector.x = _SpecialColliderSize;
                _specialBoxCollider.size = (_colliderSizeVector);
            }
            else
            {
                _SpecialColliderSize += Time.deltaTime * 10;
                _colliderSizeVector.x = _SpecialColliderSize;
                _specialBoxCollider.size = (_colliderSizeVector);
            }
        }
    }

    // Returns special attack cooldown to display in the UI
    public float GetCompletion()
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
            _SpecialColliderSize = 1;

            if (_fullyCharged)
            {
                _movement.SetAttackAnimation("HammerSpecialFull");
                StartCoroutine(ResetAfterSpecial(0.5f));
                StartCoroutine(ResetLightning(0.267f));
                _lightningAnimator.SetInteger("Animstate", 1);
                StartCoroutine(_flash.Flash());

            }
            else
            {
                _movement.SetAttackAnimation("HammerSpecial");
                StartCoroutine(ResetAfterSpecial(0.5f));
            }

            SoundManager.instance.PlaySound("lightningStrike", _movement.Source, false);
            SoundManager.instance.PlaySound("mjolnirLightning", _lightningSource, false);

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

    public void ResetBasicAttack()
    {
        basicLocked = false;
        _movement._hammerBasicActive = false;
    }

    // Sets the basic attack collider active and sets the cooldown timer
    public void BasicAttack(bool attack)
    {
        if (!_moveSpecialCollider && !_specialCharging && !basicLocked)
        {
            _timer1 = 0.0f;
            basicLocked = true;
            _movement.SetAttackAnimation("hammerbasic");
            SoundManager.instance.PlaySound("hammer_swing", _movement.Source, false);
        }
    }

    public void EnableBasicCollider()
    {
        if (_basicCollider.activeSelf)
        {
            _basicCollider.SetActive(false);
        }
        else
        {
            _basicCollider.SetActive(true);
        }
    }

    // Base operations for hammer special attack, separate air and ground functions
    public void SpecialAttack(bool attack)
    {

        if (_specialCompletion >= 100f)
        {
            _specialCompletion = 0f;
            _specialCharging = true;

            _specialCollisionScript.SetStunAndDamage("Small");
            _movement.SetAttackAnimation("Charge1");
            SoundManager.instance.PlaySound("charge_1", _lightningSource, false);
            
            StartCoroutine(SetSecondChargeStep( 0.5f));
            StartCoroutine(SetThirdChargeStep(1f));
            

        }
    }

    // Count down all timers
    private void RunTimers()
    {
        if (_specialAttackMoveTimer > 0)        
            _specialAttackMoveTimer -= Time.deltaTime;
        if (_specialCharging)
            _specialChargeTimer += Time.deltaTime;
        if (_timer1 > 0.33f && basicLocked)
        {
            ResetBasicAttack();
        }
    }

    // Returns special attack collider position after fixing it next to player to the direction he's facing
    private void SetColliderPositionToHero()
    {
        float tempX = _hero.transform.position.x;
        float tempY = _hero.transform.position.y + 1f;
        float tempZ = _hero.transform.position.z;

        _specialCollider.transform.position = new Vector3(tempX, tempY, tempZ);
        
    }

    private IEnumerator ResetLightning(float howLong)
    {
        yield return new WaitForSeconds(howLong);
        _lightningAnimator.SetInteger("Animstate", 0);
    }

    public IEnumerator ResetAfterSpecial(float howLong)
    {
        yield return new WaitForSeconds(howLong);
        _specialCollider.SetActive(false);
        _moveSpecialCollider = false;
        _specialCharging = false;
        _fullyCharged = false;
        _movement._hammerSpecialActive = false;
        
    }

    private IEnumerator SetSecondChargeStep(float howLong)
    {
        yield return new WaitForSeconds(howLong);
        _specialCollisionScript.SetStunAndDamage("Medium");
        _movement.SetAttackAnimation("Charge2");
        
    }

    private IEnumerator SetThirdChargeStep(float howLong)
    {   
        yield return new WaitForSeconds(howLong);
        SoundManager.instance.PlaySound("charge_2", _lightningSource, true);
        _specialCollisionScript.SetStunAndDamage("Large");
        _movement.SetAttackAnimation("Charge3");
        _fullyCharged = true;
                          
    }

}
