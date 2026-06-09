using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchIntro : MonoBehaviour
{
    public Animator transition;
    public void GoToPlay()
    {
        //SceneManager.LoadScene("Game");
        StartCoroutine(DelayedSceneSwitch());
    }

    private IEnumerator DelayedSceneSwitch()
    {
        transition.SetTrigger("Start");

        // Wait for the duration of the animation (in seconds)
        yield return new WaitForSeconds(1f);  // Change 5f to the duration of your animation

        // After delay, switch to the next scene
        SceneManager.LoadScene("Game");
    }
}
