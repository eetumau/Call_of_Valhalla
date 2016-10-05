using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy
{
    public class BasicEnemy_AggressiveState : BasicEnemy_StateBase
    {

        public BasicEnemy_AggressiveState() : base()
        {
            State = StateType.Aggressive;

            AddTransition(TransitionType.AggressiveToPassive, StateType.Passive);
        }

        public override void StateActivated()
        {
        }

    }
}