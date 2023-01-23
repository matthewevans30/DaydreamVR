using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractions : MonoBehaviour
{
    public TransitionManager transitionManager;

    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;

    bool customMusicToggle;

    public void StartGame()
    {
        transitionManager.GoToScene(1);
    }

    public void DisplayOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void CustomMusicToggle()
    {
        if (!customMusicToggle)
        {
            customMusicToggle = true;
            Browser.useCustomFiles = true;
        }
        else
        {
            customMusicToggle = false;
            Browser.useCustomFiles = false;
        }
    }

    public void BackToMenu()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void Credits()
    {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ButtonSelected()
    {

    }
}
