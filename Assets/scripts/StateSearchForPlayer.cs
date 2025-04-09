using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSeatchForPlayer : State
{
    private float searchTimer = 5f;
    private float currentSearchTime = 0f;
    private bool reachedSearchPosition = false;
    
    public StateSeatchForPlayer(AIController ai) : base(ai) { }

    public override void Enter()
    {
        Debug.Log("searching for player");
        ai.agent.SetDestination(ai.lastKnownPlayerPos);
        reachedSearchPosition = false;
        currentSearchTime = 0;
        
    }

    public override void Update()
    {
       if(ai.CanSeePlayer())
        {
            ai.ChangeState(new StateChase(ai));
            return;
        }

        if (!reachedSearchPosition) 
        {
            if(ai.agent.remainingDistance <= 1)
            {
                reachedSearchPosition = true;
            }
        }

        ai.transform.Rotate(Vector3.up * 60f * Time.deltaTime);
        Debug.Log("i should be spinnning");

        currentSearchTime += Time.deltaTime;
        if (currentSearchTime >= searchTimer) 
        { 
            ai.ChangeState(new StatePatrol(ai));
        }
    }

    public override void Exit()
    {
        Debug.Log("exiting idle state");
    }
}
