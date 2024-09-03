using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject menu;
    public GameObject disabledMusicSlash;
    public GameObject disabledSFXSlash;

    void Update()
    {
        disabledMusicSlash.SetActive(!GameController.IsMusicEnabled);
        disabledSFXSlash.SetActive(!GameController.IsSFXEnabled);
    }

    // Button action
    public void ToggleMusic()
    {
        SFXSoundController.buttonIsClicked = true;
        GameController.IsMusicEnabled = !GameController.IsMusicEnabled;
    }

    public void ToggleSFX()
    {
        SFXSoundController.buttonIsClicked = true;
        GameController.IsSFXEnabled = !GameController.IsSFXEnabled;
    }

    public void CloseButtonAction()
    {
        SFXSoundController.buttonIsClicked = true;
        GameController.SaveGameData();
        menu.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
