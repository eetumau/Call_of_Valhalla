using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


namespace CallOfValhalla.States
{
    public class TeemuTestState : StateBase
    {

        public TeemuTestState() : base()
        {
            State = StateType.Teemu;
        }

        public override void StateActivated()
        {
            SceneManager.LoadScene(3);
        }
    }

}