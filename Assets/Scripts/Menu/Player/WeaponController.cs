﻿using UnityEngine;
using System.Collections;
using CallOfValhalla.UI;

namespace CallOfValhalla.Player
{
    public class WeaponController : MonoBehaviour
    {

        private Weapon_Sword _sword;

        private string Weapon1;
        private string Weapon2;
        
        private bool _basicAttack;
        private bool _specialAttack;
        private bool _weapon1Current;
        
        

        private Weapon _weapon1;
        private Weapon _weapon2;
        private string _currentWeapon;

        private void Awake()
        {
            Weapon1 = "Sword";
            Weapon2 = "Sword";
            SetWeapons(Weapon1, Weapon2);
            _weapon1Current = true;
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SetWeapons(string weapon1Name, string weapon2Name)
        {

            if (weapon1Name.Equals("Sword"))
            {
                _sword = GetComponent<Weapon_Sword>();
                _weapon1 = _sword;
                _currentWeapon = "Sword";
                GameManager.Instance.GUIManager.SetWeapon("Sword");
            }
            /*else if (weapon1Name.Equals("Sword"))
                _weapon1 = GetComponent<Weapon_Sword>();
            else
                _weapon1 = GetComponent<Weapon_Sword>();
                */
            
            //if (weapon2Name.Equals("Sword"))
            //    _weapon2 = new Weapon_Sword();

            /*
            else if (weapon2Name.Equals("Sword"))
                _weapon2 = GetComponent<Weapon_Sword>();
            else
                _weapon2 = GetComponent<Weapon_Sword>();
                */
        }

        public void Attack (bool basicAttack, bool specialAttack)
        {
            
            if (_weapon1Current && basicAttack && !specialAttack)            
                _weapon1.BasicAttack(basicAttack);
            else if (_weapon1Current && !basicAttack && specialAttack)
                _weapon1.SpecialAttack(specialAttack);
            else if (!_weapon1Current && basicAttack && !specialAttack)
                _weapon2.BasicAttack(basicAttack);
            if (!_weapon1Current && !basicAttack && specialAttack)
                _weapon1.SpecialAttack(specialAttack);
                
                
            
        }

        public float GetCooldown()
        {
           // if (_weapon1Current)
                return _weapon1.GetCooldown();
            
        }

        public void ChangeCurrentWeapon ()
        {

            //Debug.Log("change");
            //if (_weapon1Current)
            //    _weapon1Current = false;
            //else
            //    _weapon1Current = true;           
        }
    }
}