using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour 
{
    private State currentState;

    public void ChangeState(State newState)

    {
        if (currentState != null)
            {
               currentState.Exit();
            }
        currentState = newState;
        currentState.Enter();
    }

    // Update is called once per frame
    public void Update()
    {
        if (currentState != null) 
        {
            currentState.Update();
        }
    }
}
