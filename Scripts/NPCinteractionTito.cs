using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This line is necessary for Button
using TMPro; // This line is for TextMeshPro, if you're using it for text

public class NPCinteractionTito : MonoBehaviour
{
    public static bool questFromTitoAccepted = false;
    private movementControl movementControl;
    public GameObject player;
    public GameObject TalktoTito;
    public GameObject QuestCanvaTito;
    private bool playerInRange = false;
    public Button yesButton;            // Reference to the Yes button
    public Button noButton; 
    public Button nextButton1;
    public GameObject script1;
    public GameObject script2;
    public GameObject script3;
    public GameObject script4;
    public GameObject script5;  
    public GameObject deniedQuest;
    public GameObject questHint; 
    public GameObject QuestMarker;
    private bool questCompleted = false;
    public GameObject TitoQuestMarker; // Reference to Maria's QuestMarker
    //public GameObject ObjectiveButtonDisable;
    //public GameObject ObjectivePanelDisable;
    //public GameObject HideButtonDisable;
    public GameObject ObjectivePanelJose;
    public GameObject ObjectivePanelGuideJose;
    public GameObject ObjectivePanelMaria;
    public GameObject ObjectivePanelGuideMaria;
    void Start()
    {
        TalktoTito.SetActive(false);
        QuestCanvaTito.SetActive(false); 
        script1.SetActive(false);
        script2.SetActive(false);
        script3.SetActive(false);
        script4.SetActive(false);
        script5.SetActive(false);
        questHint.SetActive(false);
        QuestMarker.SetActive(false);
        ObjectivePanelMaria.SetActive(false);
        ObjectivePanelGuideMaria.SetActive(false);
        movementControl = FindObjectOfType<movementControl>();
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
            if (!NPCinteractionLoLO.questFromLoloAccepted)
            {
                return; // Prevent further interaction if quest is not accepted
            }
            if (questCompleted)
            {
                questHint.SetActive(true);
            }
            else
            {
                TalktoTito.SetActive(true); 
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            TalktoTito.SetActive(false); 
            QuestCanvaTito.SetActive(false); 
            questHint.SetActive(false); // Hide the quest hint when the player leaves
        }
    }
    
    private void OnYesButtonClicked()
    {   
        TalktoTito.SetActive(false);
        QuestCanvaTito.SetActive(true); 
        script1.SetActive(true);
        movementControl.DisableMovement();
        ObjectivePanelJose.SetActive(false);
        ObjectivePanelGuideJose.SetActive(false);
    }

    IEnumerator HideWarningAfterDelay(float wait)
    {
        yield return new WaitForSeconds(wait); // Wait for the specified time
        deniedQuest.SetActive(false); // Hide the warning
    }

    public void OnNextButtonClicked()
    {
        if (script1.activeSelf) // script1
        {
            script1.SetActive(false);
            script2.SetActive(true);
            script5.SetActive(false);
        }
        else if (script2.activeSelf)
        {
            script2.SetActive(false);
            script3.SetActive(true);
        }
        else if(script3.activeSelf)
        {
            script3.SetActive(false);
            script4.SetActive(true);
        }
        else if(script4.activeSelf)
        {
            script4.SetActive(false);
            script5.SetActive(true);
        }
        else if(script5.activeSelf){
            script5.SetActive(false);
            QuestCanvaTito.SetActive(false);
            nextButton1.gameObject.SetActive(false);
            QuestMarker.SetActive(false);
            questFromTitoAccepted = true;
            questCompleted = true; 
            movementControl.EnableMovement();   
            ObjectivePanelMaria.SetActive(true);
            ObjectivePanelGuideMaria.SetActive(true);

            if (TitoQuestMarker != null)
            {
                TitoQuestMarker.SetActive(true);
            }
            }
    }

    private void OnNoButtonClicked()
    {   
        TalktoTito.SetActive(false);  // Close the NPC interaction canvas
        deniedQuest.SetActive(true);
        StartCoroutine(HideWarningAfterDelay(2f));
    }
}
