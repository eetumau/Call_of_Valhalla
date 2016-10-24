using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CallOfValhalla.States
{
    public class MainMenuState : StateBase
    {

        public MainMenuState() : base()
        {
            State = StateType.MainMenu;
            AddTransition(TransitionType.MainMenuToGame, StateType.Game);
            AddTransition(TransitionType.MainMenuToEetu, StateType.Eetu);
            AddTransition(TransitionType.MainMenuToTeemu, StateType.Teemu);
        }

        public override void StateActivated()
        {
            SceneManager.LoadScene(0);
        }


    }
}