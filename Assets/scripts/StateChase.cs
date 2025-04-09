using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class StateChase : State
    {

        public StateChase(AIController ai) : base(ai) { }

        public override void Enter()
        {
        Debug.Log("entering chase state");
        }

        public override void Update()
        {
            ai.ChasePlayer();
           if (!ai.CanSeePlayer())
           {
               ai.ChangeState(new StateSeatchForPlayer(ai));
           }
        }

        public override void Exit()
        {
            Debug.Log("exiting chase state");
        }
    }

