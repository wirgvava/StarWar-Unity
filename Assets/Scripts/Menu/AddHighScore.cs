using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class AddHighScore : MonoBehaviour
{
    public GameObject menu;
    public TMP_InputField nameField;
    private List<TopScore> topScores = Firestore.topScores;

    // Button action
    public async void AddHighScoreButton()
    {
        if (!string.IsNullOrEmpty(nameField.text))
        {
            menu.SetActive(true);
            this.gameObject.SetActive(false);
            Firestore firestore = new Firestore();
            await firestore.AddTopScore(nameField.text, ScoreManager.score);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("Top Score added successfully");
        }
    }
}