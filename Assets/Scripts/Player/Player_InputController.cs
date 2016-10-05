using UnityEngine;
using System.Collections;

namespace CallOfValhalla
{
    public class Player_InputController : MonoBehaviour
    {

        private float _inputX;
        private bool _jump;
        private bool _basicAttack;
        private bool _specialAttack;
        private string _weapon1Name = "Sword";
        private string _weapon2Name = "Sword";

        private Player_Movement _playerMovement;
        private Weapon _weapon1;
        private Weapon _weapon2;

        // Use this for initialization
        void Start()
        {

            //_playerMovement = GetComponent<Player_Movement>();
            SetWeapons();

        }

        // Update is called once per frame
        void Update()
        {

            GetInput();

        }

        private void GetInput()
        {

            if (Input.GetKeyUp(KeyCode.P))
            {
                GameManager.Instance.Pauser.TogglePause();
            }

            _inputX = Input.GetAxis("Horizontal");
            _jump = Input.GetButtonDown("Jump");
            _basicAttack = Input.GetButtonDown("Attack1");
            _specialAttack = Input.GetButtonDown("Attack2");

            if (_inputX < 0 || _inputX > 0)
            {
                GameManager.Instance.Player.Move(_inputX);
            }


            //_weapon1.BasicAttack(_basicAttack);
            //_weapon1.SpecialAttack(_specialAttack);

            //_weapon2.BasicAttack(_basicAttack);
            //_weapon2.SpecialAttack(_specialAttack);

            GameManager.Instance.Player.Jump(_jump);


        }

        private void SetWeapons()
        {
            if (_weapon1Name.Equals("Sword"))
                _weapon1 = GetComponent<Weapon_Sword>();
            else if (_weapon1Name.Equals("Sword"))
                _weapon1 = GetComponent<Weapon_Sword>();
            else
                _weapon1 = GetComponent<Weapon_Sword>();

            if (_weapon2Name.Equals("Sword"))
                _weapon2 = GetComponent<Weapon_Sword>();
            else if (_weapon2Name.Equals("Sword"))
                _weapon2 = GetComponent<Weapon_Sword>();
            else
                _weapon2 = GetComponent<Weapon_Sword>();
        }
    }
}