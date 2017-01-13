using UnityEngine;
using System.Collections;
using CallOfValhalla.Enemy;
using CallOfValhalla;

public class HammerBasicCollision : MonoBehaviour {

    private Enemy_HP _enemyHP;
    private Fenrir_HP _fenrirHP;
    private Enemy_Movement _enemyMovement;
    private Weapon_Hammer _hammer;
    private float _SpecialCompletionPercent = 10f;
    private AudioSource _source;

    // Use this for initialization
    void Awake()
    {
        _hammer = FindObjectOfType<Weapon_Hammer>();
        _source = gameObject.AddComponent<AudioSource>();
        _source.playOnAwake = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            SoundManager.instance.PlaySound("sword_hit", _source, false);
            _enemyHP = other.gameObject.GetComponentInParent<Enemy_HP>();

            if(_enemyHP == null)
            {
                _fenrirHP = other.gameObject.GetComponentInParent<Fenrir_HP>();
            }

            if (_enemyHP != null && !_enemyHP.thisIsABoss)
            {
                _enemyMovement = other.gameObject.GetComponentInParent<Enemy_Movement>();
                _enemyMovement.Knockback(250f, 250f);
            }
            
            if(_fenrirHP == null)
            {
                if (_enemyHP.HP > 0)
                    _hammer.AddCompletionByDamage(_SpecialCompletionPercent);

                _enemyHP.TakeDamage(3);
            }else
            {
                if(_fenrirHP.HP > 0)
                {
                    _hammer.AddCompletionByDamage(_SpecialCompletionPercent);
                    _fenrirHP.TakeDamage(3);
                }
            }

            _fenrirHP = null;
            _enemyHP = null;
            _enemyMovement = null;
        }
        else if (other.gameObject.tag == "Head")
        {
            SoundManager.instance.PlaySound("sword_hit", _source, false);
            other.gameObject.GetComponent<Head>().SpillBlood();
        }
    }

}
