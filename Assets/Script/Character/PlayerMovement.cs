using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Reference to the floating joystick
    public FloatingJoystick joystick;

    // Speed at which the player will move
    public float speed = 5f;

    // Reference to the Rigidbody2D component
    private Rigidbody2D rb;

    private void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get the input from the joystick
        float moveHorizontal = joystick.Horizontal;
        float moveVertical = joystick.Vertical;

        // Create a new Vector2 based on the joystick input
        Vector2 movementDirection = new Vector2(moveHorizontal, moveVertical);

        // Normalize the movement direction
        if (movementDirection.sqrMagnitude > 0)
        {
            movementDirection.Normalize();
        }

        // Apply the speed to the movement direction and set the player's velocity
        rb.velocity = movementDirection * speed;
    }
}
