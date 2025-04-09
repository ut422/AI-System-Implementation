using UnityEngine;
using System.Collections.Generic;


public class AIManager : MonoBehaviour
{
    public static AIManager Instance { get; private set; }

    public Vector3 lastKnownPlayerPos;

    public List<AIController> registeredAgents = new List<AIController>();

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else

        {
            Instance = this;
        }
    }
    public void RegisterAgent(AIController ai) {
        if (ai.isManaged)
            {
                registeredAgents.Add(ai);
            }
    }

    public void UnrgisterAgent(AIController ai)
    {
        if (ai.isManaged)
        {
            registeredAgents.Remove(ai);
        }
    }

    public void AlertPlayerSpotted()
    {
        foreach (var ai in registeredAgents)
        {
            ai.ChangeState(new StateSeatchForPlayer(ai));
        }
    }
}

   

