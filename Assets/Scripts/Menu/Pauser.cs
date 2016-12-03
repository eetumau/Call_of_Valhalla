﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CallOfValhalla
{
    public class Pauser : MonoBehaviour
    {

        private GameObject _pauseCanvasGO;
        private Canvas _canvas;
        private AudioSource _source;


        private bool _paused = false;

        private void Start()
        {
            _source = GetComponent<AudioSource>();
            GameManager.Instance.Pauser = this;
            _canvas = GetComponentInChildren<Canvas>();
            _pauseCanvasGO = _canvas.gameObject;
            _pauseCanvasGO.SetActive(false);
        }

        public void TogglePause()
        {
           
            _paused = !_paused;

            if (_paused)
            {
                _pauseCanvasGO.SetActive(true);

                Time.timeScale = 0;
            }else
            {
                SoundManager.instance.PlaySound("button", _source);
                _pauseCanvasGO.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}