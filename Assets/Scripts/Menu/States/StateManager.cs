using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CallOfValhalla.States
{

    //Reminder to remove Testing states and transitions !

    public enum StateType
    {
        Error = -1,
        MainMenu,
        Game,
        GameOver

    }

    public enum TransitionType
    {
        Error = -1,
        MainMenuToGame,
        GameToGameOver,
        GameToMainMenu,
        GameOverToMainMenu
    }

    public class StateManager
    {

        private List<StateBase> _states = new List<StateBase>();

        public StateBase CurrentState { get; private set; }
        public StateType CurrentStateType { get { return CurrentState.State; } }

        public StateManager(StateBase initialState)
        {
            if (AddState(initialState))
            {
                CurrentState = initialState;
                initialState.StateActivated();
            }
        }

        public bool AddState(StateBase state)
        {
            bool exists = false;
            foreach (var stateBase in _states)
            {
                if (stateBase.State == state.State)
                {
                    exists = true;
                }
            }

            if (!exists)
            {
                _states.Add(state);
            }

            return !exists;
        }

        public bool RemoveState(StateType stateType)
        {
            StateBase state = null;
            foreach (var stateBase in _states)
            {
                if (stateBase.State == stateType)
                {
                    state = stateBase;
                }
            }

            return state != null && _states.Remove(state);
        }

        public void PerformTransition(TransitionType transition)
        {
            if (transition == TransitionType.Error)
            {
                return;
            }

            StateType targetStateType = CurrentState.GetTargetStateType(transition);
            if (targetStateType == StateType.Error || targetStateType == CurrentStateType)
            {
                return;
            }

            foreach (var state in _states)
            {
                if (state.State == targetStateType)
                {
                    CurrentState.StateDeactivating();
                    CurrentState = state;
                    CurrentState.StateActivated();
                }
            }
        }

    }
}