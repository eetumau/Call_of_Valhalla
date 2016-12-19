using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

namespace CallOfValhalla.Player
{
    public class Player_CameraFollow : MonoBehaviour
    {

        [SerializeField]
        private float _rightBorderOffset;
        [SerializeField]
        private float _leftBorderOffset;
        [SerializeField]
        private float _topBorderOffset;
        [SerializeField]
        private float _bottomBorderOffset;
        [SerializeField]
        private Transform _playerTransform;

        private Vector3 _tmpLocation;
        private Transform _transform;
        private SepiaTone _sepiaEffect;
        private bool _cameraCentering;
        [SerializeField] private float _centeringSpeed;
        private float _resetTime = 1f;

        public SepiaTone Sepia
        {
            get { return _sepiaEffect; }
        }

        // Use this for initialization
        void Start()
        {
            GameManager.Instance.CameraFollow = this;
            _transform = GetComponent<Transform>();
            _sepiaEffect = GetComponent<SepiaTone>();
            _sepiaEffect.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_cameraCentering)
                MoveCamera();
            else
                CenterCamera();
                
        }

        private void MoveCamera()
        {
            if (_playerTransform.position.x >= _transform.position.x + _rightBorderOffset)
            {
                _transform.position = new Vector3(_playerTransform.position.x - _rightBorderOffset, _transform.position.y, _transform.position.z);
            }

            if (_playerTransform.position.x <= _transform.position.x - _leftBorderOffset)
            {
                _transform.position = new Vector3(_playerTransform.position.x + _leftBorderOffset, _transform.position.y, _transform.position.z);
            }

            if (_playerTransform.position.y >= _transform.position.y + _topBorderOffset)
            {
                _transform.position = new Vector3(_transform.position.x, _playerTransform.position.y - _topBorderOffset, _transform.position.z);
            }

            if (_playerTransform.position.y <= _transform.position.y - _bottomBorderOffset)
            {
                _transform.position = new Vector3(_transform.position.x, _playerTransform.position.y + _bottomBorderOffset, _transform.position.z);
            }
        }

        public void StartCenteringCamera()
        {

            _cameraCentering = true;
            StartCoroutine(ResetCamera());
        }

        private void CenterCamera()
        {
            float step = _centeringSpeed * Time.deltaTime;
            _tmpLocation = new Vector3(_playerTransform.position.x, _playerTransform.position.y + _bottomBorderOffset, _transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, _tmpLocation, step);
        }

        private IEnumerator ResetCamera()
        {
            yield return new WaitForSeconds(_resetTime);
            _cameraCentering = false;
        }

        private IEnumerator CameraDelay()
        {
            yield return new WaitForSeconds(0.5f);
            _cameraCentering = true;
        }
    }
}