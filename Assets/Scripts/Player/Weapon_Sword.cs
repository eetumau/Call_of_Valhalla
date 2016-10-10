﻿using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Player
{
    public class Weapon_Sword : Weapon
    {
               
        private GameObject _weapon1_1;
        private GameObject _weapon1_2;
        [SerializeField]
        GameObject _hero;
        private float _timer1;
        private float _timer2;

        private bool _firstHitActive;
        private bool _secondHitActive;

        [SerializeField]
        GameObject _sword;
        [SerializeField]
        GameObject _sword2;

        private void Awake()
        {               
                //Debug.Log("Täällä mennään");

                _weapon1_1 = Instantiate(_sword, transform.position, Quaternion.identity) as GameObject;
                _weapon1_1.transform.parent = _hero.transform;
                _weapon1_1.transform.position = new Vector2(transform.position.x + 1, transform.position.y + 1);
                _weapon1_1.SetActive(false);

                _weapon1_2 = Instantiate(_sword2, transform.position, Quaternion.identity) as GameObject;
                _weapon1_2.transform.parent = _hero.transform;
                _weapon1_2.transform.position = new Vector2(transform.position.x + 1, transform.position.y + 1);
                _weapon1_2.SetActive(false);
            
        }
        public override void BasicAttack()
        {
            
            if ( !_secondHitActive && !_firstHitActive)
            {

            }
            /*
            if (_timer1 <= 0)
            {
                _weapon1_1.SetActive(false);
                _weapon1Active = false;
            }
            if (_timer2 <= 0)
            {
                _weapon1_2.SetActive(false);
                _weapon2Active = false;
            }
            if (attack && _weapon1Active)
            {
                //Debug.Log("Weapon2");
                _timer1 = 0;
                _timer2 = 0.5f;
                _weapon1_1.SetActive(false);
                _weapon1_2.SetActive(true);
                _weapon2Active = true;
            }
            if (attack && !_weapon2Active)
            {
                _weapon1_1.SetActive(true);
                _weapon1Active = true;
                //Debug.Log("PAM");
                _timer1 = 1f;
            }

            if (_timer1 > 0)
            {
                _timer1 -= Time.deltaTime;
            }
            if (_timer2 > 0)
            {
                _timer2 -= Time.deltaTime;
            }
            */

        }

        public override void SpecialAttack()
        {

        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
