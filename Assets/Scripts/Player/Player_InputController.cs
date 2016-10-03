using UnityEngine;
using System.Collections;

public class Player_InputController : MonoBehaviour {

    private float _inputX;
    private bool _jump;
    private bool _basicAttack;

    private Player_Movement _playerMovement;
    private Attack_Basic _attackBasic;

	// Use this for initialization
	void Start () {

        _playerMovement = GetComponent<Player_Movement>();
        _attackBasic = GetComponent<Attack_Basic>();
	
	}
	
	// Update is called once per frame
	void Update () {

        GetInput();

	}

    private void GetInput() {

        _inputX = Input.GetAxis("Horizontal");
        _jump = Input.GetButtonDown("Jump");
        _basicAttack = Input.GetButtonDown("Attack1");

        if(_inputX < 0 || _inputX > 0)
        {
            _playerMovement.Move(_inputX);
        }

        _attackBasic.Attack(_basicAttack);
            _playerMovement.Jump(_jump);
            
        
    }
}
