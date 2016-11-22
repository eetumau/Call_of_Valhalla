using UnityEngine;
using System.Collections;
using CallOfValhalla.Enemy;

public class HammerSpecialCollision : MonoBehaviour {

    private Enemy_HP _enemyHP;
    private Enemy_Movement _enemyMovement;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {


            _enemyMovement = other.gameObject.GetComponentInParent<Enemy_Movement>();
            _enemyMovement.Knockback(0, 0);
            _enemyHP = other.gameObject.GetComponentInParent<Enemy_HP>();
            _enemyHP.TakeDamage(4);
        }
    }

}
