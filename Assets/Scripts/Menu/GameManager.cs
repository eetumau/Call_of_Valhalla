﻿using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;
using CallOfValhalla.States;
using UnityEngine.SceneManagement;
using CallOfValhalla.UI;

namespace CallOfValhalla
{
    public class GameManager : MonoBehaviour
    {

        private static GameManager _instance;
        private Pauser _pauser;
        private Checkpoint _checkPoint;
        private GUIManager _guiManager;
        private GameOverUI _gameOverUI;
        private LevelCompleteUI _levelCompleteUI;
        private Player_CameraFollow _cameraFollow;

        private Player_InputController _inputController;
        private Player_Movement _playerMovement;
        private Player_HP _playerHP;
        private int _level;
        private bool _toSelectLevel;

        [SerializeField]
        private int _levelCompleted;


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

        public GameOverUI GameOverUI
        {
            get { return _gameOverUI; }
            set { _gameOverUI = value; }
        }

        public LevelCompleteUI LevelCompleteUI
        {
            get { return _levelCompleteUI; }
            set { _levelCompleteUI = value; }
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

        public int LevelCompleted
        {
            get { return _levelCompleted; }
            set { _levelCompleted = value; }
        }

        public bool ToSelectLevel
        {
            get { return _toSelectLevel; }
            set { _toSelectLevel = value; }
        }

        public Player_CameraFollow CameraFollow
        {
            get { return _cameraFollow; }
            set { _cameraFollow = value; }
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
            _playerMovement = FindObjectOfType<Player_Movement>();
            _playerHP = FindObjectOfType<Player_HP>();

            InitStateManager();
            LoadGame();

        }

        private void InitStateManager()
        {
            StateManager = new StateManager(new MainMenuState());
            StateManager.AddState(new GameState());
            StateManager.AddState(new GameOverState());

        }

        public void MainMenu()
        {
            StateManager.PerformTransition(TransitionType.GameToMainMenu);
        }

        public void Game()
        {
            if(_level < 4)
            {
                SoundManager.instance.SetMusic("level_music_2");
            }else
            {
                SoundManager.instance.SetMusic("level_music_1");
            }
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
                    _gameOverUI.ToggleGameOverUI();

                }
            }
            else
            {
                _gameOverUI.ToggleGameOverUI();
            }
        }

        public void Save()
        {
            GameData data = new GameData();

            data.musicMuted = SoundManager.instance.MusicMuted;
            data.soundMuted = SoundManager.instance.SoundMuted;

            if(_levelCompleted > data.levelCompleted)
            {
                data.levelCompleted = _levelCompleted;

            }

            SaveSystem.Save(data);
        }

        public void LoadGame()
        {
            GameData data = SaveSystem.Load<GameData>();

            if (data != null)
            {
                SoundManager.instance.MusicMuted = data.musicMuted;
                SoundManager.instance.SoundMuted = data.soundMuted;

                if (_levelCompleted < data.levelCompleted)
                {
                    _levelCompleted = data.levelCompleted;

                }
            }
        }

        public void DeleteSaveData()
        {
            GameData data = new GameData();

            _levelCompleted = 0;

            data.musicMuted = false;
            data.soundMuted = false;
            data.levelCompleted = _levelCompleted;

            SaveSystem.Save(data);
        }

    }
}
