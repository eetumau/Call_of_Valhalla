using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CallOfValhalla.States
{
    public class GameCompleteState : StateBase
    {

        public GameCompleteState() : base()
        {
            State = StateType.GameComplete;
            AddTransition(TransitionType.GameCompleteToMainMenu, StateType.MainMenu);
        }

        public override void StateActivated()
        {
            SceneManager.LoadScene(13);
        }
    }
}