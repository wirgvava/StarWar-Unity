using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using Unity.VisualScripting;

public class BossMonster : MonoBehaviour {

    public GameObject spawner;
    public GameObject bossMonsterSprite;
    public GameObject explosion;
    public int health;

    private GameObject player;
    private ScoreManager scoreManager;
    private int initialHealth = 0;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        scoreManager = GameObject.FindWithTag(Tags.currentScore).GetComponent<ScoreManager>();
        initialHealth = health;
    }

    void Update() {
        if (SpawnBossMonsters.isActive) {
            AppearingAnimation();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == Tags.bullet) {
            if (bossMonsterSprite.activeInHierarchy) {
                if (health == 0) {
                    bossMonsterSprite.SetActive(false);
                    explosion.SetActive(true);
                    DeactivateExplosion();
                    scoreManager.AddScore(50);
                    health = initialHealth;
                    SpawnBossMonsters.isActive = false;
                } else {
                    health -= 1;
                    scoreManager.BossHealth(health);
                }
            }
        } else if (collision.tag == Tags.player) {
            Player.isPlaying = false;
            Player.isPlayable = false;
            Player.isGameOver = true;
            SpawnBossMonsters.isActive = false;

            player.SetActive(false);

            if (GameController.PointOfHealth != 0) {
                GameController.PointOfHealth -= 1;
                GameController.SaveGameData();
            }
        }
    }

    private IEnumerator MoveToCameraY(float duration) {
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position;

        float elapsed = 0f;
        while (elapsed < duration) {
            Vector3 endpos = new Vector3(startPos.x, spawner.transform.position.y + 1, startPos.z);
            endPos = endpos;
            transform.position = Vector3.Lerp(startPos, endpos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos; // make sure it's exactly at the end
    }

    private void AppearingAnimation() {
        StartCoroutine(MoveToCameraY(1.5f)); // adjust duration as needed
    }

    private void DeactivateExplosion() {
        Invoke("DisableExplosion", 1.0f);
    }

    private void DisableExplosion() {
        explosion.SetActive(false);
        gameObject.SetActive(false);
    }

}