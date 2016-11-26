using UnityEngine;
using System.Collections;
using CallOfValhalla;

namespace CallOfValhalla.Player
{
    public class Player_InputController : MonoBehaviour
    {

        private float _inputX;
        private bool _jump;
        private bool _basicAttack;
        private bool _specialAttack;
        private bool _changeCurrentWeapon;


        private Player_Movement _playerMovement;


        // Use this for initialization
        void Start()
        {

            //_playerMovement = GetComponent<Player_Movement>();

        }

        // Update is called once per frame
        void Update()
        {

            GetInput();

        }

        private void GetInput()
        {

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                GameManager.Instance.Pauser.TogglePause();
            }

            _inputX = Input.GetAxis("Horizontal");
            _jump = Input.GetButtonDown("Jump");
            _basicAttack = Input.GetButtonDown("Attack1");
            _specialAttack = Input.GetButtonDown("Attack2");
            _changeCurrentWeapon = Input.GetButtonDown("ChangeWeapon");



            if (_basicAttack || _specialAttack)
                GameManager.Instance.Player.WeaponController.Attack(_basicAttack, _specialAttack);
            if (_changeCurrentWeapon)
                GameManager.Instance.Player.WeaponController.ChangeCurrentWeapon();
            

            if (_inputX < 0 || _inputX > 0)
            {
                GameManager.Instance.Player.Move(_inputX);
            }            

            GameManager.Instance.Player.Jump(_jump);
            
        }        
    }
}