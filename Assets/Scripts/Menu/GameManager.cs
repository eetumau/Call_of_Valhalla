using UnityEngine;
using System.Collections;

namespace CallOfValhalla
{
    public class GameManager : MonoBehaviour
    {

        private static GameManager _instance;
        private Pauser _pauser;
        private Player_InputController _inputController;
        private Player_Movement _playerMovement;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                }
                return _instance;
            }
        }

        public Player_Movement Player
        {
            get
            {
                if (_playerMovement == null)
                {
                    _playerMovement = FindObjectOfType<Player_Movement>();
                }
                return _playerMovement;
            }
        }

        public Pauser Pauser
        {
            get { return _pauser; }
        }

        // Use this for initialization
        void Awake()
        {
            if(_instance == null)
            {
                DontDestroyOnLoad(gameObject);
                _instance = this;
                Init();
            }else if(_instance != this)
            {
                Destroy(this);
            }
        }

        private void Init()
        {
            _pauser = GetComponent<Pauser>();
            _playerMovement = FindObjectOfType<Player_Movement>();
            //_inputController = FindObjectOfType<Player_InputController>();
        }

    }
}
