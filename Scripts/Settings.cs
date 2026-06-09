using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject slider;
    public GameObject play;
    public GameObject quit;
    public GameObject loadGame;
    public GameObject settings;
    public GameObject back;

    void Start ()
    {
        slider.SetActive(false);
        back.SetActive(false);
    }
    public void GoToSettings()
    {
        play.SetActive(false);
        quit.SetActive(false);
        loadGame.SetActive(false);
        settings.SetActive(false);
        slider.SetActive(true);
        back.SetActive(true);

    }

    public void Back()
    {
        play.SetActive(true);
        quit.SetActive(true);
        loadGame.SetActive(true);
        settings.SetActive(true);
        slider.SetActive(false);
        back.SetActive(false);
    }
}
