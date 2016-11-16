﻿using UnityEngine;
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
        private Checkpoint _checkPoint;
        
        private Player_InputController _inputController;
        private Player_Movement _playerMovement;
        private Player_HP _playerHP;
        private int _level;

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
            set { _pauser = value; }
        }

        public Checkpoint CheckPoint
        {
            set { _checkPoint = value; }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
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
            _pauser = FindObjectOfType<Pauser>();
            _playerMovement = FindObjectOfType<Player_Movement>();
            _playerHP = FindObjectOfType<Player_HP>();

            InitStateManager();
        }

        private void InitStateManager()
        {
            StateManager = new StateManager(new MainMenuState());
            StateManager.AddState(new GameState());
            StateManager.AddState(new GameOverState());

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
            if(_checkPoint != null) {
                if (_checkPoint.Activated)
                {
                    _playerMovement.HP.enabled = true;
                    _playerMovement.HP.Respawn(_checkPoint.SpawnPoint);
                    _checkPoint.DestroyCheckPoint();
                }else
                {
                    StateManager.PerformTransition(TransitionType.GameToMainMenu);

                }
            }
            else
            {
                StateManager.PerformTransition(TransitionType.GameToMainMenu);

            }
        }

    }
}
