using UnityEngine;
using System.Collections;
using CallOfValhalla.States;

namespace CallOfValhalla
{
    public class MainMenu : MonoBehaviour
    {
        private int _level;
        private Animator _animator;

        void Start()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void OnNewGamePressed()
        {
            _animator.SetTrigger("Hide");

        }

        public void OnQuitPressed()
        {
            Application.Quit();
        }

        public void OnBackPressed()
        {
            _animator.SetTrigger("Show");
        }

        public void Level1()
        {
            GameManager.Instance.Level = 1;
            GameManager.Instance.Game();
        }

        public void Level2()
        {
            GameManager.Instance.Level = 2;
            GameManager.Instance.Game();
        }
        
        public void Level3()
        {
            GameManager.Instance.Level = 3;
            GameManager.Instance.Game();
        }

        //For testing purposes
        public void EetunSceneen()
        {
            GameManager.Instance.Level = 4;
            GameManager.Instance.Game();
        }

        //For testing purposes
        public void TeemunSceneen()
        {
            GameManager.Instance.Level = 5;
            GameManager.Instance.Game();
        }

    }
}