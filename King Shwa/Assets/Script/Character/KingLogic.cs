using UnityEngine;

public class KingLogic : MonoBehaviour
{
    // Serialized field to reference the game over UI
    [SerializeField]
    private GameObject gameoverUI;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the "Chaser" tag
        if (collision.gameObject.CompareTag("Chaser"))
        {
            // Log a message indicating the player has died
            Debug.Log("Player has died.");

            // Deactivate the player game object
            gameObject.SetActive(false);

            // Activate the game over UI
            gameoverUI.SetActive(true);
        }
    }
}
