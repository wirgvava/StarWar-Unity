using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullets : MonoBehaviour
{
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
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

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
        if (player.ship_1.activeInHierarchy)
        {
            Vector3 position = new Vector3(player.transform.position.x, player.transform.position.y + 0.4f, player.transform.position.z);
            Instantiate(bullet_1, position, transform.rotation);
        }
        else if (player.ship_2.activeInHierarchy)
        {
            Vector3 position = new Vector3(player.transform.position.x, player.transform.position.y + 0.6f, player.transform.position.z);
            Instantiate(bullet_2, position, transform.rotation);
        }
        else if (player.ship_3.activeInHierarchy)
        {
            Vector3 position = new Vector3(player.transform.position.x, player.transform.position.y + 0.8f, player.transform.position.z);
            Instantiate(bullet_3, position, transform.rotation);
        }
        else if (player.ship_4.activeInHierarchy)
        {
            Vector3 position = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
            Instantiate(bullet_4, position, transform.rotation);
        }
        else if (player.ship_5.activeInHierarchy)
        {
            Vector3 position = new Vector3(player.transform.position.x, player.transform.position.y + 0.75f, player.transform.position.z);
            Instantiate(bullet_5, position, transform.rotation);
        }
        else if (player.ship_6.activeInHierarchy)
        {
            Vector3 position = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
            Instantiate(bullet_6, position, transform.rotation);
        }
    }
}
