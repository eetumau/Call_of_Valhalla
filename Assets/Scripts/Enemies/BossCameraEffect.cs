using UnityEngine;
using System.Collections;
using CallOfValhalla.Enemy;
using CallOfValhalla.Player;

public class BossCameraEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject _boss;
    [SerializeField]
    private float _cameraSpeed;
    [SerializeField]
    private float _cameraResetTime;
    [SerializeField]
    private float _cameraDelay;

    private Player_CameraFollow _cameraScript;
    private Player_InputController _playerInput;
    private Enemy_HP _enemyHp;
    private Fenrir_HP _fenrirHp;
    private bool _bossDead;

	// Use this for initialization
	void Awake ()
	{
	    _enemyHp = _boss.GetComponent<Enemy_HP>();
	    _playerInput = FindObjectOfType<Player_InputController>();
        

	    if (_enemyHp == null)
	    {
            Debug.Log("NUULLL");
	        _fenrirHp = _boss.GetComponent<Fenrir_HP>();
	    }
	    _cameraScript = FindObjectOfType<Player_CameraFollow>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (_enemyHp != null)
	    {
	        if (_enemyHp.HP <= 0 && !_bossDead)
	        {
	            _bossDead = true;
	            _cameraScript.DecreaseCameraSlowly();
                _playerInput.DisableControls(true);
	            StartCoroutine(Delay());
	        }
	    }
	    else
	    {
            if (_fenrirHp.HP <= 0 && !_bossDead)
            {
                _bossDead = true;
                _cameraScript.DecreaseCameraSlowly();
                _playerInput.DisableControls(true);
                StartCoroutine(Delay());
            }
        }
	    
	}

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_cameraDelay);
        _cameraScript.MoveCamerahere(transform.position, _cameraSpeed, _cameraResetTime);
    }
}
