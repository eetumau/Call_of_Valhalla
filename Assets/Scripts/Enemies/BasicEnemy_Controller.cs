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

        public BasicEnemy_StateManager StateManager
        {
            get; set;
        }

        private bool _passive = false;

        // Use this for initialization
        void Start()
        {
            InitStateManager();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void InitStateManager()
        {
            StateManager = new BasicEnemy_StateManager(new BasicEnemy_PassiveState());
            StateManager.AddState(new BasicEnemy_PassiveState());
        }

        public void SetPassive()
        {

        }
    }
}