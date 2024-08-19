using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int ChoosenShip;
    public int PointOfHealth;
    public bool TimerIsActive;
    public string TimerEndTime;
    public int UserHighScore;
    public int Money;
    public bool IsSFXEnabled;
    public bool IsMusicEnabled;
    public List<int> UnlockedShips;

    public GameData(int choosenShip, int pointOfHealth, int userHighScore, int money,  bool timerIsActive, DateTime timerEndTime, bool isSFXEnabled, bool isMusicEnabled, List<int> unlockedShips)
    {
        ChoosenShip = choosenShip;
        PointOfHealth = pointOfHealth;
        UserHighScore = userHighScore;
        Money = money;
        TimerIsActive = timerIsActive;
        TimerEndTime = timerEndTime.ToString("o");
        IsSFXEnabled = isSFXEnabled;
        IsMusicEnabled = isMusicEnabled;
        UnlockedShips = unlockedShips ?? new List<int>();
    }
}
