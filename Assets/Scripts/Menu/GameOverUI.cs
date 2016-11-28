using UnityEngine;
using System.Collections;

namespace CallOfValhalla
{
    public class GameOverUI : MonoBehaviour
    {
        private GameObject _gameOverUI;
        private Canvas _canvas;

        private bool _gameOver = false;

        // Use this for initialization
        void Start()
        {
            GameManager.Instance.GameOverUI = this;
            _canvas = GetComponentInChildren<Canvas>();
            _gameOverUI = _canvas.gameObject;
            _gameOverUI.SetActive(false);
        }

        public void ToggleGameOverUI()
        {
            _gameOver = !_gameOver;

            if (_gameOver)
            {
                _gameOverUI.SetActive(true);
            }else
            {
                Time.timeScale = 1;
                _gameOverUI.SetActive(false);
            }
        }
    }
}