using UnityEngine;


namespace CallOfValhalla.Enemy
{
    public class Enemy_Controller : MonoBehaviour
    {
        private Enemy_Controller _instance;

        private bool _isPassive = true;
        private bool _isAggressive;
        private bool _isSearchingForPlayer;
        private Vector3 _lastSeenPlayerPos;

        public Enemy_Controller Instance
        {
            get { return _instance; }
        }

        public bool IsPassive
        {
            get { return _isPassive; }
        }

        public bool IsAggressive
        {
            get { return _isAggressive; }
        }

        public bool IsSearchingForPlayer
        {
            get { return _isSearchingForPlayer; }
        }

        public Vector3 LastSeenPlayerPos
        {
            get { return _lastSeenPlayerPos; }
            set { _lastSeenPlayerPos = value; }
        }

        private void Start()
        {
            _instance = this;
        }

        private void Update()
        {

        }

        public void TurnToPassive()
        {
            _isPassive = true;
            _isAggressive = false;
        }

        public void TurnToAggressive()
        {
            _isAggressive = true;
            _isPassive = false;
        }

        public void TurnToSearchingForPlayer()
        {
            _isSearchingForPlayer = true;
            _isAggressive = false;
            _isPassive = false;
        }

    }

}