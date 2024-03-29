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

        private void Update()
        {
            if (Input.GetButtonDown("Continue"))
            {
                OnNextLevelPressed();
            }
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

            if (GameManager.Instance.Level > 9)
            {
                SoundManager.instance.SetMusic("level_music_3");
            }else if(GameManager.Instance.Level > 3)
            {
                SoundManager.instance.SetMusic("level_music_5");
            }else
            {
                SoundManager.instance.SetMusic("level_music_4");
            }


            
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