using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBotMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject[] targets; // Array of targets to follow

    private Vector2 randomDirection;
    private float changeDirectionTimer = 2f;
    private float timer;

    // Cooldown variables
    private bool isCooldown;
    private float cooldownDuration;
    private float cooldownTimer;

    // Follow target variables
    private bool isFollowingTarget;
    private float followDuration;
    private float followTimer;
    private float followCooldownTimer;
    private GameObject currentTarget; // Currently selected target

    void Start()
    {
        // Initialize the timer
        timer = changeDirectionTimer;
        // Start moving in a random direction
        ChangeDirection();
    }

    void Update()
    {
        if (isFollowingTarget)
        {
            FollowTargetUpdate();
        }
        else
        {
            RandomMovementUpdate();
        }
    }

    void RandomMovementUpdate()
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

        // Randomly decide to follow the target
        if (Random.Range(0f, 100f) < 0.1f && followCooldownTimer <= 0) // 0.1% chance per frame
        {
            StartFollowingTarget();
        }
        else
        {
            followCooldownTimer -= Time.deltaTime;
        }
    }

    void FollowTargetUpdate()
    {
        if (currentTarget != null)
        {
            // Move towards the current target
            Vector2 direction = (currentTarget.transform.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }

        // Update the follow timer
        followTimer -= Time.deltaTime;
        if (followTimer <= 0)
        {
            StopFollowingTarget();
        }
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

    void StartFollowingTarget()
    {
        if (targets.Length > 0)
        {
            // Randomly select a target from the array
            currentTarget = targets[Random.Range(0, targets.Length)];
            isFollowingTarget = true;
            followDuration = Random.Range(.3f, 2.5f); // Follow for 1 to 3 seconds
            followTimer = followDuration;
            followCooldownTimer = Random.Range(3f, 5f); // Cooldown for 3 to 5 seconds after following
        }
    }

    void StopFollowingTarget()
    {
        isFollowingTarget = false;
        ChangeDirection();
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
