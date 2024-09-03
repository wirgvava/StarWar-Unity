using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public GameObject menu;
    public GameObject player;
    public GameObject leaderboardCellPrefab;
    public Transform contentPanel;
    private List<TopScore> topScores = Firestore.topScores;
    // Start is called before the first frame update
    void Start()
    {
        PopulateLeaderboard();
    }

    private void PopulateLeaderboard()
    {
        ClearLeaderboard();

        for (int i = 0; i < topScores.Count; i++)
        {
            GameObject cell = Instantiate(leaderboardCellPrefab, contentPanel);
            cell.transform.Find("Rank").GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
            cell.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = topScores[i].Name;
            cell.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = topScores[i].Score.ToString();
        }
    }

    private void ClearLeaderboard()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
    }

    // Button action
    public void CloseLeaderboard()
    {
        SFXSoundController.buttonIsClicked = true;
        menu.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
