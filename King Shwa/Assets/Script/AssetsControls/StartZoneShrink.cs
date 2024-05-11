using UnityEngine;

public class StartZoneShrink : MonoBehaviour
{
    // Duration over which the shrink happens
    public float shrinkDuration = 10.0f;

    // Delay before the shrink starts
    public float shrinkStartDelay = 5.0f;

    // Reference to the original scale and position of the GameObject
    private Vector3 originalScale;
    private Vector3 originalPosition;

    // Timers to track the elapsed time
    private float delayTimer = 0f; // Timer for the delay before shrink starts
    private float shrinkTimer = 0f; // Timer for the shrink duration

    void Start()
    {
        // Store the original scale and position of the GameObject
        originalScale = transform.localScale;
        originalPosition = transform.position;
    }

    void Update()
    {
        // Update the delay timer
        delayTimer += Time.deltaTime;

        // Check if the delay has passed
        if (delayTimer >= shrinkStartDelay)
        {
            // Update the shrink timer
            shrinkTimer += Time.deltaTime;

            // Calculate the fraction of the shrink duration that has elapsed
            float fractionElapsed = shrinkTimer / shrinkDuration;

            // Calculate the new scale for the width (x-axis)
            float newWidth = Mathf.Lerp(originalScale.x, 0f, fractionElapsed);

            // Update the GameObject's scale only on the x-axis
            transform.localScale = new Vector3(newWidth, originalScale.y, originalScale.z);

            // Calculate how much to shift the position to keep the left side fixed
            float widthDifference = originalScale.x - newWidth;
            float shiftAmount = widthDifference / 2f; // Half of the width difference

            // Update the GameObject's position to keep the left side fixed
            transform.position = originalPosition - new Vector3(shiftAmount, 0f, 0f);

            // If the shrink duration has elapsed, deactivate the GameObject
            if (shrinkTimer >= shrinkDuration)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
