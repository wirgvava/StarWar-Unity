using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject description;
    public GameObject watchAdButton;
    public GameObject restartButton;
    private GameObject player;
    private bool isAdAlreadyUsed = false;
    private bool isPresentedGameOverPanel = false;

    // Ad
    private RewardedAd rewardedAd;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.isGameOver && !isPresentedGameOverPanel)
        {
            isPresentedGameOverPanel = true;
            gameOverPanel.SetActive(true);
            customizeUI();
            LoadRewardedAd();
            GameController.SaveGameData();
        }    
    }

    void customizeUI()
    {
        if (isAdAlreadyUsed)
        {
            watchAdButton.SetActive(false);
            description.SetActive(false);
            restartButton.transform.position = new Vector3(gameOverPanel.transform.position.x, restartButton.transform.position.y, restartButton.transform.position.z);
        }
    }

    // Button Actions
    public void Restart()
    {
        isPresentedGameOverPanel = false;
        SFXSoundController.buttonIsClicked = true;
        Player.isPlaying = false;
        Player.isPlayable = true;
        Player.isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void WatchAdAndContinuePlaying()
    {
        SFXSoundController.buttonIsClicked = true;
        
        AdMobManager.ShowRewardedAd(
            (Reward reward) =>
            {
                gameOverPanel.SetActive(false);
                player.gameObject.SetActive(true);
                Player.isPlaying = true;
                Player.isPlayable = true;
                Player.isGameOver = false;
                isAdAlreadyUsed = true;

                Debug.Log("Ad completed. User rewarded with: " + reward.Amount);
            },
            (string error) =>
            {
                Debug.LogError("Failed to show ad: " + error);
            }
        );
    }

    private void LoadRewardedAd()
    {
        AdMobManager.LoadRewardedAd(
            (RewardedAd ad) =>
            {
                rewardedAd = ad;
                Debug.Log("Rewarded ad is loaded and ready to be shown.");
            },
            (string error) =>
            {
                Debug.LogError("Failed to load rewarded ad: " + error);
            }
        );
    }
}
