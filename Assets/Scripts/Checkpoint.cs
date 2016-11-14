using UnityEngine;
using System.Collections;

namespace CallOfValhalla
{
    public class Checkpoint : MonoBehaviour
    {

        [SerializeField]
        private Sprite _activatedSprite;
        [SerializeField]
        private Sprite _destroyedSprite;

        private bool _activated = false;
        private bool _used = false;
        private Transform _transform;
        private SpriteRenderer _renderer;

        public Transform SpawnPoint
        {
            get { return _transform; }
        }

        public bool Activated
        {
            get { return _activated; }
           
        }


        // Use this for initialization
        void Start()
        {
            _transform = GetComponent<Transform>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!_used)
            {
                if (other.gameObject.tag == "Player")
                {
                    _activated = true;
                    _renderer.sprite = _activatedSprite;
                    GameManager.Instance.CheckPoint = this;
                }
            }
        }

        public void DestroyCheckPoint()
        {
            _activated = false;
            _used = true;
            _renderer.sprite = _destroyedSprite;
        }
    }
}