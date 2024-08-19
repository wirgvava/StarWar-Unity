using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnMonsters : MonoBehaviour
{
    public GameObject monster1;
    public GameObject monster2;
    public GameObject monster3;
    public GameObject monster4;

    public float maxX;
    public float minX;
    public float timeBetweenSpawn;
    private float spawnTime;

    // Update is called once per frame
    void Update()
    {
        if (Player.isPlaying)
        {
            if (Time.time > spawnTime)
            {
                Spawn();
                spawnTime = Time.time + timeBetweenSpawn;
            }
        }
    }

    void Spawn()
    {
        // Array of all monster prefabs
        GameObject[] monsters = new GameObject[] { monster1, monster2, monster3, monster4 };

        // Get a random index within the monsters array length
        int randomIndex = Random.Range(0, monsters.Length);

        // Pick the monster at the random index
        GameObject monster = monsters[randomIndex];

        float randomX = Random.Range(minX, maxX);

        Instantiate(monster, transform.position + new Vector3(randomX, 0, 0), transform.rotation);
    }
}
