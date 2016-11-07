using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


namespace CallOfValhalla.States
{
    public class EetuTestState : StateBase
    {

        public EetuTestState() : base()
        {
            State = StateType.Eetu;
            AddTransition(TransitionType.EetuToGameOver, StateType.MainMenu);
        }

        public override void StateActivated()
        {
            SceneManager.LoadScene(1);
        }
    }
}