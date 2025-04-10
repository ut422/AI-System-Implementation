using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // stores the current active state
    private State currentState;

    // changes the current state
    public void ChangeState(State newState)
    {
        // if there's a current state, exit it
        if (currentState != null)
        {
            currentState.Exit();
        }
        // set the new state and enter it
        currentState = newState;
        currentState.Enter();
    }

    // Update is called once per frame
    public void Update()
    {
        // if there's a current state, update it
        if (currentState != null)
        {
            currentState.Update();
        }
    }
}
