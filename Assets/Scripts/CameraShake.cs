using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _maxVolume;
    [SerializeField]
    private float _minVolume;


    private Camera _cam;

    private Vector3 _ogPosition;
    private Vector3 _newPosition;
    private float _randomVolume;
    private Vector3 _distance;

	// Use this for initialization
	void Start () {

        _cam = FindObjectOfType<Camera>();
        _ogPosition = _cam.transform.position;

    }
	
	public IEnumerator Shake(float duration)
    {

        _randomVolume = Random.Range(_minVolume, _maxVolume);
        _newPosition = new Vector3(_ogPosition.x + _randomVolume, _ogPosition.y + _randomVolume, _ogPosition.z);

        _distance = _cam.transform.position - _newPosition;

        if(_distance.magnitude > 0)
        {
            _cam.transform.position = Vector3.Lerp(_ogPosition, _newPosition, _speed * Time.deltaTime);
        }else
        {

        }

        yield return null;
    }
}
