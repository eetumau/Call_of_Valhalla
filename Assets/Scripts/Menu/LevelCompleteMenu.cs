﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CallOfValhalla
{
    public class LevelCompleteMenu : MonoBehaviour
    {
        private Text _text;

        // Use this for initialization
        void Start()
        {
            _text = GetComponentInChildren<Text>();
            _text.text = "Level " + GameManager.Instance.Level + " Completed!";
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnRestartPressed()
        {
            GameManager.Instance.LevelCompleteUI.ToggleLevelCompleteUI();
            SceneManager.LoadScene(GameManager.Instance.Level);
        }

        public void OnNextLevelPressed()
        {
            GameManager.Instance.LevelCompleteUI.ToggleLevelCompleteUI();
            GameManager.Instance.Level += 1;
            SceneManager.LoadScene(GameManager.Instance.Level);
        }

        public void OnExitPressed()
        {
            GameManager.Instance.ToSelectLevel = true;
            GameManager.Instance.LevelCompleteUI.ToggleLevelCompleteUI();
            GameManager.Instance.MainMenu();
        }
    }
}