using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnBossMonsters : MonoBehaviour {
   
    public GameObject bossMonster1;
    public static bool isActive = false;

    void Update() {
        if (Player.isPlaying) {
            Spawn();
        }
    }

    void Spawn() {
        switch (ScoreManager.score) {
        case 20:
            bossMonster1.SetActive(true);
            isActive = true;
            break;
        default:
            break;
        }   
    }
}
