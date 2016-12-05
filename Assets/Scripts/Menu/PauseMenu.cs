using UnityEngine;
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
            _musicToggle = _music.GetComponent<Toggle>();
            _sound = GameObject.Find("Sound");
            _soundToggle = _sound.GetComponent<Toggle>();
            _musicToggle.isOn = !SoundManager.instance.MusicMuted;
            _soundToggle.isOn = !SoundManager.instance.SoundMuted;
        }

        public void OnResumePressed()
        {
            GameManager.Instance.Pauser.TogglePause();
        }

        public void OnRestartPressed()
        {
            GameManager.Instance.Pauser.TogglePause();
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
    }
}