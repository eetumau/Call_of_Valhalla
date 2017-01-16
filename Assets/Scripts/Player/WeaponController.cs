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

        private string _currentWeapon;

        private void Awake()
        {
            SetWeapons();
            _weapon1Current = true;
            _animator = GetComponent<Animator>();
        }


        private void SetWeapons()
        {
            _sword = GetComponent<Weapon_Sword>();
            _currentWeapon = "Sword";

            _hammer = GetComponent<Weapon_Hammer>();

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
                _sword.BasicAttack(basicAttack);
            else if (_weapon1Current && !basicAttack && specialAttack)
                _sword.SpecialAttack(specialAttack);

            if (_hammerInGame) { 
                if (!_weapon1Current && basicAttack && !specialAttack)
                    _hammer.BasicAttack(basicAttack);
                if (!_weapon1Current && !basicAttack && specialAttack)
                    _hammer.SpecialAttack(specialAttack);
            }
        }

        public Weapon_Hammer GetHammer()
        {
            return _hammer;
        }

        public float GetCompletion()
        {
            if (_weapon1Current)
                return _sword.GetCompletion();
            else
                return _hammer.GetCompletion();
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


