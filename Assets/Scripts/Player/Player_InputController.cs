using UnityEngine;
using System.Collections;

public class Player_InputController : MonoBehaviour {

    private float _inputX;
    private bool _jump;

    private Player_Movement _playerMovement;

	// Use this for initialization
	void Start () {

        _playerMovement = GetComponent<Player_Movement>();
	
	}
	
	// Update is called once per frame
	void Update () {

        GetInput();

	}

    private void GetInput() {

        _inputX = Input.GetAxis("Horizontal");
        _jump = Input.GetButtonDown("Jump");

        if(_inputX < 0 || _inputX > 0)
        {
            _playerMovement.Move(_inputX);
        }

        if (_jump)
        {
            _playerMovement.Jump();
        }
    }
}
