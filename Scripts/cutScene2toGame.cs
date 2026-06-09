using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cutScene2toGame : MonoBehaviour
{
    public Animator transition;
    
    public void GoToPlay()
    {
        // Start the coroutine to delay scene switch
        StartCoroutine(DelayedSceneSwitch());
    }

    private IEnumerator DelayedSceneSwitch()
    {
        transition.SetTrigger("Start");
        NPCInteraction.isScene2toGame = true;

        // Wait for the duration of the animation (in seconds)
        yield return new WaitForSeconds(1f);  // Change 5f to the duration of your animation

        // After delay, switch to the next scene
        SceneManager.LoadScene("Game"); 
        NPCInteraction.showTalkToFisherman3 = (true);
        NPCInteraction.isQuestGuide2 = (true);

    }
}
