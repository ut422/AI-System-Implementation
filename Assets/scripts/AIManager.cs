using UnityEngine;
using System.Collections.Generic;


public class AIManager : MonoBehaviour
{
    // single instance
    public static AIManager Instance { get; private set; }

    // last known player position
    public Vector3 lastKnownPlayerPos;

    // list of registered AI agents
    public List<AIController> registeredAgents = new List<AIController>();

    // Start is called before the first frame update
    private void Awake()
    {
        // ensure only one instance of AIManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // adds an AI agent to the list
    public void RegisterAgent(AIController ai)
    {
        if (ai.isManaged)
        {
            registeredAgents.Add(ai);
        }
    }

    // removes an AI agent from the list
    public void UnrgisterAgent(AIController ai)
    {
        if (ai.isManaged)
        {
            registeredAgents.Remove(ai);
        }
    }

    // alerts agents that the player was spotted
    public void AlertPlayerSpotted()
    {
        // change all agents' state to search for the player
        foreach (var ai in registeredAgents)
        {
            ai.ChangeState(new StateSeatchForPlayer(ai));
        }
    }
}
