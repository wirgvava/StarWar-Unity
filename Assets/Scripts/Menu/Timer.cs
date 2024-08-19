using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    void Update()
    {
        // Check if PointOfHealth is 0 and the timer is not already active
        if (GameController.PointOfHealth == 0 && GameController.TimerIsActive)
        {
            StartHealthRecoveryTimer();
            Debug.Log("HEALTH RECOVERY TIMER IS ACTIVE");
        }

        // Update the timer display if the timer is active
        if (GameController.TimerIsActive)
        {
            UpdateTimerText();
            CheckTimer();
            Debug.Log("TIMER UPDATE");
        }
    }

    private void StartHealthRecoveryTimer()
    {
        GameController.TimerEndTime = DateTime.Now.AddHours(2);  // Set the end time to 2 hours from now
        Debug.Log("TIMER DISABLE TIME: " + GameController.TimerEndTime);
    }

    private void UpdateTimerText()
    {         // დარჩენილი დრო =           19:30:00          -      17:30:00         =  2:00:00
        TimeSpan remainingTime = GameController.TimerEndTime - DateTime.Now;

        if (remainingTime.TotalSeconds > 0)
        {
            // Update the timer text in the format HH:MM:SS
            timerText.text = string.Format("{0:00}:{0:00}:{0:00}",
                remainingTime.Hours,
                remainingTime.Minutes,
                remainingTime.Seconds);

            Debug.Log("TIMER FORMAT: " + timerText.text);
        }
        else
        {
            timerText.text = "00:00:00";
        }
    }

    private void CheckTimer()
    {     
        if (DateTime.Now >= GameController.TimerEndTime)
        {
            GameController.PointOfHealth = 6;
            GameController.TimerIsActive = false;
            GameController.SaveGameData();
            Debug.Log("HEALTH IS RESTORED");
        }
    }


    // BUTTON ACTION
    public void WatchAd()
    {
        // TODO: Watch ad logic
    }
}
