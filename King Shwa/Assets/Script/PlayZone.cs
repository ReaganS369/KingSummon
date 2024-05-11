using UnityEngine;

public class PlayZone : MonoBehaviour
{
    private BoxCollider2D wallCollider; // Reference to BoxCollider2D
    public string runnerTag = "Runner"; // Tag to identify the runner

    private Collider2D runnerCollider; // Reference to the runner collider

    private void Awake()
    {
        // Initialize wallCollider as BoxCollider2D
        wallCollider = GetComponent<BoxCollider2D>();

        // Set the wall collider as a trigger by default
        wallCollider.isTrigger = false;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision object has the runner tag
        if (collision.collider.CompareTag(runnerTag))
        {
            // Assign the collider to runnerCollider
            runnerCollider = collision.collider;

            // Set the wall's trigger to false to block the Runner
            runnerCollider.isTrigger = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider has the runner tag
        if (other.CompareTag(runnerTag))
        {
            // Assign the collider to runnerCollider
            runnerCollider = other;

            // Set the wall's trigger to false to block the Runner
            runnerCollider.isTrigger = false;
            Debug.Log("Runner touched the wall. wallCollider.isTrigger set to false.");
        }
    }
    
}
