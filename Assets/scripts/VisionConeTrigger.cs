using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionConeTrigger : MonoBehaviour
{
    // reference to the AIController
    private AIController ai;

    // Start is called before the first frame update
    private void Start()
    {
        // get the AIController component
        ai = GetComponent<AIController>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        // check if the player entered the trigger
        if (other.CompareTag("Player"))
        {
            // log and update AI about player in vision cone
            Debug.Log("Can see player");
            ai.SetPlayerInVisionCone(true);
        }
    }

    // called when another collider exits the trigger
    private void OnTriggerExit(Collider other)
    {
        // check if the player exited the trigger
        if (other.CompareTag("Player"))
        {
            // log and update AI about player leaving vision cone
            Debug.Log("Can't see player");
            ai.SetPlayerInVisionCone(false);
        }
    }
}
