using UnityEngine;
using System.Collections;

public class StartZoneWall : MonoBehaviour
{
    [SerializeField]
    private float delayInSeconds = 5.0f; // Serialized field for delay time

    private BoxCollider2D wallCollider; // Use BoxCollider2D directly
    public string runnerTag = "Runner";

    private bool canToggleTrigger = false; // Flag to control the trigger toggling

    // Reference to the runner collider
    private Collider2D runnerCollider;

    private void Awake()
    {
        // Initialize wallCollider as BoxCollider2D
        wallCollider = GetComponent<BoxCollider2D>();

        // Start the coroutine for the initial delay
        StartCoroutine(InitialDelay());
    }

    // Coroutine for initial delay
    private IEnumerator InitialDelay()
    {
        // Keep isTrigger off during the delay period
        wallCollider.isTrigger = false;
        Debug.Log("Initial delay started, isTrigger is set to false.");

        // Wait for the specified delay time
        yield return new WaitForSeconds(delayInSeconds);

        // Allow toggling of the trigger
        canToggleTrigger = true;
        Debug.Log("Initial delay ended, now allowing trigger toggling.");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Only allow trigger toggling after the delay period
        if (canToggleTrigger && collision.collider.CompareTag(runnerTag))
        {
            // Assign the collider to runnerCollider
            runnerCollider = collision.collider;

            // Enable trigger if runner touches from the left side
            if (collision.contacts[0].normal.x > 0)
            {
                wallCollider.isTrigger = true;
                runnerCollider.isTrigger = true;
                Debug.Log("Runner touched the wall from the left. isTrigger set to true.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only allow trigger toggling after the delay period
        if (canToggleTrigger && other.CompareTag(runnerTag))
        {
            // Assign the collider to runnerCollider
            runnerCollider = other;

            // Get Runner's Rigidbody2D component
            Rigidbody2D runnerRb = other.GetComponent<Rigidbody2D>();

            if (runnerRb != null && runnerRb.velocity.x < 0)
            {
                // Disable the trigger to block the Runner
                wallCollider.isTrigger = false;
                runnerCollider.isTrigger = false;
                Debug.Log("Runner is moving from right to left. isTrigger set to false.");
            }
        }
    }

    private void Update()
    {
        // Check if runnerCollider is not null before using it
        if (runnerCollider != null)
        {
            // Check if the runner is touching any objects tagged as "StartLine" or "playzone"
            GameObject[] startLines = GameObject.FindGameObjectsWithTag("StartLine");
            GameObject[] playZones = GameObject.FindGameObjectsWithTag("PlayzoneWall");

            bool runnerTouchingZone = false;

            foreach (GameObject zone in startLines)
            {
                if (runnerCollider.IsTouching(zone.GetComponent<Collider2D>()))
                {
                    runnerTouchingZone = true;
                    break;
                }
            }

            foreach (GameObject zone in playZones)
            {
                if (runnerCollider.IsTouching(zone.GetComponent<Collider2D>()))
                {
                    runnerTouchingZone = true;
                    break;
                }
            }

            // If the runner is not touching any zone, make it trigger
            if (!runnerTouchingZone)
            {
                runnerCollider.isTrigger = true;
            }
            else // If the runner touches a playzone, make it solid
            {
                runnerCollider.isTrigger = false;
            }
        }
    }

}
