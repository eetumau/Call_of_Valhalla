using UnityEngine;
using System.Collections;
using CallOfValhalla.States;

namespace CallOfValhalla
{
    public class MainMenu : MonoBehaviour
    {
        private int _level;

        public void OnNewGamePressed()
        {
            GameManager.Instance.Level = 1;
            GameManager.Instance.Game();

        }

        public void OnQuitPressed()
        {
            Application.Quit();
        }
        
        //For testing purposes
        public void EetunSceneen()
        {
            GameManager.Instance.Level = 2;
            GameManager.Instance.Game();
        }

        //For testing purposes
        public void TeemunSceneen()
        {
            GameManager.Instance.Level = 3;
            GameManager.Instance.Game();
        }

    }
}