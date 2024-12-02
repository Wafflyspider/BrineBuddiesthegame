using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI Text

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 600f; // Total countdown time in seconds (default is 10 minutes)
    public Text countdownText; // Reference to the Text UI element
    
    private float timeLeft; // Remaining time in seconds

    void Start()
    {
        // Initialize the remaining time
        timeLeft = countdownTime;
        UpdateCountdownDisplay(); // Update the UI at the start
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            // Reduce time left by the time elapsed since the last frame
            timeLeft -= Time.deltaTime;

            // Clamp the value to ensure it doesn't go below 0
            timeLeft = Mathf.Max(timeLeft, 0);

            // Update the UI
            UpdateCountdownDisplay();
        }
    }

    void UpdateCountdownDisplay()
    {
        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);

        // Format and display the countdown as MM:SS
        countdownText.text = string.Format("Countdown: {0:00}:{1:00}", minutes, seconds);
    }
}
