using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy
{
    public class BasicEnemy_WallCheck : MonoBehaviour
    {

        private Enemy_Movement _enemyMovement;
        private Transform _transform;
        private BasicEnemy_WallCheck _instance;

        public BasicEnemy_WallCheck Instance
        {
            get { return _instance; }
        }

        private void Start()
        {
            _instance = this;
            _enemyMovement = GetComponentInParent<Enemy_Movement>();
            _transform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {

        }

        private void OnCollisionEnter2D(Collision2D col)
        {

            if (col.gameObject.tag == "Ground")
            {

                _enemyMovement.Instance.ChangeDirection();
            }
        }

        public bool CheckRight()
        {
            var allButIgnoreLinecast = ~(1 << 8);
            var blocked = Physics2D.Raycast(_transform.position, _transform.right, 2, allButIgnoreLinecast);
            return blocked;
        }

        public bool CheckLeft()
        {
            var allButIgnoreLinecast = ~(1 << 8);
            var blocked = Physics2D.Linecast(_transform.position, -_transform.right, 2, allButIgnoreLinecast);
            return blocked;
        }
    }
}