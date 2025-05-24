using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnBossMonsters : MonoBehaviour {
   
    public GameObject bossMonster1;
    public GameObject bossMonster2;
    public GameObject bossMonster3;
    public GameObject bossMonster4;
    public GameObject bossMonster5;
    public GameObject bossMonster6;
    public GameObject bossMonster7;
    public GameObject bossMonster8;
    public GameObject bossMonster9;

    public static bool isActive = false;

    void Update() {
        if (Player.isPlaying) {
            Spawn();
        }
    }

    void Spawn() {
        switch (ScoreManager.score) {
            case 900:
                bossMonster1.SetActive(true);
                isActive = true;
                break;

            case 1500:
                bossMonster2.SetActive(true);
                isActive = true;
                break;

            case 2000:
                bossMonster3.SetActive(true);
                isActive = true;
                break;

            case 2500:
                bossMonster4.SetActive(true);
                isActive = true;
                break;

            case 3000:
                bossMonster5.SetActive(true);
                isActive = true;
                break;

            case 3500:
                bossMonster6.SetActive(true);
                isActive = true;
                break;

            case 4000:
                bossMonster7.SetActive(true);
                isActive = true;
                break;

            case 4500:
                bossMonster8.SetActive(true);
                isActive = true;
                break;

            case 5000:
                bossMonster9.SetActive(true);
                isActive = true;
                break;

            default:
                break;
        }   
    }
}
