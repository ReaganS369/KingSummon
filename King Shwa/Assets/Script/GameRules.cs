using UnityEngine;
using UnityEngine.Events;

public class GameRules : MonoBehaviour
{

    // This method is called when another collider enters the trigger collider attached to the game object
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider's game object has the "Chaser" tag
        if (other.CompareTag("Chaser"))
        {
            // Check if the current game object has the "Runner" tag
            if (gameObject.CompareTag("Runner"))
            {

                // Destroy the game object with the "Runner" tag
                Destroy(gameObject);
            }
        }
    }
}
