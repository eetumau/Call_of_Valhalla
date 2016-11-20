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

    [SerializeField]
    GameObject _basic;
    [SerializeField]
    GameObject _special;

    private bool _basicActive;
    private bool _specialActive;
    private float _specialAttackCooldown;

    private float _timer1;

    private void Awake()
    {
        _hero = GameObject.Find("HeroSword_0");
        _movement = GetComponent<Player_Movement>();

        if (_hero != null)
        {
            Debug.Log("TOIMII");
        }

        _basicCollider = Instantiate(_basic, transform.position, Quaternion.identity) as GameObject;
        _basicCollider.transform.parent = _hero.transform;
        _basicCollider.transform.position = new Vector2(transform.position.x + 1, transform.position.y + 1);
        _basicCollider.SetActive(false);

        _specialCollider = Instantiate(_special, transform.position, Quaternion.identity) as GameObject;
        _specialCollider.transform.parent = _hero.transform;
        _specialCollider.transform.position = new Vector2(transform.position.x + 1, transform.position.y + 1);
        _specialCollider.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        RunTimers();
        CheckTimers();
    }

    public override float GetCooldown()
    {
        return 0f;
    }

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
        throw new NotImplementedException();
    }

    private void RunTimers()
    {
        if (_timer1 > 0)
            _timer1 -= Time.deltaTime;
        
    }

    private void CheckTimers()
    {
        if (_timer1 <= 0.6f)
            _basicCollider.SetActive(false);
        if (_timer1 <= 0)
        {            
            _basicActive = false;
            Debug.Log("HammerBasic Collider");
        }
        
    }
}
