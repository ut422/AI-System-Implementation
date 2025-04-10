using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatrol : State
{

    public StatePatrol(AIController ai) : base(ai) { }

    // called when the state is entered
    public override void Enter()
    {
        //Debug.Log("entering patrol state");
    }

    // called every frame while the state is active
    public override void Update()
    {
        // if AI sees the player, switch to chase state
        if (ai.CanSeePlayer())
        {
            //Debug.Log("Can see player");
            ai.ChangeState(new StateChase(ai));
        }
        // if AI hears the player and doesn't see them, switch to search state
        else if (ai.CanHearPlayer(ai.playerVolume) && !ai.CanSeePlayer())
        {
            ai.ChangeState(new StateSeatchForPlayer(ai));
        }
        // if the player is neither seen nor heard, continue patrolling
        else
        {
            //Debug.Log("Can't see player");
            ai.Patrol();
        }
    }

    // called when exiting the state
    public override void Exit()
    {
        //Debug.Log("exiting patrol state");
    }
}
