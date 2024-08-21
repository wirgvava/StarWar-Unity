using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject description;
    public GameObject watchAdButton;
    public GameObject restartButton;
    private GameObject player;
    private bool isAdAlreadyUsed = false;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.isGameOver)
        {
            gameOverPanel.SetActive(true);
            customizeUI();
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
        SFXSoundController.buttonIsClicked = true;
        Player.isPlaying = false;
        Player.isPlayable = true;
        Player.isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void WatchAdAndContinuePlaying()
    {
        // TODO: Watch Ad Logic

        SFXSoundController.buttonIsClicked = true;
        gameOverPanel.SetActive(false);
        player.gameObject.SetActive(true);
        Player.isPlaying = true;
        Player.isPlayable = true;
        Player.isGameOver = false;
        isAdAlreadyUsed = true;
    }
}
