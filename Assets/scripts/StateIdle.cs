using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : State
{

    public StateIdle(AIController ai) : base(ai) { }

    // called when the state is entered
    public override void Enter()
    {
        Debug.Log("entering idle state");
    }

    // called every frame while the state is active
    public override void Update()
    {
        // if the AI sees the player, switch to chase state
        if (ai.CanSeePlayer())
        {
            ai.ChangeState(new StateChase(ai));
        }
        // if the player is not seen, continue patrolling
        else
        {
            ai.Patrol();
        }
    }

    // called when exiting the state
    public override void Exit()
    {
        Debug.Log("exiting idle state");
    }
}
