using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CallOfValhalla
{
    public class Pauser : MonoBehaviour
    {

        private GameObject _pauseCanvasGO;
        private Canvas _canvas;
        private AudioSource _source;
        private GameObject _settingsPanel;
        private bool _showSettings;
        public bool _gameOver;
        public bool _levelCompleted;



        private bool _paused = false;

        private void Start()
        {
            
            _settingsPanel = GameObject.Find("SettingsPanel");
            _source = GetComponent<AudioSource>();
            GameManager.Instance.Pauser = this;
            _canvas = GetComponentInChildren<Canvas>();
            _pauseCanvasGO = _canvas.gameObject;
            _pauseCanvasGO.SetActive(false);
            _settingsPanel.SetActive(false);
            
        }

        public void TogglePause()
        {
            if (!_gameOver && !_levelCompleted)
            {
                _paused = !_paused;

                if (_paused)
                {
                    _pauseCanvasGO.SetActive(true);

                    Time.timeScale = 0;
                }
                else
                {
                    SoundManager.instance.PlaySound("button", _source, false);
                    _settingsPanel.SetActive(false);
                    _showSettings = false;
                    _pauseCanvasGO.SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }

        public void ToggleSettings()
        {
            SoundManager.instance.PlaySound("button", _source, false);

            _showSettings = !_showSettings;

            if (_showSettings)
            {
                _settingsPanel.SetActive(true);
            }else
            {
                _settingsPanel.SetActive(false);
            }
        }

    }
}