using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteors : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(this.gameObject);
        }
        else if (collision.tag == "Player")
        {
            Player.isPlaying = false;
            Player.isPlayable = false;
            Player.isGameOver = true;

            player.SetActive(false);

            if (GameController.PointOfHealth != 0)
            {
                GameController.PointOfHealth -= 1;
                GameController.SaveGameData();
            }
        }
    }
}
