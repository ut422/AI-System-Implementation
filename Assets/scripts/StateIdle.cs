using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : State
{

    public StateIdle(AIController ai) : base(ai) { }

    public override void Enter()
    {
        Debug.Log("entering idle state");
    }

    public override void Update()
    {
        if (ai.CanSeePlayer())
        {
            ai.ChangeState(new StateChase(ai));
        }
        else
        {
            ai.Patrol();
        }
    }

    public override void Exit()
    {
        Debug.Log("exiting idle state");
    }
}
