using UnityEngine;
using System.Collections;
using CallOfValhalla.Enemy;
using CallOfValhalla;

public class HammerSpecialCollision : MonoBehaviour {

    private Enemy_HP _enemyHP;
    private Enemy_Movement _enemyMovement;
    private Weapon_Hammer _hammer;
    

    // Use this for initialization
    void Start()
    {
        _hammer = GameManager.Instance.Player.WeaponController.GetHammer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")  {
            _enemyMovement = other.gameObject.GetComponentInParent<Enemy_Movement>();
            _enemyHP = other.gameObject.GetComponentInParent<Enemy_HP>();
            _enemyMovement.Knockback(0, 0);
            
            if (_hammer._airSpecialCollision)
            {
                _enemyMovement.Stun(3f);
                _enemyHP.TakeDamage(1);
            }
            else
            {
                _enemyMovement.Stun(1.5f);
                _enemyHP.TakeDamage(0);
            }                       
        }
    }
}
