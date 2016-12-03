using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using CallOfValhalla.States;

namespace CallOfValhalla {
    public class PauseMenu : MonoBehaviour {

        private AudioSource _source;

        void Start()
        {
            _source = GetComponent<AudioSource>();
        }

        public void OnResumePressed()
        {
            SoundManager.instance.PlaySound("button", _source);
            GameManager.Instance.Pauser.TogglePause();
        }

        public void OnRestartPressed()
        {
            SoundManager.instance.PlaySound("button", _source);
            GameManager.Instance.Pauser.TogglePause();
            SceneManager.LoadScene(GameManager.Instance.Level);
        }

        public void OnExitPressed()
        {
            SoundManager.instance.PlaySound("button", _source);
            GameManager.Instance.Pauser.TogglePause();
            GameManager.StateManager.PerformTransition(TransitionType.GameToMainMenu);
        }
    }
}