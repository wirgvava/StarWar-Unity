using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static int ChoosenShip;
    public static int PointOfHealth;
    public static bool TimerIsActive = false;
    public static DateTime TimerEndTime;
    public static int UserHighScore;
    public static int Money;
    public static bool IsSFXEnabled;
    public static bool IsMusicEnabled;
    public static List<int> UnlockedShips;

    public static bool healthIsEmpty;

    void Start()
    {
        GameData data = GameDataManager.LoadGame();

        if (data != null)
        {
            ChoosenShip = data.ChoosenShip;

            PointOfHealth = data.PointOfHealth;
            TimerIsActive = data.TimerIsActive;
            
            // Parse the stored string back to DateTime
            if (DateTime.TryParse(data.TimerEndTime, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime parsedDateTime))
            {
                TimerEndTime = parsedDateTime;
            }
            else
            {
                TimerEndTime = DateTime.MinValue;
                Debug.LogError("Failed to parse TimerEndTime, setting to DateTime.MinValue");
            }

            UserHighScore = data.UserHighScore;
            Money = data.Money;
            IsSFXEnabled = data.IsSFXEnabled;
            IsMusicEnabled = data.IsMusicEnabled;
            UnlockedShips = data.UnlockedShips;
        }
    }

    void Update()
    {
        if (PointOfHealth == 0 && !TimerIsActive)
        {
            healthIsEmpty = true;
        }
    }

    public static void SaveGameData()
    {
        GameData data = new GameData(ChoosenShip, PointOfHealth, UserHighScore, Money,  TimerIsActive, TimerEndTime, IsSFXEnabled, IsMusicEnabled, UnlockedShips);
        GameDataManager.SaveGame(data);
    }
}
