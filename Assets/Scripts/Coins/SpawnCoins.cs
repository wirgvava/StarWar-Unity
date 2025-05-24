using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SpawnCoins : MonoBehaviour {

    public GameObject coin;

    public float maxX;
    public float minX;
    public float timeBetweenSpawn;
    private float spawnTime;
    
    // Update is called once per frame
    void Update() {
        if (ScoreManager.score > 800) {
            if (Player.isPlaying) {
                if (Time.time > spawnTime) {
                    Spawn();
                    spawnTime = Time.time + timeBetweenSpawn;
                }
            }
        }
    }

    void Spawn() {
        float randomX = Random.Range(minX, maxX);
        Instantiate(coin, transform.position + new Vector3(randomX, 0, 0), transform.rotation);
    }
}
