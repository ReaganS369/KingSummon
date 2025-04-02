using UnityEngine;

public class MissionDeath : MonoBehaviour
{
    [SerializeField] private GameObject[] targetObjects;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collided with any of the target objects
        foreach (GameObject targetObject in targetObjects)
        {
            if (collision.gameObject == targetObject)
            {
                // Destroy both the player (this game object) and the target object
                Destroy(gameObject);
                // Exit the loop after destroying objects to avoid further processing
                break;
            }
        }
    }
}
