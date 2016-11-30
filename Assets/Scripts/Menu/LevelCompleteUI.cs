using UnityEngine;
using System.Collections;

namespace CallOfValhalla
{
    public class LevelCompleteUI : MonoBehaviour
    {

        private GameObject _levelCompleteUI;
        private Canvas _canvas;

        private bool _levelCompleted;

        // Use this for initialization
        void Start()
        {
            _canvas = GetComponentInChildren<Canvas>();
            _levelCompleteUI = _canvas.gameObject;
            GameManager.Instance.LevelCompleteUI = this;
            _levelCompleteUI.SetActive(false);
        }

        public void ToggleLevelCompleteUI()
        {
            _levelCompleted = !_levelCompleted;

            if (_levelCompleted)
            {
                _levelCompleteUI.SetActive(true);
                Time.timeScale = 0;
            }else
            {
                _levelCompleteUI.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}