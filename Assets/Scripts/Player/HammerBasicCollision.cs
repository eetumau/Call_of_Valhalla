using UnityEngine;
using System.Collections;
using CallOfValhalla.Enemy;

public class HammerBasicCollision : MonoBehaviour {

    private Enemy_HP _enemyHP;
    private Enemy_Movement _enemyMovement;
    private Weapon_Hammer _hammer;
    private float _SpecialCompletionPercent = 10f;

    // Use this for initialization
    void Awake()
    {
        _hammer = FindObjectOfType<Weapon_Hammer>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _enemyMovement = other.gameObject.GetComponentInParent<Enemy_Movement>();
            _enemyMovement.Knockback(400, 250);
            _enemyHP = other.gameObject.GetComponentInParent<Enemy_HP>();
            
            if (_enemyHP.HP > 0)
                _hammer.AddCompletionByDamage(_SpecialCompletionPercent);

            _enemyHP.TakeDamage(3);
        }
    }

}
