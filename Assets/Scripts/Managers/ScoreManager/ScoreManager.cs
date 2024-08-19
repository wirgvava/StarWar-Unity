using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static int score = 0;

    void Start()
    {
        UpdateScoreText();
    }

    // Call this function whenever the score changes
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
