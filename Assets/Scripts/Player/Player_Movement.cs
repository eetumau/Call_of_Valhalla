using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour
{

    private Transform _playerTransform;
    private Rigidbody2D _playerRigidbody2D;
	public Animator animator;

    [SerializeField]
    private float _playerMoveSpeed;
    [SerializeField]
    private float _playerJumpForce;
    [SerializeField]
    private float _groundCheckRadius;
    [SerializeField]
    private Transform _groundCheckTransform;

<<<<<<< HEAD
        private bool _isGrounded;
        private Player_HP _hp;
=======
    private bool _isGrounded;
>>>>>>> 2b68356a4e66406912b6011ec671c1b747bc6080


<<<<<<< HEAD
        public Player_HP HP
        {
            get { return _hp; }
        }

        // Use this for initialization
        void Start()
        {

            _playerTransform = GetComponent<Transform>();
            _playerRigidbody2D = GetComponent<Rigidbody2D>();
            _weaponController = GetComponent<WeaponController>();
            _hp = GetComponent<Player_HP>();
=======
    // Use this for initialization
    void Start()
    {

        _playerTransform = GetComponent<Transform>();
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
>>>>>>> 2b68356a4e66406912b6011ec671c1b747bc6080

    }

    // Update is called once per frame
    void Update()
    {

        CheckIfGrounded();

        if (Input.GetAxis ("Horizontal") > 0) {
            _playerTransform.localScale = new Vector3(1, 1, 1);
        } 
        else if (Input.GetAxis ("Horizontal") < 0) {
           _playerTransform.localScale = new Vector3(-1, 1, 1); 
        }

		if (_isGrounded == true && Input.GetAxis ("Horizontal") != 0) {
			animator.SetInteger ("animState", 1);
		}
        else if (_isGrounded == false && _playerRigidbody2D.velocity.y > 0.5) {
            animator.SetInteger ("animState", 2);
        }
        else if (_isGrounded == false && _playerRigidbody2D.velocity.y < 0.5) {
            animator.SetInteger ("animState", 3);
        } 
		else {
			animator.SetInteger ("animState", 0);
		}

    }

    public void Move(float inputX)
    {

        _playerRigidbody2D.velocity = new Vector2(inputX * _playerMoveSpeed, _playerRigidbody2D.velocity.y);

    }

    public void Jump(bool _jump)
    {
        if (_isGrounded && _jump)
        {
            //_playerRigidbody2D.AddForce(new Vector2(0, _playerJumpForce));
            _playerRigidbody2D.velocity = new Vector2(_playerRigidbody2D.velocity.x, _playerJumpForce);
            _isGrounded = false;

        }

    }

    private void CheckIfGrounded()
    {
        _isGrounded = false;

        var colliders = Physics2D.OverlapCircleAll(_groundCheckTransform.position, _groundCheckRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Ground")
            {

                _isGrounded = true;
            }
        }

    }

}
