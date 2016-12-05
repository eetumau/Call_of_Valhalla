using UnityEngine;
using System.Collections;
using CallOfValhalla.Enemy;
using CallOfValhalla;

public class HammerSpecialCollision : MonoBehaviour {

    private Enemy_HP _enemyHP;
    private Enemy_Movement _enemyMovement;
    private Weapon_Hammer _hammer;
    private float _stunTime;
    private int _damage;

    private float _smallStunTime    = 0.5f;
    private float _mediumStunTime   = 1.5f;
    private float _largeStunTime     = 2.5f;

    private int _smallDamage  = 1;
    private int _mediumDamage = 2;
    private int _largeDamage  = 4;

    // Use this for initialization
    private void Awake()
    {
        _hammer = GameManager.Instance.Player.WeaponController.GetHammer();
    }

    public void SetStunAndDamage (float chargeTime)
    {
        if (chargeTime > 0 && chargeTime <= 0.75f)
        {
            _stunTime = _smallStunTime;
            _damage = _smallDamage;

        } else if (chargeTime > 0.75f && chargeTime <= 1.5f)
        {
            _stunTime = _mediumStunTime;
            _damage = _mediumDamage;
        }
        else
        {
            _stunTime = _largeStunTime;
            _damage = _largeDamage;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")  {
            _enemyMovement = other.gameObject.GetComponentInParent<Enemy_Movement>();
            _enemyHP = other.gameObject.GetComponentInParent<Enemy_HP>();
            _enemyMovement.Knockback(0, 0);
            
            _enemyMovement.Stun(_stunTime);
            _enemyHP.TakeDamage(_damage);                           
        }
    }
}
