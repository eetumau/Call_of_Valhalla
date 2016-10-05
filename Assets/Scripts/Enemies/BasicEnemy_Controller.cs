using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy
{
    public class BasicEnemy_Controller : MonoBehaviour
    {
        
        private static BasicEnemy_Controller _instance;

        public static BasicEnemy_Controller Instance
        {
            get { return _instance; }
        }


        [SerializeField]
        private float _aggroRadius;
        private Transform _transform;
        private bool _passive = false;
        private BasicEnemy_PassiveState _passiveState;

        public BasicEnemy_PassiveState PassiveState
        {
            get { return _passiveState; }
        }

        // Use this for initialization
        void Start()
        {
            _transform = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void SetPassive()
        {
            _passive = true;

        }

        private void SearchForPlayer()
        {
            var allButIgnoreLinecast = ~(1 << 8);

            var colliders = Physics2D.OverlapCircleAll(_transform.position, _aggroRadius);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.tag == "Player")
                {

                    if (!Physics2D.Linecast(_transform.position + _transform.up / 2, colliders[i].transform.position + colliders[i].transform.up / 2, allButIgnoreLinecast))
                    {
                        Debug.DrawLine(_transform.position + _transform.up / 2, colliders[i].transform.position + colliders[i].transform.up / 2);

                        _passive = false;

                    }
                }
            }
        }
    }
}