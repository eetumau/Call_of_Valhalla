using UnityEngine;
using System.Collections;

namespace CallOfValhalla
{
    public class CameraShake : MonoBehaviour
    {

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
        void Start()
        {

            _cam = FindObjectOfType<Camera>();

        }

        public IEnumerator Shake(float duration)
        {
            float dur = duration;

            while (dur > 0)
            {
                _randomVolume = Random.Range(_minVolume, _maxVolume);
                _newPosition = new Vector3(_cam.transform.position.x + _randomVolume, _cam.transform.position.y + _randomVolume, _cam.transform.position.z);
                _distance = _cam.transform.position - _newPosition;


                _cam.transform.position = Vector3.Lerp(_cam.transform.position, _newPosition, _speed * Time.deltaTime);



                dur -= Time.deltaTime;
                yield return null;
            }


        }
    }
}