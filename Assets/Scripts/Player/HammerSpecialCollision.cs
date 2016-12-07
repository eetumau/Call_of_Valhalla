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

    private float _smallStunTime    = 1f;
    private float _mediumStunTime   = 2f;
    private float _largeStunTime     = 3.5f;

    private int _smallDamage  = 2;
    private int _mediumDamage = 3;
    private int _largeDamage  = 6;

    // Use this for initialization
    private void Awake()
    {
        _hammer = GameManager.Instance.Player.WeaponController.GetHammer();
    }

    public void SetStunAndDamage (string charge)
    {
        if (charge.Equals("Small"))
        {
            _stunTime = _smallStunTime;
            _damage = _smallDamage;

        } else if (charge.Equals("Medium"))
        {
            _stunTime = _mediumStunTime;
            _damage = _mediumDamage;
        }
        else if (charge.Equals("Large"))
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
            
            _enemyMovement.Stun(_stunTime);
            _enemyHP.TakeDamage(_damage);                           
        }
    }
}
