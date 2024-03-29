﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using CallOfValhalla.States;
using UnityEngine.UI;

namespace CallOfValhalla {
    public class PauseMenu : MonoBehaviour {

        private GameObject _music;
        private Toggle _musicToggle;
        private GameObject _sound;
        private Toggle _soundToggle;

        private void Start()
        {
            _music = GameObject.Find("Music");
            //_musicToggle = _music.GetComponent<Toggle>();
            _sound = GameObject.Find("Sound");
            //_soundToggle = _sound.GetComponent<Toggle>();
            //_musicToggle.isOn = !SoundManager.instance.MusicMuted;
            //_soundToggle.isOn = !SoundManager.instance.SoundMuted;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Continue"))
            {
                OnResumePressed();
            }
        }

        public void OnResumePressed()
        {
            GameManager.Instance.Pauser.TogglePause();
        }

        public void OnRestartPressed()
        {
            GameManager.Instance.Pauser.TogglePause();

            ResetMusic();
            SceneManager.LoadScene(GameManager.Instance.Level);

        }

        public void OnSettingsPressed()
        {
            GameManager.Instance.Pauser.ToggleSettings();
        }

        public void OnExitPressed()
        {
            GameManager.Instance.Pauser.TogglePause();
            GameManager.StateManager.PerformTransition(TransitionType.GameToMainMenu);
        }

        public void MuteMusic()
        {
            SoundManager.instance.ToggleMusic(_musicToggle.isOn);
        }



        public void MuteSound()
        {
            SoundManager.instance.ToggleSound(_soundToggle.isOn);
        }

        private void ResetMusic()
        {
            if (GameManager.StateManager.CurrentStateType == StateType.MainMenu)
            {
                SoundManager.instance.SetMusic("epic-bensound");
            }
            else if (GameManager.Instance.Level < 4)
            {
                SoundManager.instance.SetMusic("level_music_4");
            }
            else if (GameManager.Instance.Level > 3 && GameManager.Instance.Level < 10)
            {
                SoundManager.instance.SetMusic("level_music_5");
            }
            else
            {
                SoundManager.instance.SetMusic("level_music_3");
            }
        }
    }
}