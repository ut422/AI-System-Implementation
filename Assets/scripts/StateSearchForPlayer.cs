using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSeatchForPlayer : State
{
    // time for how long to search
    private float searchTimer = 5f;
    // current search time
    private float currentSearchTime = 0f;
    // flag to check if search position is reached
    private bool reachedSearchPosition = false;

    // constructor
    public StateSeatchForPlayer(AIController ai) : base(ai) { }

    // called when state is entered
    public override void Enter()
    {
        // log search start
        Debug.Log("searching for player");
        // set destination to last known player position
        ai.agent.SetDestination(ai.lastKnownPlayerPos);
        // reset search state
        reachedSearchPosition = false;
        currentSearchTime = 0;
    }

    // called every frame in this state
    public override void Update()
    {
        // if player is seen, chase
        if (ai.CanSeePlayer())
        {
            ai.ChangeState(new StateChase(ai));
            return;
        }

        // check if search position is reached
        if (!reachedSearchPosition)
        {
            // if close enough, mark search position as reached
            if (ai.agent.remainingDistance <= 1)
            {
                reachedSearchPosition = true;
            }
        }

        // rotate to simulate searching
        ai.transform.Rotate(Vector3.up * 120f * Time.deltaTime);
        // log rotation
        Debug.Log("i should be spinnning");

        // track search time
        currentSearchTime += Time.deltaTime;
        // if time exceeds limit, patrol
        if (currentSearchTime >= searchTimer)
        {
            ai.ChangeState(new StatePatrol(ai));
        }
    }

    // called when state is exited
    public override void Exit()
    {
        // log exit
        Debug.Log("exiting idle state");
    }
}
