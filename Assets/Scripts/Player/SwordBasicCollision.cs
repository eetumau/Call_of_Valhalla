﻿using UnityEngine;
using System.Collections;
using CallOfValhalla.Enemy;
using CallOfValhalla.Player;
using CallOfValhalla;

public class SwordBasicCollision : MonoBehaviour {

    private Enemy_HP _enemyHP;
    private Fenrir_HP _fenrirHP;
    private Enemy_Movement _enemyMovement;
    private Weapon_Sword _sword;
    private int _damage = 1;
    private float _specialCompletionPercent = 5f;
    private AudioSource _source;

    // Use this for initialization
    void Awake()
    {
        _sword = FindObjectOfType<Weapon_Sword>();
        _source = gameObject.AddComponent<AudioSource>();
        _source.playOnAwake = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _enemyHP = other.gameObject.GetComponentInParent<Enemy_HP>();

            if(_enemyHP == null)
            {
                _fenrirHP = other.gameObject.GetComponentInParent<Fenrir_HP>();
            }

            SoundManager.instance.PlaySound("sword_hit", _source, false);

            if (_enemyHP != null && !_enemyHP.thisIsABoss)
            {
                _enemyMovement = other.gameObject.GetComponentInParent<Enemy_Movement>();
                _enemyMovement.Knockback(250f, 250f);
            }
            
            if(_fenrirHP == null)
            {
                if (_enemyHP.HP > 0)
                    _sword.AddCompletionByDamage(_specialCompletionPercent);

                _enemyHP.TakeDamage(_damage);
            }else
            {
                if(_fenrirHP.HP > 0)
                {
                    _sword.AddCompletionByDamage(_specialCompletionPercent);
                }
                _fenrirHP.TakeDamage(_damage);
                
            }

            _fenrirHP = null;
            _enemyHP = null;
            _enemyMovement = null;
        }else if(other.gameObject.tag == "Head")
        {
            SoundManager.instance.PlaySound("sword_hit", _source, false);
            other.gameObject.GetComponent<Head>().SpillBlood();
        }
    }

}
