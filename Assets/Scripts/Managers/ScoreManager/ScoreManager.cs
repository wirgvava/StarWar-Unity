using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour {

    public TextMeshProUGUI scoreText;
    public static int score = 0;

    void Start() {
        UpdateScoreText();
    }

    // Call this function whenever the score changes
    public void AddScore(int points) {
        score += points;
        UpdateScoreText();
    }

    public void BossHealth(int health) {
        scoreText.text = "BOSS XP\n" + health.ToString();
    }

    void UpdateScoreText() {
        scoreText.text = "Score\n" + score.ToString();
    }
}
