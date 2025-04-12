using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullets : MonoBehaviour {

    public GameObject bullet_1;
    public GameObject bullet_2;
    public GameObject bullet_3;
    public GameObject bullet_4;
    public GameObject bullet_5;
    public GameObject bullet_6;
    private Player player;
    public float timeBetweenSpawn;
    private float spawnTime;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindWithTag(Tags.player).GetComponent<Player>();
    }

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
        if (player.ship_1.activeInHierarchy) {
            spawnAt(0.3f, bullet_1);
        } else if (player.ship_2.activeInHierarchy){
            spawnAt(0.5f, bullet_2);
        } else if (player.ship_3.activeInHierarchy) {
            spawnAt(0.6f, bullet_3);
        } else if (player.ship_4.activeInHierarchy) {
            spawnAt(0.4f, bullet_4);
        } else if (player.ship_5.activeInHierarchy) {
            spawnAt(0.6f, bullet_5);
        } else if (player.ship_6.activeInHierarchy) {
            spawnAt(0.4f, bullet_6);
        }
    }

    void spawnAt(float y, GameObject gameObject) {
        Vector3 position = new Vector3(player.transform.position.x, player.transform.position.y + y, player.transform.position.z);
        Instantiate(gameObject, position, transform.rotation);
    }
}
