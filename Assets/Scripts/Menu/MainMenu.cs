using UnityEngine;
using System.Collections;
using CallOfValhalla.States;

namespace CallOfValhalla
{
    public class MainMenu : MonoBehaviour
    {

        public void OnNewGamePressed()
        {

            GameManager.Instance.Game();

        }

        public void OnQuitPressed()
        {
            Application.Quit();
        }
        
    }
}