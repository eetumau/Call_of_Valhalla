using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CallOfValhalla.States
{
    public class GameState : StateBase
    {

        public GameState() : base()
        {
            State = StateType.Game;
            AddTransition(TransitionType.GameToMainMenu, StateType.MainMenu);
            AddTransition(TransitionType.GameToGameOver, StateType.GameOver);
            AddTransition(TransitionType.GameToGameComplete, StateType.GameComplete);
        }

        public override void StateActivated()
        {
            SceneManager.LoadScene(GameManager.Instance.Level);
        }

    }
}