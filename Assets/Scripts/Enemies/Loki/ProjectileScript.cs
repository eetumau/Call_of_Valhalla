using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;

public class ProjectileScript : MonoBehaviour {

    private Rigidbody2D _rigidBody;
    private GameObject _player;
    private Player_HP _playerHP;
    private Transform _playerTransform;
    private Transform _startingPointTransform;
    private Vector3 _aimPoint;
    private int _speed = 10;

	// Use this for initialization
	void Awake () {

        _startingPointTransform = GameObject.Find("ProjectileStartingPoint").GetComponent<Transform>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("HeroSword_0");
        _playerTransform = _player.GetComponent<Transform>();
    }
	
	void OnEnable()
    {
        transform.position = _startingPointTransform.position;
        _aimPoint = _playerTransform.position;
        _aimPoint.y += 1;
        transform.LookAt(_aimPoint);
        _rigidBody.velocity = transform.forward * _speed;
        StartCoroutine(Disable(4));        
    }

    public void SetSpeed(int speed)
    {
        _speed = speed;
    }

    private IEnumerator Disable(float howLong)
    {
        yield return new WaitForSeconds(howLong);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _rigidBody.velocity = new Vector3(0, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _playerHP = other.GetComponent<Player_HP>();

        if (other.gameObject.tag == "Player")
        {
                _playerHP.TakeDamage(3);
        }


    }
    
}
