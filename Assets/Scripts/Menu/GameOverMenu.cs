using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CallOfValhalla
{
    public class GameOverMenu : MonoBehaviour
    {

        public void OnRestartPressed()
        {
            GameManager.Instance.GameOverUI.ToggleGameOverUI();
            SceneManager.LoadScene(GameManager.Instance.Level);
        }

        public void OnExitPressed()
        {
            GameManager.Instance.GameOverUI.ToggleGameOverUI();
            GameManager.Instance.MainMenu();
        }
    }
}