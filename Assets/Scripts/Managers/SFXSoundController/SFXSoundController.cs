using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonClicked;
    public AudioClip buy;
    public AudioClip error;
    public AudioClip healthRestored;
    public AudioClip collectMoney;

    public static bool buttonIsClicked = false;
    public static bool isBought = false;
    public static bool isErrorPresented = false;
    public static bool healthIsRestored = false;
    public static bool isMoneyCollected = false;

    // Update is called once per frame
    void Update()
    {
        if (GameController.IsSFXEnabled)
        {
            CheckSoundsIfNeeded();
        }
    }

    private void CheckSoundsIfNeeded()
    {
        if (buttonIsClicked)
        {
            audioSource.clip = buttonClicked;
            audioSource.Play();
            buttonIsClicked = false;
        }

        if (isBought)
        {
            audioSource.clip = buy;
            audioSource.Play();
            isBought = false;
        }

        if (isErrorPresented)
        {
            audioSource.clip = error;
            audioSource.Play();
            isErrorPresented = false;
        }

        if (healthIsRestored)
        {
            audioSource.clip = healthRestored;
            audioSource.Play();
            healthIsRestored = false;
        }

        if (isMoneyCollected)
        {
            audioSource.clip = collectMoney;
            audioSource.Play();
            isMoneyCollected = false;
        }
    }
}
