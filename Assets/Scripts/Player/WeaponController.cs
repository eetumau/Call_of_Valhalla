using UnityEngine;
using System.Collections;
using CallOfValhalla.UI;

namespace CallOfValhalla.Player
{
    public class WeaponController : MonoBehaviour
    {

        private Weapon_Sword _sword;
        private Weapon_Hammer _hammer;

        private bool _hammerInGame;
        private bool _basicAttack;
        private bool _specialAttack;
        public bool _weapon1Current;
        private Animator _animator;


        private Weapon _weapon1;
        private Weapon _weapon2;
        private string _currentWeapon;

        private void Awake()
        {
            SetWeapons();
            _weapon1Current = true;
            _animator = GetComponent<Animator>();
        }


        private void SetWeapons()
        {
            _weapon1 = GetComponent<Weapon_Sword>();
            _currentWeapon = "Sword";

            _weapon2 = GetComponent<Weapon_Hammer>();

            string tmp = Application.loadedLevelName;

            if (tmp.Equals("level1") || tmp.Equals("Level2") || tmp.Equals("Level3"))
                _hammerInGame = false;
            else
                _hammerInGame = true;
                        
        }

        public void EnableHammer()
        {
            _hammerInGame = true;
        }

        public void Attack(bool basicAttack, bool specialAttack)
        {

            if (_weapon1Current && basicAttack && !specialAttack)
                _weapon1.BasicAttack(basicAttack);
            else if (_weapon1Current && !basicAttack && specialAttack)
                _weapon1.SpecialAttack(specialAttack);

            if (_hammerInGame) { 
                if (!_weapon1Current && basicAttack && !specialAttack)
                    _weapon2.BasicAttack(basicAttack);
                if (!_weapon1Current && !basicAttack && specialAttack)
                    _weapon2.SpecialAttack(specialAttack);
            }


        }

        public Weapon_Hammer GetHammer()
        {
            return _hammer;
        }

        public float GetCompletion()
        {
            if (_weapon1Current)
                return _weapon1.GetCompletion();
            else
                return _weapon2.GetCompletion();
        }

        public void ChangeCurrentWeapon()
        {

            if (_hammerInGame)
            {
                if (_weapon1Current)
                {
                    _weapon1Current = false;
                    _animator.runtimeAnimatorController = Resources.Load("Hero_Mjölnir", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
                }
                else
                {
                    _weapon1Current = true;
                    _animator.runtimeAnimatorController = Resources.Load("Hero_Sword", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
                }
            }
        }
    }
}


