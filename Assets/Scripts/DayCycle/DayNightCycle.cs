using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Import the UI namespace for Text

public class DayNightCycle : MonoBehaviour
{
    public Text timeDisplay;  // Reference to the UI Text component to display time

    public float tick = 1f;   // Rate at which time progresses (e.g., 1 for normal speed, greater for faster time)
    public float seconds = 0f;  // Tracks the seconds
    public int mins = 0;      // Tracks the minutes

    void Update()  // Use Update() instead of FixedUpdate() for time progression
    {
        CalcTime();
        DisplayTime();
    }

    // Method to calculate time
    public void CalcTime()
    {
        // Increment the seconds based on time elapsed since last frame, multiplied by the tick rate
        seconds += Time.deltaTime * tick;

        if (seconds >= 60)  // If seconds exceed 60, reset to 0 and increment minutes
        {
            seconds = 0;
            mins += 1;
        }

        if (mins >= 24)  // If minutes exceed 24, reset the cycle (to simulate day-night cycle)
        {
            mins = 0;
        }
    }

    // Method to display time on the UI
    public void DisplayTime()
    {
        // Format the time as minutes and seconds (mm:ss)
        timeDisplay.text = string.Format("Time:{0:00}:{1:00}", mins, Mathf.FloorToInt(seconds)); 
    }
}