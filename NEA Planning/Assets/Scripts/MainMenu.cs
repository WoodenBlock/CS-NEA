using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenu;

    public GameObject settingsMenu;

    public void playGame ()
    {
        // Finds current scene's index, and then loads the scene at the next index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void openSettings()
    {
        // Activates settingsMenu object while deactivating mainMenu object 
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void closeSettings()
    {
        // Activates mainMenu object while deactivating settingsMenu object 
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void quitGame() {
        Application.Quit();
    }
    
}
