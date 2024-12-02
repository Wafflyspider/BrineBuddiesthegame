using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class DayNightCycle : MonoBehaviour
{
    [Header("UI Elements")]
    public Text timeDisplay;

    [Header("Post-Processing Volume")]
    public Volume ppv; 

    [Header("Time Settings")]
    public float tick = 1f; 
    public float seconds = 0f; 
    public int mins = 0; 

    void Update()
    {
        CalcTime();
        DisplayTime();
        ControlPPV();
    }

 
    public void CalcTime()
    {
        seconds += Time.deltaTime * tick;

        if (seconds >= 60f) 
        {
            seconds = 0f;
            mins += 1;
        }

        if (mins >= 24) 
        {
            mins = 0;
        }
    }


    public void ControlPPV()
    {
        if (ppv == null)
        {
            Debug.LogWarning("ppv");
            return;
        }

        if (mins >= 21 && mins < 22)
        {
            ppv.weight = Mathf.Lerp(0f, 1f, (mins - 21) + seconds / 60f);
        }
        else if (mins >= 22 && mins < 6)
        {
            ppv.weight = 1f;
        }
        else if (mins >= 6 && mins < 7)
        {
            ppv.weight = Mathf.Lerp(1f, 0f, (mins - 6) + seconds / 60f);
        }
        else
        {
            ppv.weight = 0f;
        }
    }


    public void DisplayTime()
    {
        if (timeDisplay != null)
        {
            timeDisplay.text = string.Format("Time: {0:00}:{1:00}", mins, Mathf.FloorToInt(seconds));
        }
        else
        {
            Debug.LogWarning("Time Display Text is not assigned!");
        }
    }
}