using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy
{
    public class BasicEnemy_PassiveState : BasicEnemy_StateBase
    {

        public BasicEnemy_PassiveState() : base()
        {
            State = StateType.Passive;
            AddTransition(TransitionType.PassiveToAggressive, StateType.Aggressive);
        }

        public override void StateActivated()
        {
            BasicEnemy_Controller.Instance.SetPassive();
        }

    }
}