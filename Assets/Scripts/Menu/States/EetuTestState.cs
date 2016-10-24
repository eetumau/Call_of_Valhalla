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
        }

        public override void StateActivated()
        {
            SceneManager.LoadScene(1);
        }
    }
}