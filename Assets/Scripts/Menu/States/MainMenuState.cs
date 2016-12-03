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

        }

        public override void StateActivated()
        {
            SceneManager.LoadScene(0);
            SoundManager.instance.SetMusic("menu_music_1");
        }


    }
}