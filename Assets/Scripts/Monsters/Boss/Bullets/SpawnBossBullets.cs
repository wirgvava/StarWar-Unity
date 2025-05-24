using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnBossBullets : MonoBehaviour {

    public GameObject[] bullets;

    public float maxX;
    public float minX;
    public float timeBetweenSpawn;
    private float spawnTime;

    // Update is called once per frame
    void Update() {
        if (Player.isPlaying) {
            if (Time.time > spawnTime) {
                Spawn();
                spawnTime = Time.time + timeBetweenSpawn;
            }
        }
    }

    void Spawn() {
        if (SpawnBossMonsters.isActive) {
            SpawnBulletsAfter(1);
        }
    }
    
     private void SpawnBulletsAfterDelay() {
        int randomIndex = Random.Range(0, bullets.Length);
        GameObject bullet = bullets[randomIndex];

        float randomX = Random.Range(minX, maxX);
        Instantiate(bullet, transform.position + new Vector3(randomX, 0, 0), transform.rotation);
    }

    private void SpawnBulletsAfter(float time) {
        Invoke("SpawnBulletsAfterDelay", time);
    }
}
