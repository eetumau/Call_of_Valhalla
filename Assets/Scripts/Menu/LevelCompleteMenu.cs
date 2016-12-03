using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CallOfValhalla
{
    public class LevelCompleteMenu : MonoBehaviour
    {
        private Text _text;
        private AudioSource _source;
        // Use this for initialization
        void Start()
        {
            _text = GetComponentInChildren<Text>();
            _text.text = "Level " + GameManager.Instance.Level + " Completed!";
            _source = GetComponent<AudioSource>();
        }

        public void OnRestartPressed()
        {
            SoundManager.instance.PlaySound("button", _source);
            GameManager.Instance.LevelCompleteUI.ToggleLevelCompleteUI();
            SceneManager.LoadScene(GameManager.Instance.Level);
        }

        public void OnNextLevelPressed()
        {
            SoundManager.instance.PlaySound("button", _source);
            GameManager.Instance.LevelCompleteUI.ToggleLevelCompleteUI();
            GameManager.Instance.Level += 1;
            SceneManager.LoadScene(GameManager.Instance.Level);
        }

        public void OnExitPressed()
        {
            SoundManager.instance.PlaySound("button", _source);
            GameManager.Instance.ToSelectLevel = true;
            GameManager.Instance.LevelCompleteUI.ToggleLevelCompleteUI();
            GameManager.Instance.MainMenu();
        }
    }
}