using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CallOfValhalla.Enemy
{

    public enum StateType
    {
        Error = -1,
        Passive,
        Aggressive
    }

    public enum TransitionType
    {
        Error = -1,
        PassiveToAggressive,
        AggressiveToPassive
    }

    public class BasicEnemy_StateManager
    {

        private List<BasicEnemy_StateBase> _states = new List<BasicEnemy_StateBase>();
  
        public BasicEnemy_StateBase CurrentState { get; private set; }
        public StateType CurrentStateType { get { return CurrentState.State; } }

        public BasicEnemy_StateManager(BasicEnemy_StateBase initialState)
        {
            if (AddState(initialState))
            {
                CurrentState = initialState;
            }
        }

        public bool AddState(BasicEnemy_StateBase state)
        {
            bool exists = false;
            foreach(var stateBase in _states)
            {
                if(stateBase.State == state.State)
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
            BasicEnemy_StateBase state = null;
            foreach(var stateBase in _states)
            {
                if(stateBase.State == stateType)
                {
                    state = stateBase;
                }
            }

            return state != null && _states.Remove(state);
        }

        public void PerformTransition(TransitionType transition)
        {
            if(transition == TransitionType.Error)
            {
                return;
            }

            StateType targetStateType = CurrentState.GetTargetStateType(transition);
            if(targetStateType == StateType.Error || targetStateType == CurrentStateType)
            {
                return;
            }

            foreach(var state in _states)
            {
                if(state.State == targetStateType)
                {
                    CurrentState.StateDeactivating();
                    CurrentState = state;
                    CurrentState.StateActivated();
                }
            }
        }

    }
}