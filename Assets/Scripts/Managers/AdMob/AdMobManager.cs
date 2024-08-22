using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdMobManager : MonoBehaviour
{
    #if UNITY_ANDROID
    public static string _adUnitId = "ca-app-pub-7386410973532338/5893094814";
    #elif UNITY_IPHONE
    public static string _adUnitId = "ca-app-pub-7386410973532338/7501689951";
    #else
    public static string _adUnitId = "unused";
    #endif

    private static RewardedAd rewardedAd;

    public static void LoadRewardedAd(Action<RewardedAd> onAdLoaded, Action<string> onAdFailed)
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // Create the ad request
        var adRequest = new AdRequest();

        // Load the rewarded ad
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load: " + error);
                    onAdFailed?.Invoke(error.ToString());
                    return;
                }

                Debug.Log("Rewarded ad loaded successfully.");
                rewardedAd = ad;
                onAdLoaded?.Invoke(ad);
            });
    }

    public static void ShowRewardedAd(Action<Reward> onAdCompleted, Action<string> onAdFailed)
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                onAdCompleted?.Invoke(reward);
            });
        }
        else
        {
            Debug.LogError("Ad is not loaded or cannot be shown.");
            onAdFailed?.Invoke("Ad is not loaded or cannot be shown.");
        }
    }
}