using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCInteractionLapu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lapuPanel;
    public GameObject LapuGuide;
    public GameObject player;
    public GameObject TalkToLapu;
    public GameObject LapuQuestPanel;
    public Button yesButton;
    public Button noButton;
    public Button nextButton;
    public GameObject deniedQuest;
    private bool playerInRange = false;
    public GameObject script1;
    public GameObject script2;
    public GameObject script3;
    public GameObject script4;
    public GameObject script5;
    public GameObject script6;
    public static bool questcomplete = false;
    public GameObject LapuQuestMarker;
    public Button Continuebtn;
    private movementControl movementControl;
    public Animator transition;

    void Start()
    {
        TalkToLapu.SetActive(false);
        LapuQuestPanel.SetActive(false);  
        script1.SetActive(false);
        script2.SetActive(false);
        script3.SetActive(false);
        script4.SetActive(false);
        script5.SetActive(false);
        script6.SetActive(false);
        lapuPanel.SetActive(false);
        LapuGuide.SetActive(false);
        noButton.onClick.AddListener(OnNoButtonLapuClicked);
        yesButton.onClick.AddListener(OnYesButtonLapuClicked);
        nextButton.onClick.AddListener(OnNetButtonLapuClicked);  
        Continuebtn.gameObject.SetActive(false);
        Continuebtn.onClick.AddListener(OnContinueBtnClicked);
        movementControl = FindObjectOfType<movementControl>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is in range, rotate towards the player
        if (playerInRange && player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0; // Keep the NPC's rotation on the Y-axis only
            Quaternion rotation = Quaternion.LookRotation(direction);

            float rotationSpeed = 5f; // Adjust this value for faster/slower rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator HideWarningAfterDelay(float wait)
    {
        yield return new WaitForSeconds(wait); // Wait for the specified time
        deniedQuest.SetActive(false); // Hide the warning
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (questcomplete)
            {
                Debug.Log("Quest is already complete.");
            }
            else if (NPCInteraction.FishermanQuestCompleted)
            {
                TalkToLapu.SetActive(true);  // Activate TalkToLapu if FishermanQuestCompleted
            }
            else if (!NPCInteraction.FishermanQuestCompleted)
            {
                TalkToLapu.SetActive(false);  // Activate TalkToLapu if FishermanQuestCompleted
            }
            else
            {
                TalkToLapu.SetActive(true);  // Activate TalkToLapu if no other conditions
            }
        }
    }

  


    void OnTriggerExit(Collider other)  
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            TalkToLapu.SetActive(false);
        }
    }

    private void OnNoButtonLapuClicked(){

        deniedQuest.SetActive(true);
        StartCoroutine(HideWarningAfterDelay(2f));
        TalkToLapu.SetActive(false);
    }

    private void OnYesButtonLapuClicked(){
        movementControl.DisableMovement();
        lapuPanel.SetActive(false);
        LapuGuide.SetActive(false);
        TalkToLapu.SetActive(false);
        LapuQuestPanel.SetActive(true);
        script1.SetActive(true);
    }

    private void OnNetButtonLapuClicked(){
        if(script1.activeSelf){
            script1.SetActive(false);
            script2.SetActive(true);
        }
        else if(script2.activeSelf){
            script2.SetActive(false);
            script3.SetActive(true);
        }else if(script3.activeSelf){
            script3.SetActive(false);
            script4.SetActive(true);
        }else if(script4.activeSelf){
            script4.SetActive(false);
            script5.SetActive(true);
        }else if(script5.activeSelf){
            script5.SetActive(false);
            script6.SetActive(true);
            nextButton.gameObject.SetActive(false);
            Continuebtn.gameObject.SetActive(true);
        }
    }

    private IEnumerator DelayedSceneSwitch()
    {
        transition.SetTrigger("Start");

        // Wait for the duration of the animation (in seconds)
        yield return new WaitForSeconds(1f);  // Change 5f to the duration of your animation

        // After delay, switch to the next scene
        SceneManager.LoadScene("cutscene3");
    }

    private void SaveGameState()
    {
        // Save player position
        GameObject player = GameObject.FindWithTag("Player");
        PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);

        PlayerPrefs.Save(); // Save all the PlayerPrefs values
    }

    private void OnContinueBtnClicked(){
        script6.SetActive(false);
        TalkToLapu.SetActive(false);
        LapuQuestPanel.SetActive(false);
        questcomplete = true;
        LapuQuestMarker.SetActive(false);
        movementControl.EnableMovement();
        StartCoroutine(DelayedSceneSwitch());
    }
}
