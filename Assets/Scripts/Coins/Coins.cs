using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {

    private GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag(Tags.player);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == Tags.border) {
            Destroy(this.gameObject);
        } else if (collision.tag == Tags.player) {
            SFXSoundController.isMoneyCollected = true;
            GameController.Money += 20;
            Destroy(this.gameObject);
        }
    }
}
