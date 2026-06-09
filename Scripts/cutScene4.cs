using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class cutScene4 : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject replayButton;
    public GameObject continueButton;
    public GameObject skip;
    private void Start()
    {
        replayButton.SetActive(false);
        continueButton.SetActive(false);

        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        replayButton.SetActive(true);
        continueButton.SetActive(true);
    }

    public void WatchAgain()
    {
        replayButton.SetActive(false);
        continueButton.SetActive(false);
        videoPlayer.Play();
    }

    public void Skip()
    {
        videoPlayer.Stop();
        replayButton.SetActive(true);
        continueButton.SetActive(true);
    }

    public void continueBtn()
    {
        SceneManager.LoadScene("mainmenu");
    }
}
