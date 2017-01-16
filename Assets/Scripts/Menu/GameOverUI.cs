using UnityEngine;
using System.Collections;

namespace CallOfValhalla
{
    public class GameOverUI : MonoBehaviour
    {
        private GameObject _gameOverUI;
        private Canvas _canvas;
        private AudioSource _source;
        private Pauser _pauser;

        private bool _gameOver = false;

        // Use this for initialization
        void Start()
        {
            _source = GetComponent<AudioSource>();
            GameManager.Instance.GameOverUI = this;
            _canvas = GetComponentInChildren<Canvas>();
            _pauser = FindObjectOfType<Pauser>();
            _gameOverUI = _canvas.gameObject;
            _gameOverUI.SetActive(false);
        }

        public void ToggleGameOverUI()
        {
            _gameOver = !_gameOver;
            _pauser._gameOver = _gameOver;

            if (_gameOver)
            {
                _gameOverUI.SetActive(true);
            }else
            {
                SoundManager.instance.PlaySound("button", _source, false);
                Time.timeScale = 1;
                _gameOverUI.SetActive(false);
            }
        }
    }
}