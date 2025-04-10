using UnityEngine;
using UnityEngine.SceneManagement; // scene management

public class ResetLevel : MonoBehaviour
{
    // this method is called when another collider enters the trigger area
    private void OnTriggerEnter(Collider other)
    {
        // check if the object that triggered the collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // call the RestartLevel method
            RestartLevel();
        }
    }

    // restarts the level
    private void RestartLevel()
    {
        // reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
