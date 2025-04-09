using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatrol : State
{

    public StatePatrol(AIController ai) : base(ai) { }

    public override void Enter()
    {
        //Debug.Log("entering patrol state");
    }

    public override void Update()
    {
        if (ai.CanSeePlayer())
        {
            //Debug.Log("Can see player");
            ai.ChangeState(new StateChase(ai));
        }
        else if (ai.CanHearPlayer(ai.playerVolume) && !ai.CanSeePlayer())
        {
            ai.ChangeState(new StateSeatchForPlayer(ai));
        }
        else
        {
            //Debug.Log("Can't see player");
            ai.Patrol();
        }
    }

    public override void Exit()
    {
        //Debug.Log("exiting patrol state");
    }
}
