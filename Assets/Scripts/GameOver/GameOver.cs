using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class GameOver : MonoBehaviour {

    public GameObject gameOverPanel;
    public GameObject description;
    public GameObject watchAdButton;
    public GameObject restartButton;
    public GameObject errorMessage;
    private GameObject player;
    private bool isAdAlreadyUsed = false;
    private bool isPresentedGameOverPanel = false;

    // Ad
    private RewardedAd rewardedAd;
    
    void Start() {
        errorMessage.SetActive(false);
        player = GameObject.FindGameObjectWithTag(Tags.player);
    }

    // Update is called once per frame
    void Update() {
        if (Player.isGameOver && !isPresentedGameOverPanel) {
            isPresentedGameOverPanel = true;
            gameOverPanel.SetActive(true);
            customizeUI();
            LoadRewardedAd();
            GameController.SaveGameData();
        }    
    }

    void customizeUI() {
        if (isAdAlreadyUsed) {
            watchAdButton.SetActive(false);
            description.SetActive(false);
            restartButton.transform.position = new Vector3(gameOverPanel.transform.position.x, restartButton.transform.position.y, restartButton.transform.position.z);
        }
    }

    // Button Actions
    public void Restart() {
        isPresentedGameOverPanel = false;
        SFXSoundController.buttonIsClicked = true;
        Player.isPlaying = false;
        Player.isPlayable = true;
        Player.isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void WatchAdAndContinuePlaying() {
        var isMusicEnabledState = GameController.IsMusicEnabled;
        SFXSoundController.buttonIsClicked = true;
        GameController.IsMusicEnabled = false;
        
        AdMobManager.ShowRewardedAd(
            (RewardedAd ad) => {
                ad.OnAdFullScreenContentClosed += () => {
                    GameController.IsMusicEnabled = isMusicEnabledState;
                    gameOverPanel.SetActive(false);
                    isPresentedGameOverPanel = false;
                    player.SetActive(true);
                    Player.isPlaying = true;
                    Player.isPlayable = true;
                    Player.isGameOver = false;
                    isAdAlreadyUsed = true;
                };
            },
            (string error) => {
                errorMessage.SetActive(true);
                Invoke("HideMessage", 2.5f);
                Debug.LogError("Failed to show ad: " + error);
            }
        );
    }

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
}
