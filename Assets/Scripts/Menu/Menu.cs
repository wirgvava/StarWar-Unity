using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public TextMeshProUGUI userHighScore;
    public GameObject currentScore;
    public TextMeshProUGUI currentScoreText;
    public GameObject banner;
    public GameObject inGameCanvas;
    public GameObject market;
    public GameObject leaderboard;
    public GameObject settings;

    void Start()
    {
        userHighScore.text = GameController.UserHighScore.ToString();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Player.isPlaying)
        {
            menu.SetActive(false);
            banner.SetActive(false);
            inGameCanvas.SetActive(true);
            currentScore.SetActive(false);
            ScoreManager.score = 0;
            Player.isPlayable = true;
            Player.isGameOver = false;
        }
        else 
        {
            menu.SetActive(true);
            banner.SetActive(true);
            inGameCanvas.SetActive(false);
        }
        
        CheckForHighScore();
    }

    void OnEnable()
    {
        if (ScoreManager.score > 0)
        {
            currentScore.SetActive(true);
            currentScoreText.text = ScoreManager.score.ToString();
        }
    }

    // Button actions 
    public void OpenMarket()
    {
        menu.SetActive(false);
        market.SetActive(true);
    }

    public void OpenLeaderboard()
    {
        menu.SetActive(false);
        leaderboard.SetActive(true);
    }

    public void OpenSettings()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }

    private void CheckForHighScore()
    {
        if (ScoreManager.score > GameController.UserHighScore)
        {
            GameController.UserHighScore = ScoreManager.score;
            GameController.SaveGameData();
            userHighScore.text = ScoreManager.score.ToString();
        }
    }
}
