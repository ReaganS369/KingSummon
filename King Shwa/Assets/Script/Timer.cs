using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    // SerializeField allows you to set this in the Unity editor
    [SerializeField] private Text timerText;

    // The duration of the first timer phase in seconds (60 seconds)
    [SerializeField] private float countUpDuration = 60f;

    // The duration of the second timer phase in seconds (3 minutes and 60 seconds = 240 seconds)
    [SerializeField] private float countDownDuration = 240f;

    // The remaining time in seconds
    private float remainingTime;

    // Enum to track the current phase of the timer
    private enum TimerPhase
    {
        CountingUp,
        CountingDown
    }
    private TimerPhase timerPhase;

    private void Start()
    {
        // Initialize the remaining time to the count-up duration
        remainingTime = 0f;
        timerPhase = TimerPhase.CountingUp;

        // Update the timer text
        UpdateTimerText();
    }

    private void Update()
    {
        // Update the remaining time based on the current phase
        if (timerPhase == TimerPhase.CountingUp)
        {
            remainingTime += Time.deltaTime;

            // Check if the count-up phase is complete
            if (remainingTime >= countUpDuration)
            {
                remainingTime = countDownDuration;
                timerPhase = TimerPhase.CountingDown;
            }
        }
        else if (timerPhase == TimerPhase.CountingDown)
        {
            remainingTime -= Time.deltaTime;

            // Make sure the remaining time doesn't go below 0
            if (remainingTime < 0f)
            {
                remainingTime = 0f;
                // Add any additional logic here (e.g., game over when time runs out)
            }
        }

        // Update the timer text
        UpdateTimerText();
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
