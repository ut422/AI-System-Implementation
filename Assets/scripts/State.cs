using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    // reference to the AIController
    protected AIController ai;

    // constructor, initializes the AI reference
    public State(AIController ai)
    {
        this.ai = ai;
    }

    // method called when entering the state
    public abstract void Enter();

    // method called every frame while in the state
    public abstract void Update();

    // method called when exiting the state
    public abstract void Exit();
}
