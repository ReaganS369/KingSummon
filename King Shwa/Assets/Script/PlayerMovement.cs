using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private MovementJoystick movementJoystick;
    private float playerSpeed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        movementJoystick = GameObject.Find("Movement Joystick").GetComponent<MovementJoystick>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 movement = movementJoystick.joystickVec.normalized;
        rb.velocity = movement * playerSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayzoneWall"))
        {
            rb.velocity = Vector2.zero;
        }
    }
}
