﻿using UnityEngine;
using System.Collections;
using CallOfValhalla.States;
using UnityEngine.UI;

namespace CallOfValhalla
{
    public class MainMenu : MonoBehaviour
    {
        private int _level;
        private Animator _animator;
        private GameObject _levelPanel;
        private GameObject _settingsPanel;
        private AudioSource _source;

        [SerializeField]
        private Toggle _music;
        [SerializeField]
        private Toggle _sound;
        [SerializeField]
        private Slider _mVolume;
        [SerializeField]
        private Slider _sVolume;
        [SerializeField]
        private Button[] _levelButtons;

        void Start()
        {
            _levelPanel = GameObject.Find("LevelSelectPanel");
            _settingsPanel = GameObject.Find("SettingsPanel");

            _animator = GetComponentInChildren<Animator>();
            _source = GetComponent<AudioSource>();

            CheckLevelCompleted();
            //_levelPanel.SetActive(false);
            //_settingsPanel.SetActive(false);

            CheckPanel();

            _music.isOn = !SoundManager.instance.MusicMuted;
            _sound.isOn = !SoundManager.instance.SoundMuted;
            _mVolume.value = SoundManager.instance.MusicVolume;
            _sVolume.value = SoundManager.instance.SoundVolume;
            
        }

        //If returned to main menu after completing a level, move to level selection instantly.
        private void CheckPanel()
        {
            if (GameManager.Instance.ToSelectLevel)
            {
                OnNewGamePressed();
                GameManager.Instance.ToSelectLevel = false;
            }
        }



        private void CheckLevelCompleted()
        {
            int levelCompleted = GameManager.Instance.LevelCompleted;

            for(int i = 0; i < _levelButtons.Length; i++)
            {
                if(levelCompleted >= i)
                {
                    _levelButtons[i].interactable = true;
                    _levelButtons[i].GetComponentInChildren<Text>().enabled = true;
                }else if(levelCompleted < i)
                {
                    _levelButtons[i].interactable = false;
                    _levelButtons[i].GetComponentInChildren<Text>().enabled = false;

                }
            }
        }

        public void OnNewGamePressed()
        {
            SoundManager.instance.PlaySound("button", _source, false);
            //_levelPanel.SetActive(true);
            _animator.SetTrigger("Hide");
        }

        public void OnSettingsPressed()
        {
            //_settingsPanel.SetActive(true);
            SoundManager.instance.PlaySound("button", _source, false);
            _animator.SetTrigger("Show2");
        }

        public void OnQuitPressed()
        {
            SoundManager.instance.PlaySound("button", _source, false);
            Application.Quit();
        }

        public void OnBackPressed()
        {
            //_levelPanel.SetActive(false);
            SoundManager.instance.PlaySound("button", _source, false);
            _animator.SetTrigger("Show");
        }

        public void OnBack2Pressed()
        {
            //_settingsPanel.SetActive(false);
            SoundManager.instance.PlaySound("button", _source, false);
            _animator.SetTrigger("Show");
        }

        public void Level1()
        {
            SoundManager.instance.PlaySound("button", _source, false);
            GameManager.Instance.Level = 1;
            GameManager.Instance.Game();
        }

        public void Level2()
        {
            SoundManager.instance.PlaySound("button", _source, false);
            GameManager.Instance.Level = 2;
            GameManager.Instance.Game();
        }
        
        public void Level3()
        {
            SoundManager.instance.PlaySound("button", _source, false);
            GameManager.Instance.Level = 3;
            GameManager.Instance.Game();
        }

        public void Level4()
        {
            SoundManager.instance.PlaySound("button", _source, false);
            GameManager.Instance.Level = 4;
            GameManager.Instance.Game();
        }

        public void Level5()
        {
            SoundManager.instance.PlaySound("button", _source, false);
            GameManager.Instance.Level = 5;
            GameManager.Instance.Game();
        }

		public void Level6()
		{
            SoundManager.instance.PlaySound("button", _source, false);
            GameManager.Instance.Level = 6;
			GameManager.Instance.Game();
		}

		public void Level7()
		{
            SoundManager.instance.PlaySound("button", _source, false);
            GameManager.Instance.Level = 7;
			GameManager.Instance.Game();
		}

		public void Level8()
		{
            SoundManager.instance.PlaySound("button", _source, false);
            GameManager.Instance.Level = 8;
			GameManager.Instance.Game();
		}

		public void Level9()
		{
            SoundManager.instance.PlaySound("button", _source, false);
            GameManager.Instance.Level = 9;
			GameManager.Instance.Game();
		}

		public void Level10()
		{
			SoundManager.instance.PlaySound("button", _source, false);
			GameManager.Instance.Level = 10;
			GameManager.Instance.Game();
		}

		public void Level11()
		{
			SoundManager.instance.PlaySound("button", _source, false);
			GameManager.Instance.Level = 11;
			GameManager.Instance.Game();
		}

		public void Level12()
		{
			SoundManager.instance.PlaySound("button", _source, false);
			GameManager.Instance.Level = 12;
			GameManager.Instance.Game();
		}

        //For testing purposes
        public void EetunSceneen()
        {
            SoundManager.instance.PlaySound("button", _source, false);
            GameManager.Instance.Level = 12;
            GameManager.Instance.Game();
        }

        //For testing purposes
        public void TeemunSceneen()
        {
            SoundManager.instance.PlaySound("button", _source, false);
            GameManager.Instance.Level = 13;
            GameManager.Instance.Game();
        }

        public void MuteMusic()
        {
            SoundManager.instance.ToggleMusic(_music.isOn);
        }

    

        public void MuteSound()
        {
            SoundManager.instance.ToggleSound(_sound.isOn);
        }

        public void AdjustMusic()
        {
            SoundManager.instance.MusicVolume = _mVolume.value;
            GameManager.Instance.Save();
        }

        public void AdjustSound()
        {
            SoundManager.instance.SoundVolume = _sVolume.value;
            GameManager.Instance.Save();
        }

        public void DeleteSaveData()
        {


            _sound.isOn = true;
            _music.isOn = true;
            SoundManager.instance.MusicVolume = 1;
            SoundManager.instance.SoundVolume = 1;
            _mVolume.value = SoundManager.instance.MusicVolume;
            _sVolume.value = SoundManager.instance.SoundVolume;
            GameManager.Instance.LevelCompleted = 0;
            CheckLevelCompleted();
            GameManager.Instance.DeleteSaveData();
        }
    }
}