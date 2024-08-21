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
    public GameObject addHighScorePanel;

    private List<TopScore> topScores = Firestore.topScores;

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
            Player.isAddedHighScoreToLeaderboard = false;
        }
        else 
        {
            menu.SetActive(true);
            banner.SetActive(true);
            inGameCanvas.SetActive(false);
        }
        
        CheckForHighScore();
        CheckForLeaderboardHighScore();
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
        SFXSoundController.buttonIsClicked = true;
        menu.SetActive(false);
        market.SetActive(true);
    }

    public void OpenLeaderboard()
    {
        SFXSoundController.buttonIsClicked = true;
        menu.SetActive(false);
        leaderboard.SetActive(true);
    }

    public void OpenSettings()
    {
        SFXSoundController.buttonIsClicked = true;
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

    private void CheckForLeaderboardHighScore()
    {
        if (!Player.isPlaying && !Player.isAddedHighScoreToLeaderboard) {
            foreach (TopScore topScore in topScores)
            {
                if (ScoreManager.score > topScore.Score) 
                {
                    menu.SetActive(false);
                    addHighScorePanel.SetActive(true);
                    Player.isAddedHighScoreToLeaderboard = true;
                    Player.isPlayable = false; 
                    break;
                }
            }
        }
    }
}
