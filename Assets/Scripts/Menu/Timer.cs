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
        if (GameController.PointOfHealth == 0 && GameController.healthIsEmpty)
        {
            StartHealthRecoveryTimer();
        }


        // Update the timer display if the timer is active
        if (GameController.TimerIsActive)
        {
            UpdateTimerText();
            CheckTimer();
        }
    }

    private void StartHealthRecoveryTimer()
    {
        GameController.healthIsEmpty = false;
        DateTime timerStartTime = DateTime.Now;
        GameController.TimerIsActive = true;
        GameController.TimerEndTime = timerStartTime.AddHours(2);
        GameController.SaveGameData();
    }

    private void UpdateTimerText()
    { 
        TimeSpan remainingTime = GameController.TimerEndTime - DateTime.Now;

        if (remainingTime.TotalSeconds > 0)
        {
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}",
                remainingTime.Hours,
                remainingTime.Minutes,
                remainingTime.Seconds);
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
        }
    }


    // BUTTON ACTION
    public void WatchAd()
    {
        // TODO: Watch ad logic
    }
}
