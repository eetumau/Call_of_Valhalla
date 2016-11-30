using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CallOfValhalla;
using CallOfValhalla.Player;
using System;

namespace CallOfValhalla.UI
{
    public class GUIManager : MonoBehaviour
    {

        [SerializeField]
        private Text _cooldown;

        private string _currentWeapon;
      

        public void SetWeapon(string wpn)
        {
            _currentWeapon = wpn;            
        }

        private void UpdateCooldown()
        {
            if (_currentWeapon.Equals("Sword"))
            {
                
            }
        }
        // Use this for initialization
        void Awake()
        {
            _cooldown.text = "10";
            _cooldown.gameObject.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            float tmp = GameManager.Instance.Player.WeaponController.GetCompletion();

            int tmp2 = (int)tmp;
            if (tmp <= 0)
            {
                _cooldown.text = "ready";
            } else 
            _cooldown.text = string.Format("{0}", tmp2);
            
        }


    }
}