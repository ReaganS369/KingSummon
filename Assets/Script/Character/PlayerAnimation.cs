using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator animator;

    private void Update()
    {
        // Check if the player is moving
        bool isMoving = rb2d.velocity != Vector2.zero;

        // Update the IsRunning parameter in the Animator
        animator.SetBool("IsRunning", isMoving);

        // Print the player's velocity to the console
        Debug.Log($"Player Velocity: {rb2d.velocity}");

        // Print whether the player is running or not to the console
        Debug.Log($"IsRunning: {isMoving}");
    }
}
