using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Health : MonoBehaviour
{
    public UnityEngine.UI.Image heart_1;
    public UnityEngine.UI.Image heart_2;
    public UnityEngine.UI.Image heart_3;

    // Sprite Images
    public Sprite heart_empty;
    public Sprite heart_half;
    public Sprite heart_full;

    public GameObject timer;
    public GameObject dragToPlayMessage;

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        switch (GameController.PointOfHealth)
        {
            case 0:
            heart_1.sprite = heart_empty;
            heart_2.sprite = heart_empty;
            heart_3.sprite = heart_empty;
            Player.isPlayable = false;
            timer.SetActive(true);
            break;

            case 1:
            heart_1.sprite = heart_empty;
            heart_2.sprite = heart_empty;
            heart_3.sprite = heart_half;
            Player.isPlayable = true;
            timer.SetActive(false);
            break;

            case 2:
            heart_1.sprite = heart_empty;
            heart_2.sprite = heart_empty;
            heart_3.sprite = heart_full;
            Player.isPlayable = true;
            timer.SetActive(false);
            break;

            case 3:
            heart_1.sprite = heart_empty;
            heart_2.sprite = heart_half;
            heart_3.sprite = heart_full;
            Player.isPlayable = true;
            timer.SetActive(false);
            break;

            case 4:
            heart_1.sprite = heart_empty;
            heart_2.sprite = heart_full;
            heart_3.sprite = heart_full;
            Player.isPlayable = true;
            timer.SetActive(false);
            break;

            case 5:
            heart_1.sprite = heart_half;
            heart_2.sprite = heart_full;
            heart_3.sprite = heart_full;
            Player.isPlayable = true;
            timer.SetActive(false);
            break;

            case 6:
            heart_1.sprite = heart_full;
            heart_2.sprite = heart_full;
            heart_3.sprite = heart_full;
            Player.isPlayable = true;
            timer.SetActive(false);
            break;
        }

        dragToPlayMessage.SetActive(!GameController.TimerIsActive);
    }
}
