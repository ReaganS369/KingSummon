using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 randomDirection;
    private float changeDirectionTimer = 2f;
    private float timer;

    // Cooldown variables
    private bool isCooldown;
    private float cooldownDuration;
    private float cooldownTimer;

    void Start()
    {
        // Initialize the timer
        timer = changeDirectionTimer;
        // Start moving in a random direction
        ChangeDirection();
    }

    void Update()
    {
        // Update the timer
        timer -= Time.deltaTime;
        // If it's time to change direction, do so
        if (timer <= 0 && !isCooldown)
        {
            ChangeDirection();
            // Reset the timer
            timer = changeDirectionTimer;
        }

        // If in cooldown, decrement the cooldown timer
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                // End cooldown
                isCooldown = false;
            }
        }

        // Move the bot in the current direction
        rb.velocity = randomDirection * moveSpeed;
    }

    void ChangeDirection()
    {
        // Generate a random direction
        float randomAngle = Random.Range(0f, 360f);
        randomDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));

        // Start cooldown
        isCooldown = true;
        // Set a random cooldown duration between 1 to 3 seconds
        cooldownDuration = Random.Range(1f, 3f);
        cooldownTimer = cooldownDuration;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayzoneWall"))
        {
            // If colliding with a wall, change direction
            ChangeDirection();
        }
    }
}
