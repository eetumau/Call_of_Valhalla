using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy
{
    public class Head : MonoBehaviour
    {
        [SerializeField]
        private GameObject _blood;

        private Transform _transform;
        

        // Use this for initialization
        void Start()
        {
            _transform = GetComponent<Transform>();
        }


        public void SpillBlood()
        {
            Instantiate(_blood, _transform.position + Vector3.up, _transform.rotation);

        }
    }
}