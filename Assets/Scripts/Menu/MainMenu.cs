using UnityEngine;
using System.Collections;
using CallOfValhalla.States;

namespace CallOfValhalla
{
    public class MainMenu : MonoBehaviour
    {
        private int _level;
        private Animator _animator;

        private AudioSource _source;


        void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _source = GetComponent<AudioSource>();
            CheckPanel();
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

        public void OnNewGamePressed()
        {
            SoundManager.instance.PlaySound("button", _source);
            _animator.SetTrigger("Hide");
        }

        public void OnSettingsPressed()
        {
            SoundManager.instance.PlaySound("button", _source);
            _animator.SetTrigger("Show2");
        }

        public void OnQuitPressed()
        {
            SoundManager.instance.PlaySound("button", _source);
            Application.Quit();
        }

        public void OnBackPressed()
        {
            SoundManager.instance.PlaySound("button", _source);
            _animator.SetTrigger("Show");
        }

        public void OnBack2Pressed()
        {
            SoundManager.instance.PlaySound("button", _source);
            _animator.SetTrigger("Show");
        }

        public void Level1()
        {
            SoundManager.instance.PlaySound("button", _source);
            GameManager.Instance.Level = 1;
            GameManager.Instance.Game();
        }

        public void Level2()
        {
            SoundManager.instance.PlaySound("button", _source);
            GameManager.Instance.Level = 2;
            GameManager.Instance.Game();
        }
        
        public void Level3()
        {
            SoundManager.instance.PlaySound("button", _source);
            GameManager.Instance.Level = 3;
            GameManager.Instance.Game();
        }

        public void Level4()
        {
            SoundManager.instance.PlaySound("button", _source);
            GameManager.Instance.Level = 4;
            GameManager.Instance.Game();
        }

        public void Level5()
        {
            SoundManager.instance.PlaySound("button", _source);
            GameManager.Instance.Level = 5;
            GameManager.Instance.Game();
        }

		public void Level6()
		{
            SoundManager.instance.PlaySound("button", _source);
            GameManager.Instance.Level = 6;
			GameManager.Instance.Game();
		}

		public void Level7()
		{
            SoundManager.instance.PlaySound("button", _source);
            GameManager.Instance.Level = 7;
			GameManager.Instance.Game();
		}

		public void Level8()
		{
            SoundManager.instance.PlaySound("button", _source);
            GameManager.Instance.Level = 8;
			GameManager.Instance.Game();
		}

		public void Level9()
		{
            SoundManager.instance.PlaySound("button", _source);
            GameManager.Instance.Level = 9;
			GameManager.Instance.Game();
		}

        //For testing purposes
        public void EetunSceneen()
        {
            SoundManager.instance.PlaySound("button", _source);
            GameManager.Instance.Level = 9;
            GameManager.Instance.Game();
        }

        //For testing purposes
        public void TeemunSceneen()
        {
            SoundManager.instance.PlaySound("button", _source);
            GameManager.Instance.Level = 10;
            GameManager.Instance.Game();
        }

    }
}