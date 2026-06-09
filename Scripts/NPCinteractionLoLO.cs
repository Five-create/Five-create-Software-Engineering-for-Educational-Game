using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This line is necessary for Button
using TMPro; // This line is for TextMeshPro, if you're using it for text

public class NPCinteractionLoLO : MonoBehaviour
{
    public static bool questFromLoloAccepted = false;
    private movementControl movementControl;
    public GameObject player;
    public GameObject TalktoLolo;
    public GameObject QuestCanvaLoLo;
    public Button yesButton;            // Reference to the Yes button
    public Button noButton;
    public Button nextButton1;
    public GameObject deniedQuest;
    public GameObject script1;
    public GameObject script2;
    public GameObject script3;
    public GameObject script4;
    public GameObject script5;
    public GameObject script6;
    public GameObject script7;
    public GameObject script8;
    public GameObject questHint;
    public GameObject QuestMarker;
    public GameObject LoloQuestMarker; // Reference to Tito Daryl's QuestMarker
    //public GameObject ObjectiveButtonDisable;
    public GameObject ObjectivePanelLolo;
    public GameObject ObjectivePanelGuideLolo;

    public GameObject ObjectivePanelJose;
    public GameObject ObjectivePanelGuideJose;
    //public GameObject HideButtonDisable;


    private bool playerInRange = false; // To check if the player is in range
    private bool questCompleted = false; // To check if the quest has been completed

    void Start()
    {
        TalktoLolo.SetActive(false);
        deniedQuest.SetActive(false);
        QuestCanvaLoLo.SetActive(false); 
        script1.SetActive(false);
        script2.SetActive(false);
        script3.SetActive(false);
        script4.SetActive(false);
        script5.SetActive(false);
        script6.SetActive(false);
        script7.SetActive(false);
        script8.SetActive(false);
        questHint.SetActive(false);
        QuestMarker.SetActive(false);
        ObjectivePanelJose.SetActive(false);
        ObjectivePanelGuideJose.SetActive(false);
        movementControl = FindObjectOfType<movementControl>();
        //buttons
        yesButton.onClick.AddListener(OnYesButtonClicked);
        noButton.onClick.AddListener(OnNoButtonClicked);
        nextButton1.onClick.AddListener(OnNextButtonClicked);   
    }

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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (!NPCInteraction.questFromFishermanAccepted)
            {
                return; // Prevent further interaction if quest is not accepted
                
            }
            if (questCompleted)
            {
                questHint.SetActive(true); // Show only the quest hint
            }
            else
            {
                TalktoLolo.SetActive(true); // Show the initial interaction options
            }
        }
    }

    IEnumerator HideWarningAfterDelay(float wait)
    {
        yield return new WaitForSeconds(wait); // Wait for the specified time
        deniedQuest.SetActive(false); // Hide the warning
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TalktoLolo.SetActive(false);
            questHint.SetActive(false); // Hide the quest hint if leaving
            playerInRange = false;
        }
    }

    private void OnYesButtonClicked()
    {   
        TalktoLolo.SetActive(false);  // Close the NPC interaction canvas
        QuestCanvaLoLo.SetActive(true);     
        script1.SetActive(true);
        nextButton1.gameObject.SetActive(true);
        movementControl.DisableMovement();
        ObjectivePanelLolo.SetActive(false);
        ObjectivePanelGuideLolo.SetActive(false);

    }
    
    private void OnNoButtonClicked()
    {   
        TalktoLolo.SetActive(false);  // Close the NPC interaction canvas
        deniedQuest.SetActive(true);
        StartCoroutine(HideWarningAfterDelay(2f));
    }

    public void OnNextButtonClicked()
    {
        if (script1.activeSelf) // script1
        {
            script1.SetActive(false);
            script2.SetActive(true);
            script8.SetActive(false);
        }
        else if (script2.activeSelf)
        {
            script2.SetActive(false);
            script3.SetActive(true);
        }
        else if (script3.activeSelf)
        {
            script3.SetActive(false);
            script4.SetActive(true);
        }
        else if (script4.activeSelf)
        {
            script4.SetActive(false);
            script5.SetActive(true);
        }
        else if (script5.activeSelf)
        {
            script5.SetActive(false);
            script6.SetActive(true);
        }
        else if (script6.activeSelf)
        {
            script6.SetActive(false);
            script7.SetActive(true);
        }
        else if (script7.activeSelf)
        {
            script7.SetActive(false);
            script8.SetActive(true);
        }
        else if (script8.activeSelf)
        {
            script8.SetActive(false);   
            QuestCanvaLoLo.SetActive(false);
            nextButton1.gameObject.SetActive(false);
            questFromLoloAccepted = true;
            QuestMarker.SetActive(false); // Deactivate Quest Marker after script8
            questCompleted = true; // Mark quest as completed
            movementControl.EnableMovement();
            ObjectivePanelLolo.SetActive(false);
            ObjectivePanelGuideLolo.SetActive(false);
            ObjectivePanelJose.SetActive(true);
            ObjectivePanelGuideJose.SetActive(true);

            // Show Lolo's QuestMarker when the quest is accepted
        if (LoloQuestMarker != null)
        {
            LoloQuestMarker.SetActive(true);
        }

        }
    }
}
