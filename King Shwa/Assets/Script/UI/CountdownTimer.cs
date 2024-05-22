using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField]
    private float timerDuration = 60.0f; // Duration of the timer in seconds

    [SerializeField]
    private Text timerText; // Reference to the UI Text component that displays the timer

    [SerializeField]
    private GameObject gameOverUI; // Reference to the Game Over UI GameObject

    private float currentTime;

    void Start()
    {
        currentTime = timerDuration;
        gameOverUI.SetActive(false); // Ensure Game Over UI is hidden at start
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay(currentTime);
        }
        else
        {
            TimerEnded();
        }
    }

    void UpdateTimerDisplay(float time)
    {
        time = Mathf.Max(time, 0); // Ensure the time doesn't go below 0
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerEnded()
    {
        gameOverUI.SetActive(true); // Show Game Over UI
        // Additional game over logic can be added here if needed
    }
}
