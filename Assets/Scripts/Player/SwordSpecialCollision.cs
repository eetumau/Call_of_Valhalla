using UnityEngine;
using System.Collections;
using CallOfValhalla.Enemy;

public class SwordSpecialCollision : MonoBehaviour {

    private Enemy_HP _enemyHP;
    private Enemy_Movement _enemyMovement;
    private Fenrir_HP _fenrirHP;
    private Fenrir_Movement _fenrirMovement;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
           
            _enemyMovement = other.gameObject.GetComponentInParent<Enemy_Movement>();
            _enemyHP = other.gameObject.GetComponentInParent<Enemy_HP>();

            if (_enemyHP != null)
            {
                if(_enemyMovement != null)
                {
                    _enemyMovement.Knockback(250f, 250f);

                }
                _enemyHP.TakeDamage(10);
            }else
            {
                _fenrirHP = other.gameObject.GetComponentInParent<Fenrir_HP>();
                _fenrirMovement = other.gameObject.GetComponentInParent<Fenrir_Movement>();

                _fenrirMovement.Knockback(250f, 250f);
                _fenrirHP.TakeDamage(10);
            }


            _fenrirHP = null;
            _enemyHP = null;
            _enemyMovement = null;

        }
        else if (other.gameObject.tag == "Head")
        {
            other.gameObject.GetComponent<Head>().SpillBlood();
        }

    }

}
