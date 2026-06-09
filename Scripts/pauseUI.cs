using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseUI : MonoBehaviour
{
    public GameObject menu;
    public GameObject menuclose;
    public GameObject pause;
    public GameObject play;
    public GameObject settings;
    public GameObject mainmenu;
    public GameObject pausePanel;
    public GameObject settingsPanel;
    private gotoMainMenu Mainmenu;
    private bool isPaused = false;

    void Start()
    {
        Mainmenu = FindObjectOfType<gotoMainMenu>();
        menuclose.SetActive(false);
        pause.SetActive(true);
        settings.SetActive(false);
        mainmenu.SetActive(false);
        play.SetActive(false);
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void menuclick()
    {
        // Show the menu UI without pausing the game
        menu.SetActive(false);
        menuclose.SetActive(true);
        settings.SetActive(true);
        mainmenu.SetActive(true);

        // Show the correct UI based on whether the game is paused
        if (isPaused)
        {
            pause.SetActive(false);
            play.SetActive(true);  // Display the "Resume" button
        }
        else
        {
            pause.SetActive(true);  // Display the "Pause" button
            play.SetActive(false);
        }
    }

    public void menucloseclick()
    {
        // Close the pause menu UI and resume the game if it was paused
        menu.SetActive(true);
        menuclose.SetActive(false);
        settings.SetActive(false);
        mainmenu.SetActive(false);
        settingsPanel.SetActive(false);

        // If the game was paused, resume it
        if (isPaused)
        {
            ResumeGame();
        }
    }

    public void pauseclick()
    {
        // Pause the game and show the pause UI
        PauseGame();
    }

    public void playclick()
    {
        // Resume the game and show the play UI
        ResumeGame();
    }

    public void mainmenuclick()
    {
        // Resume the game and return to the main menu
        ResumeGame();
        Mainmenu.GoToPlay();
    }

    public void settingsclick()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OnSaveButtonClick()
    {
        GameObject.FindObjectOfType<saveAndLoadGame>().SaveGame();
    }

    public void PauseGame()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
        // Freeze game time
        Time.timeScale = 0f;
        isPaused = true;

        // Update UI elements
        menu.SetActive(false);
        menuclose.SetActive(true);
        pause.SetActive(false);
        settings.SetActive(true);
        mainmenu.SetActive(true);
        play.SetActive(true);  // Show the "Resume" button
    }

    public void ResumeGame()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(false);
        // Resume game time
        Time.timeScale = 1f;
        isPaused = false;

        // Update UI elements
        menu.SetActive(true);
        menuclose.SetActive(false);
        settings.SetActive(false);
        mainmenu.SetActive(false);
        play.SetActive(false);  // Hide the "Resume" button
        pause.SetActive(true);  // Show the "Pause" button
    }

    void Update()
    {
        // Check if the pause button (KeyCode.P) is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Toggle pause state
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }
}