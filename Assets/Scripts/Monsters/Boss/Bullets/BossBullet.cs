using UnityEngine;

public class BossBullet : MonoBehaviour {

    // public GameObject bossBullet;
    // public GameObject explosion;
    private GameObject player;
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        scoreManager = GameObject.FindWithTag(Tags.currentScore).GetComponent<ScoreManager>();
    }

    void Update() {
       
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == Tags.border) {
            Destroy(this.gameObject);
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