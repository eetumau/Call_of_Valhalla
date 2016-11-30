using UnityEngine;
using System.Collections;
using CallOfValhalla.Enemy;
using CallOfValhalla.Player;

public class SwordBasicCollision : MonoBehaviour {

    private Enemy_HP _enemyHP;
    private Enemy_Movement _enemyMovement;
    private Weapon_Sword _sword;
    private int _damage = 1;
    private float _specialCompletionPercent = 5f;

    // Use this for initialization
    void Awake()
    {
        _sword = FindObjectOfType<Weapon_Sword>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _enemyMovement = other.gameObject.GetComponentInParent<Enemy_Movement>();
            _enemyMovement.Knockback(250f, 250f);
            _enemyHP = other.gameObject.GetComponentInParent<Enemy_HP>();

            if (_enemyHP.HP > 0)
                _sword.AddCompletionByDamage(_specialCompletionPercent);

            _enemyHP.TakeDamage(_damage);
            
        }
    }

}
