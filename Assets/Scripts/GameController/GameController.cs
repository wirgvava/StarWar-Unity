using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Stored Data
    public static int ChoosenShip;
    public static int PointOfHealth;
    public static bool TimerIsActive = false;
    public static DateTime TimerEndTime;
    public static int UserHighScore;
    public static int Money;
    public static bool IsSFXEnabled;
    public static bool IsMusicEnabled;
    public static List<int> UnlockedShips;
    // ---------

    public static bool healthIsEmpty;
    // Audio
    public AudioSource backgroundMusic;
    public AudioClip soundtrack;
    public AudioClip inGameSound;

    async void Start()
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

            Firestore firestore = new Firestore();
            await firestore.GetTopScores();
        }
    }

    void Update()
    {
        CheckForTimer();
        SwitchClip();
        CheckMusicAvailablity();
    }

    public static void SaveGameData()
    {
        GameData data = new GameData(ChoosenShip, PointOfHealth, UserHighScore, Money,  TimerIsActive, TimerEndTime, IsSFXEnabled, IsMusicEnabled, UnlockedShips);
        GameDataManager.SaveGame(data);
    }

    // AUDIO
    public void PlayMusic()
    {
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }

    public void StopMusic()
    {
        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Pause();
        }
    }

    private void SwitchClip()
    {
        if (Player.isPlaying)
        {
            backgroundMusic.clip = inGameSound;
        }
        else if (Player.isGameOver)
        {
            backgroundMusic.clip = null;
        }
        else 
        {
            backgroundMusic.clip = soundtrack;
        }
    }

    private void CheckMusicAvailablity()
    {
        if (IsMusicEnabled)
        {
            PlayMusic();
        }
        else
        {
            StopMusic();
        }
    }


    // Timer
    private void CheckForTimer()
    {
        if (PointOfHealth == 0 && !TimerIsActive)
        {
            healthIsEmpty = true;
        }
    }
}
