using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using GoogleMobileAds.Api;

#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

#if UNITY_IOS
using Unity.Notifications.iOS;
#endif

public class Timer : MonoBehaviour {

    public TextMeshProUGUI timerText;
    public GameObject errorMessage;

    // AD
    private RewardedAd rewardedAd;

    void Start() {
        errorMessage.SetActive(false);
    }

    void Update() {
        if (GameController.PointOfHealth == 0 && GameController.healthIsEmpty) {
            LoadRewardedAd();
            StartHealthRecoveryTimer();
        }

        // Update the timer display if the timer is active
        if (GameController.TimerIsActive) {
            UpdateTimerText();
            CheckTimer();
        }
    }

    private void StartHealthRecoveryTimer() {
        GameController.healthIsEmpty = false;
        DateTime timerStartTime = DateTime.Now;
        GameController.TimerIsActive = true;
        GameController.TimerEndTime = timerStartTime.AddHours(2);
        GameController.SaveGameData();
        ScheduleHealthRestoredNotification();
    }

    private void UpdateTimerText() { 
        TimeSpan remainingTime = GameController.TimerEndTime - DateTime.Now;

        if (remainingTime.TotalSeconds > 0) {
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}",
                remainingTime.Hours,
                remainingTime.Minutes,
                remainingTime.Seconds);
        } else {
            timerText.text = "00:00:00";
        }
    }

    private void CheckTimer() {    
        if (DateTime.Now >= GameController.TimerEndTime) {
            SFXSoundController.healthIsRestored = true;
            GameController.PointOfHealth = 6;
            GameController.TimerIsActive = false;
            GameController.SaveGameData();
        }
    }

    private void ScheduleHealthRestoredNotification() {
        // Android notification
        #if UNITY_ANDROID
        if (Application.platform == RuntimePlatform.Android) {
            var notification = new AndroidNotification {
                Title = Constants.notyTitle,
                Text = Constants.notyBody,
                SmallIcon = Constants.androidSmallIcon,
                FireTime = DateTime.Now.AddHours(2)
            };
            AndroidNotificationCenter.SendNotification(notification, Constants.androidNotyChannel);
        }
        #endif

        // iOS notification
        #if UNITY_IOS
        if (Application.platform == RuntimePlatform.IPhonePlayer) {
            var notification = new iOSNotification {
                Identifier = Constants.iosNotyIdentifier,
                Title = Constants.notyTitle,
                Body = Constants.notyBody,
                ShowInForeground = true,
                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                SoundName = Constants.soundName,
                Trigger = new iOSNotificationTimeIntervalTrigger()
                {
                    TimeInterval = new TimeSpan(2, 0, 0),
                    Repeats = false
                }
            };
            iOSNotificationCenter.ScheduleNotification(notification);
        }
        #endif
    }


    // BUTTON ACTION
    public void WatchAd() {
        var isMusicEnabledState = GameController.IsMusicEnabled;
        SFXSoundController.buttonIsClicked = true;
        GameController.IsMusicEnabled = false;

        AdMobManager.ShowRewardedAd(
            (RewardedAd ad) => {
                GameController.IsMusicEnabled = isMusicEnabledState;
                SFXSoundController.healthIsRestored = true;
                GameController.PointOfHealth = 6;
                GameController.TimerIsActive = false;
                GameController.SaveGameData();
            },
            (string error) => {
                errorMessage.SetActive(true);
                InvokeMessage(2.5f);
                Debug.LogError("Failed to show ad: " + error);
            }
        );
    }

    // Load Ad
    private void LoadRewardedAd() {
        AdMobManager.LoadRewardedAd(
            (RewardedAd ad) => {
                rewardedAd = ad;
                Debug.Log("Rewarded ad is loaded and ready to be shown.");
            },
            (string error) => {
                Debug.LogError("Failed to load rewarded ad: " + error);
            }
        );
    }

    private void HideMessage() {
        errorMessage.SetActive(false);
    }

    private void InvokeMessage(float time) {
        Invoke("HideMessage", time);
    }

    struct Constants {
        public const string iosNotyIdentifier = "_health_full";
        public const string androidNotyChannel = "health_channel";
        public const string notyTitle = "Pew Pew";
        public const string notyBody = "🚀 Time to shoot 👾";
        public const string androidSmallIcon = "AppIcon_Android_Notification";
        public const string soundName = "notification.wav";
    }
}
