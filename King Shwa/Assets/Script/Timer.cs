using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    // SerializeField allows you to set this in the Unity editor
    [SerializeField] private Text timerText;

    // The duration of the timer in seconds (60 seconds)
    private float timerDuration = 60f;

    // The remaining time in seconds
    private float remainingTime;

    private void Start()
    {
        // Initialize the remaining time to the timer duration
        remainingTime = timerDuration;

        // Update the timer text
        UpdateTimerText();
    }

    private void Update()
    {
        // Decrease the remaining time
        remainingTime -= Time.deltaTime;

        // Make sure the remaining time doesn't go below 0
        if (remainingTime < 0f)
        {
            remainingTime = 0f;
        }

        // Update the timer text
        UpdateTimerText();

        // Add any additional logic here (e.g., game over when time runs out)
    }

    // Method to update the timer text
    private void UpdateTimerText()
    {
        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        // Format the time as 00:00
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
