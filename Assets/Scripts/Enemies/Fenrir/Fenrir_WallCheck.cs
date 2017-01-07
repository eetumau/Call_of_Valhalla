using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy
{
    public class Fenrir_WallCheck : MonoBehaviour
    {
        //Used to check if the enemy is flying to allow movement even when there is no ground ahead.
        [SerializeField]
        private bool _flying;

        private Fenrir_Movement _enemyMovement;
        private Transform _transform;
        private Fenrir_Controller _controller;


        private void Start()
        {
            _enemyMovement = GetComponentInParent<Fenrir_Movement>();
            _transform = GetComponent<Transform>();
            _controller = GetComponentInParent<Fenrir_Controller>();
        }


        private void OnCollisionEnter2D(Collision2D col)
        {

            if (col.gameObject.tag == "Ground")
            {
                if (_controller.IsPassive)
                {

                    _enemyMovement.Instance.ChangeDirection();
                }
            }
        }

        public bool CheckRight()
        {
            var allButIgnoreLinecast = ~(1 << 8);
            bool blocked = Physics2D.Linecast(_transform.position, new Vector3(_transform.position.x + 1, _transform.position.y + 0.3f, _transform.position.z), allButIgnoreLinecast);

            if (!blocked && !_flying)
            {
                blocked = Physics2D.Linecast(new Vector3(_transform.position.x + 1, _transform.position.y, _transform.position.z), new Vector3(_transform.position.x + 1, _transform.position.y - 3, _transform.position.z), allButIgnoreLinecast);
                Debug.DrawLine(new Vector3(_transform.position.x + 1, _transform.position.y, _transform.position.z), new Vector3(_transform.position.x + 1, _transform.position.y - 3, _transform.position.z));
                if (!blocked)
                {
                    blocked = true;
                }
                else
                {
                    blocked = false;
                }
            }

            return blocked;
        }

        public bool CheckLeft()
        {
            var allButIgnoreLinecast = ~(1 << 8);
            bool blocked = Physics2D.Linecast(_transform.position, new Vector3(_transform.position.x - 1, _transform.position.y + 0.3f, _transform.position.z), allButIgnoreLinecast);


            if (!blocked && !_flying)
            {
                blocked = Physics2D.Linecast(new Vector3(_transform.position.x - 1, _transform.position.y, _transform.position.z), new Vector3(_transform.position.x - 1, _transform.position.y - 3, _transform.position.z), allButIgnoreLinecast);
                Debug.DrawLine(new Vector3(_transform.position.x - 1, _transform.position.y, _transform.position.z), new Vector3(_transform.position.x - 1, _transform.position.y - 3, _transform.position.z));
                if (!blocked)
                {

                    blocked = true;
                }
                else
                {
                    blocked = false;
                }
            }

            return blocked;
        }
    }
}