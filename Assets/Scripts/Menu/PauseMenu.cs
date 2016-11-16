using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using CallOfValhalla.States;

namespace CallOfValhalla {
    public class PauseMenu : MonoBehaviour {


        private void Start()
        {
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

        public void OnExitPressed()
        {
            GameManager.Instance.Pauser.TogglePause();
            GameManager.StateManager.PerformTransition(TransitionType.GameToMainMenu);
        }
    }
}