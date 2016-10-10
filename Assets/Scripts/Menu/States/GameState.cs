﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CallOfValhalla.States
{
    public class GameState : StateBase
    {

        public GameState() : base()
        {
            State = StateType.Game;
            AddTransition(TransitionType.GameToGameOver, StateType.GameOver);
        }

        public override void StateActivated()
        {
            SceneManager.LoadScene(1);
        }

    }
}