using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CallOfValhalla.States
{
    public class GameOverState : StateBase
    {

        public GameOverState() : base()
        {
            State = StateType.GameOver;
            AddTransition(TransitionType.GameOverToMainMenu, StateType.MainMenu);
        }

        public override void StateActivated()
        {

            SceneManager.LoadScene(0);
        }


    }
}