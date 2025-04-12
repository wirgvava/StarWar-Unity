using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    public GameObject monsterSprite;
    public GameObject explosion;
    private GameObject player;
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        scoreManager = GameObject.FindWithTag(Tags.currentScore).GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == Tags.border) {
            Destroy(this.gameObject);
            scoreManager.AddScore(2);
        } else if (collision.tag == Tags.bullet) {
            monsterSprite.SetActive(false);
            explosion.SetActive(true);
            Destroy(this.gameObject, 0.2f);
            scoreManager.AddScore(2);
        } else if (collision.tag == Tags.player) {
            Player.isPlaying = false;
            Player.isPlayable = false;
            Player.isGameOver = true;

            player.SetActive(false);

            if (GameController.PointOfHealth != 0) {
                GameController.PointOfHealth -= 1;
                GameController.SaveGameData();
            }
        }
    }
}
