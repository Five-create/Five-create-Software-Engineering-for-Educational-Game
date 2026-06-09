using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SceneIntro : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject replayButton;
    public GameObject continueButton;
    public GameObject skip;

    private SceneSwitchIntro sceneswitchintro;

    private void Start()
    {
        replayButton.SetActive(false);
        continueButton.SetActive(false);
        sceneswitchintro = FindObjectOfType<SceneSwitchIntro>();

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

    //public void Continue()
    //{
        //sceneswitchintro.GoToPlay();
    //}
}
