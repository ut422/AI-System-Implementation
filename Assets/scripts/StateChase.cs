using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChase : State
{

    public StateChase(AIController ai) : base(ai) { }

    // called when the state is entered
    public override void Enter()
    {
        Debug.Log("entering chase state");
    }

    // called every frame while the state is active
    public override void Update()
    {
        // AI chases the player
        ai.ChasePlayer();

        // if the AI can no longer see the player, switch to search state
        if (!ai.CanSeePlayer())
        {
            ai.ChangeState(new StateSeatchForPlayer(ai));
        }
    }

    // called when exiting the state
    public override void Exit()
    {
        Debug.Log("exiting chase state");
    }
}
