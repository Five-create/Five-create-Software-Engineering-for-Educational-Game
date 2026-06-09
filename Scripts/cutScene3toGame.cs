using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cutScene3toGame : MonoBehaviour
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

        // Wait for the duration of the animation (in seconds)
        yield return new WaitForSeconds(1f);  // Change 5f to the duration of your animation

        // After delay, switch to the next scene
        SceneManager.LoadScene("Game"); 
        NPCInteraction.isScene3toGame = true;
        NPCInteraction.fight = true;
        NPCInteraction.isScene4 = true;
        //NPCInteraction.showTalkToFisherman3 = (true);
    }
}
