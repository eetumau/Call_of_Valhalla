using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;
using CallOfValhalla.States;
using UnityEngine.SceneManagement;

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

        public static StateManager StateManager
        {
            get; set;
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

            InitStateManager();
        }

        private void InitStateManager()
        {
            StateManager = new StateManager(new MainMenuState());
            StateManager.AddState(new GameState());
            StateManager.AddState(new GameOverState());

            //For testing purposes
            StateManager.AddState(new EetuTestState());
            StateManager.AddState(new TeemuTestState());
        }

        public void MainMenu()
        {
            StateManager.PerformTransition(TransitionType.GameOverToMainMenu);
        }

        public void Game()
        {
            StateManager.PerformTransition(TransitionType.MainMenuToGame);
        }

        public void GameOver()
        {
            Debug.Log("Game over");
            StateManager.PerformTransition(TransitionType.EetuToGameOver);
        }

        //For testing purposes
        public void Eetu()
        {
            StateManager.PerformTransition(TransitionType.MainMenuToEetu);
        }
 
        //For testing purposes
        public void Teemu()
        {
            StateManager.PerformTransition(TransitionType.MainMenuToTeemu);
        }


    }
}
