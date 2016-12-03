using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CallOfValhalla
{
    public class GameOverMenu : MonoBehaviour
    {

        private AudioSource _source;

        void Start()
        {
            _source = GetComponent<AudioSource>();
        }
        public void OnRestartPressed()
        {
            SoundManager.instance.PlaySound("button", _source);
            GameManager.Instance.GameOverUI.ToggleGameOverUI();
            GameManager.Instance.CameraFollow.Sepia.enabled = true;
            SceneManager.LoadScene(GameManager.Instance.Level);
        }

        public void OnExitPressed()
        {
            SoundManager.instance.PlaySound("button", _source);
            GameManager.Instance.GameOverUI.ToggleGameOverUI();
            GameManager.Instance.CameraFollow.Sepia.enabled = true;
            GameManager.Instance.MainMenu();
        }
    }
}