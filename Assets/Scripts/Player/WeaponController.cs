using UnityEngine;
using System.Collections;
using CallOfValhalla.UI;

namespace CallOfValhalla.Player
{
    public class WeaponController : MonoBehaviour
    {

        private Weapon_Sword _sword;
        private Weapon_Hammer _hammer;

        private string Weapon1;
        private string Weapon2;

        private bool _basicAttack;
        private bool _specialAttack;
        public bool _weapon1Current;
        private Animator _animator;


        private Weapon _weapon1;
        private Weapon _weapon2;
        private string _currentWeapon;

        private void Awake()
        {
            Weapon1 = "Sword";
            Weapon2 = "Hammer";
            SetWeapons(Weapon1, Weapon2);
            _weapon1Current = true;
            _animator = GetComponent<Animator>();

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

            }

            if (weapon2Name.Equals("Hammer"))
            {
                _hammer = GetComponent<Weapon_Hammer>();
                _weapon2 = _hammer;
            }

        }

        public void Attack(bool basicAttack, bool specialAttack)
        {

            if (_weapon1Current && basicAttack && !specialAttack)
                _weapon1.BasicAttack(basicAttack);
            else if (_weapon1Current && !basicAttack && specialAttack)
                _weapon1.SpecialAttack(specialAttack);
            else if (!_weapon1Current && basicAttack && !specialAttack)
                _weapon2.BasicAttack(basicAttack);
            if (!_weapon1Current && !basicAttack && specialAttack)
                _weapon2.SpecialAttack(specialAttack);



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

            Debug.Log("change");
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


