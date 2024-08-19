using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static int ChoosenShip;
    public static int PointOfHealth;
    public static bool TimerIsActive;
    public static DateTime TimerEndTime;
    public static int UserHighScore;
    public static int Money;
    public static bool IsSFXEnabled;
    public static bool IsMusicEnabled;
    public static List<int> UnlockedShips;

    void Start()
    {
        GameData data = GameDataManager.LoadGame();

        if (data != null)
        {
            ChoosenShip = data.ChoosenShip;
            PointOfHealth = data.PointOfHealth;
            TimerIsActive = data.TimerIsActive;
            TimerEndTime = data.TimerEndTime;
            UserHighScore = data.UserHighScore;
            Money = data.Money;
            IsSFXEnabled = data.IsSFXEnabled;
            IsMusicEnabled = data.IsMusicEnabled;
            UnlockedShips = data.UnlockedShips;

            Debug.Log("GameData Loaded succesfully");
            // Debug.Log("Timer end time: " + TimerEndTime);

            // Debug.Log("DateTime.Now: " + DateTime.Now);
            // Debug.Log("Date from now + 2H: " + DateTime.Now.AddHours(2));
        }
        // SaveGameData();
    }

    void Update()
    {
        if (PointOfHealth == 0)
        {
            TimerIsActive = true;
        }
        else 
        {
            TimerIsActive = false;
        }
    }

    public static void SaveGameData()
    {
        GameData data = new GameData(ChoosenShip, PointOfHealth, UserHighScore, Money,  TimerIsActive, TimerEndTime, IsSFXEnabled, IsMusicEnabled, UnlockedShips);
        GameDataManager.SaveGame(data);
    }
}
