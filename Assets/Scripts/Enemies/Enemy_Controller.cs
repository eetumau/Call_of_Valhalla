using UnityEngine;


namespace CallOfValhalla.Enemy
{
    public class Enemy_Controller : MonoBehaviour
    {
        private Enemy_Controller _instance;
        private Enemy_Movement _movement;
        private BasicEnemy_WallCheck _wallCheck;
        private Enemy_HP _hp;
        private Enemy_SearchForPlayer _searchForPlayer;
        private Enemy_Attack _attack;
        private Enemy_AttackTrigger _attackTrigger;

        private bool _isPassive = true;
        private bool _isAggressive = false;
        private bool _isSearchingForPlayer = false;
        private Vector3 _lastSeenPlayerPos;
        private bool _inAttackRange;

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

        public bool InAttackRange
        {
            get { return _inAttackRange; }
            set { _inAttackRange = value; }
        }

        private void Start()
        {
            _instance = this;
            _hp = GetComponent<Enemy_HP>();
            _movement = GetComponent<Enemy_Movement>();
            _searchForPlayer = GetComponent<Enemy_SearchForPlayer>();
            _wallCheck = GetComponentInChildren<BasicEnemy_WallCheck>();
            _attack = GetComponentInChildren<Enemy_Attack>();
            _attackTrigger = GetComponentInChildren<Enemy_AttackTrigger>();
        }

        private void Update()
        {

        }

        public void TurnToPassive()
        {
            _isPassive = true;
            _isAggressive = false;
            _isSearchingForPlayer = false;
        }

        public void TurnToAggressive()
        {
            _isAggressive = true;
            _isPassive = false;
            _isSearchingForPlayer = false;
        }

        public void TurnToSearchingForPlayer()
        {
            _isSearchingForPlayer = true;
            _isAggressive = false;
            _isPassive = false;
        }

        public void Die()
        {
            _movement.enabled = false;
            _searchForPlayer.enabled = false;
            _hp.enabled = false;
            _wallCheck.enabled = false;
            _instance.enabled = false;
            _attack.enabled = false;
            _attackTrigger.enabled = false;
            
        }

    }

}