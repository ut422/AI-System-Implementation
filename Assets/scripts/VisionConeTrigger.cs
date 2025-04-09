using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionConeTrigger : MonoBehaviour
{
    private AIController ai;

    // Start is called before the first frame update
    private void Start()
    {
        ai = GetComponent<AIController>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player)"))
        {
            Debug.Log("Can see player");
            ai.SetPlayerInVisionCone(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player)"))
        {
            Debug.Log("Can't see player");
            ai.SetPlayerInVisionCone(false);
        }

    }
}

