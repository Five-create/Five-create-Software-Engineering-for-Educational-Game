using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This line is necessary for Button
using TMPro; // This line is for TextMeshPro, if you're using it for text
using UnityEngine.SceneManagement;

public class NPCInteraction : MonoBehaviour{
    public static bool isInQuest1;
    public static bool isInQuest2;
    public static bool isInQuest3;
    public static bool questFromFishermanAccepted = false;
    public GameObject QuestMarker1;
    private movementControl movementControl;
    public GameObject player;
    public GameObject TalktoFisherman;
    public GameObject deniedQuest;
    public GameObject questCanvas;     // Canvas for quest details
    public Button yesButton;            // Reference to the Yes button
    public Button noButton;
    public Button nextButton1;
    public GameObject script1;
    public GameObject script2; //script2
    public GameObject script3;
    public GameObject script4;
    public GameObject script5;
    public GameObject script6;
    public GameObject script1t;
    public GameObject script2t;
    public GameObject script3t;
    public GameObject script4t;
    public GameObject script5t;
    public GameObject script6t;
    public GameObject script7t;
    public GameObject script8t;
    public GameObject script9t;
    public GameObject script10t;
    public GameObject script11t;
    public GameObject script12t;
    public Button nextbutton2;
    public GameObject acceptQuest;
    public GameObject questHint;
    public Button acceptbutton;
    public Button declinebutton;
    public static bool questAccepted = true;
    public static bool questaccepted3 = true;
    public static bool questaccepted2 = true;
    public static bool isQuestGuide2 = false;
    private CurrentQuest currentQuest;
    public GameObject QuestMarker;
    public GameObject FishermanQuestMarker; // Reference to Lolo's QuestMarker
    public GameObject QuestGuide;
    public GameObject QuestGuide2;
    private bool playerInRange = false; // To check if the player is in range
    public GameObject TalkToFisherman2;
    public GameObject FisherManPanel2;
    public Button yesButton2;            // Reference to the Yes button
    public Button noButton2;
    public Button continueBtn;
    public GameObject AndresQuestCompleted;
    public GameObject AndresQuestGuide;
    public GameObject TalkToFisherman3;
    public GameObject FisherManPanel3;
    public GameObject scripts1;
    public GameObject scripts2;
    public GameObject scripts3;
    public GameObject scripts4;
    public GameObject DeniedQuest3;
    public Button yesButton3;
    public Button noButton3;
    public Button nextButton3;
    public Animator transition;
    public static bool isScene2toGame = false;
    public static bool isScene3toGame = false;
    public static bool showTalkToFisherman3 = false;
    public GameObject QuestObjectiveLapulapu;
    public GameObject QuestGuideLapulapu;
    public static bool FishermanQuestCompleted = false; // New flag
    public GameObject accepttheQuest;
    public Button Acceptbtn1;
    public Button Declinebtn1;
    public GameObject checkpoint;
    public GameObject checkpoint2;
    public static bool fight = false;
    public static bool isScene4 = false;


    void Start()
    {
        TalktoFisherman.SetActive(false);
        questCanvas.SetActive(false);
        deniedQuest.SetActive(false);
        script1.SetActive(false);
        script2.SetActive(false);
        script3.SetActive(false);
        script4.SetActive(false);
        script5.SetActive(false);
        script6.SetActive(false);
        script1t.SetActive(false);
        script2t.SetActive(false);
        script3t.SetActive(false);
        script4t.SetActive(false);
        script5t.SetActive(false);
        script6t.SetActive(false);
        script7t.SetActive(false);
        script8t.SetActive(false);
        script9t.SetActive(false);
        script10t.SetActive(false);
        script11t.SetActive(false);
        script12t.SetActive(false);
        scripts1.SetActive(false);
        scripts2.SetActive(false);
        scripts3.SetActive(false);
        scripts4.SetActive(false);
        acceptQuest.SetActive(false);
        questHint.SetActive(false);
        TalkToFisherman2.SetActive(false);  
        FisherManPanel2.SetActive(false);
        TalkToFisherman3.SetActive(false);  
        FisherManPanel3.SetActive(false);
        DeniedQuest3.SetActive(false);
        accepttheQuest.SetActive(false);
        QuestObjectiveLapulapu.SetActive(false);
        QuestGuideLapulapu.SetActive(false);
        QuestGuide2.SetActive(false);
        currentQuest = FindObjectOfType<CurrentQuest>();
        movementControl = FindObjectOfType<movementControl>();
        checkpoint.SetActive(false);
        checkpoint2.SetActive(false);


        if (NPCInteraction.isScene2toGame) 
        {
            showTalkToFisherman3 = true;
            isInQuest1 = false;
            isInQuest2 = true;
            isInQuest3 = false;
        }
        if (NPCInteraction.isQuestGuide2) 
        {
            QuestGuide2.SetActive(true);
        }
        if (NPCInteraction.isScene3toGame)
        {
            isInQuest1 = false;
            isInQuest2 = false;
            isInQuest3 = true;
            fight = true;
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = new Vector3(720.2798f, 21.47f, 442.934f);
        }

        //buttons
        yesButton.onClick.AddListener(OnYesButtonClicked);
        noButton.onClick.AddListener(OnNoButtonClicked);
        yesButton2.onClick.AddListener(OnYesButton2Clicked);
        noButton2.onClick.AddListener(OnNoButton2Clicked);
        nextButton1.onClick.AddListener(OnNextButtonClicked);
        nextbutton2.onClick.AddListener(OnNextButton2Clicked);
        declinebutton.onClick.AddListener(OnDeclineButtonClicked);
        acceptbutton.onClick.AddListener(OnAcceptButtonClicked);
        continueBtn.onClick.AddListener(OnContinueButtonClicked);
      
        //quest2 buttons;
        nextButton3.onClick.AddListener(OnNextButton3Clicked);
        noButton3.onClick.AddListener(OnNoButton3Clicked);
        yesButton3.onClick.AddListener(OnYesButton3Clicked);
        Acceptbtn1.onClick.AddListener(OnAcceptButton1Clicked);
        Declinebtn1.onClick.AddListener(OnDeclineButton1Clicked);
        
    
    }

    public void StartQuest1()
    {
        isInQuest1 = true;
        isInQuest2 = false;
        isInQuest3 = false;
    }

    public void StartQuest2()
    {
        isInQuest1 = false;
        isInQuest2 = true;
        isInQuest3 = false;
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
            // Check if showTalkToFisherman3 is true first
            if (showTalkToFisherman3)
            {
                TalkToFisherman3.SetActive(true); // Show TalkToFisherman3 if re-entering after Continue
            }
            else if (questAccepted && !NPCinteractionAndres.andresQuestCompleted)
            {
                TalktoFisherman.SetActive(true);
            }
            else if (!questAccepted && !NPCinteractionAndres.andresQuestCompleted)
            {
                questHint.SetActive(true);
            }
            else if (NPCinteractionAndres.andresQuestCompleted && questAccepted)
            {
                questHint.SetActive(false);
                TalkToFisherman2.SetActive(true);
                continueBtn.gameObject.SetActive(false);
            }
            else if (!questAccepted && !NPCinteractionAndres.andresQuestCompleted)
            {
                questHint.SetActive(true);
            }
            else if (!questaccepted3 && !NPCinteractionAndres.andresQuestCompleted)
            {
                TalkToFisherman3.SetActive(false);
                Debug.Log("");

            }
        

            

            playerInRange = true;
        }
    }



    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TalktoFisherman.SetActive(false);
            questCanvas.SetActive(false);
            TalkToFisherman2.SetActive(false);
            TalkToFisherman3.SetActive(false);
            script1.SetActive(false);
            script2.SetActive(false); // Deactivate the player reply text
            script3.SetActive(false); // Deactivate script3
            script4.SetActive(false);
            script4.SetActive(false);
            script5.SetActive(false);
            script6.SetActive(false);
            questHint.SetActive(false);
            playerInRange = false;
        }
    }

    IEnumerator HideWarningAfterDelay(float wait)
    {
        yield return new WaitForSeconds(wait); // Wait for the specified time
        deniedQuest.SetActive(false); // Hide the warning
        DeniedQuest3.SetActive(false);
    }
    
    IEnumerator acceptedQuestDelay(float wait)
    {
        if (isInQuest2)
        {
            checkpoint2.SetActive(true);
        }
        yield return new WaitForSeconds(wait); // Wait for the specified time
        if(isInQuest2)
        {
            checkpoint2.SetActive(false);
            QuestObjectiveLapulapu.SetActive(true);
            QuestGuideLapulapu.SetActive(true);
        }
        acceptQuest.SetActive(false); // Hide the warning
        accepttheQuest.SetActive(false);
        currentQuest.displayObjectives();
        
    }
    private void OnYesButtonClicked()
    {   
        movementControl.DisableMovement();
        StartQuest1();
        checkpoint.SetActive(true);
        TalktoFisherman.SetActive(false);  // Close the NPC interaction canvas
        questCanvas.SetActive(true); // Show the quest canvas
        script1.SetActive(true);
        nextButton1.gameObject.SetActive(true);
        acceptbutton.gameObject.SetActive(false);
        declinebutton.gameObject.SetActive(false);

        QuestGuide.SetActive(false);       // Disable QuestGuide
        
    }
    
    private void OnNoButtonClicked()
    {   
        TalktoFisherman.SetActive(false);  // Close the NPC interaction canvas
        deniedQuest.SetActive(true);
        StartCoroutine(HideWarningAfterDelay(2f));
        acceptbutton.gameObject.SetActive(false);
        declinebutton.gameObject.SetActive(false);
    }

    private void OnYesButton2Clicked()
    {   
        movementControl.DisableMovement();
        StartQuest2();
        QuestGuide2.SetActive(false);
        FisherManPanel2.SetActive(true);
        TalkToFisherman2.SetActive(false);
        script1t.SetActive(true);
        AndresQuestCompleted.SetActive(false);
        AndresQuestGuide.SetActive(false);
    }
    
    private void OnNoButton2Clicked()
    {   
        TalkToFisherman2.SetActive(false);
        deniedQuest.SetActive(true);
        StartCoroutine(HideWarningAfterDelay(2f));
    }

    private void OnNextButtonClicked()
    {
        // Check if script1 is currently active
        if (script1.activeSelf)//script1
        {
            // Deactivate script1 and show playerreply1
            script1.SetActive(false);
            script2.SetActive(true);
        }
        else if (script2.activeSelf)//script2
        {
            // Deactivate playerreply1 and show script3
            script2.SetActive(false);//script2
            script3.SetActive(true);
        }
        else if (script3.activeSelf)
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
            script6.SetActive(true);
            nextButton1.gameObject.SetActive(false);
            acceptbutton.gameObject.SetActive(true);
            declinebutton.gameObject.SetActive(true);
            
        }
    }
    private void OnNextButton2Clicked(){
        if (script1t.activeSelf)
        {
            script1t.SetActive(false);
            script2t.SetActive(true);
        }
        else if (script2t.activeSelf)
        {
            script2t.SetActive(false);
            script3t.SetActive(true);
        }
        else if (script3t.activeSelf)
        {
            script3t.SetActive(false);
            script4t.SetActive(true);
        }
        else if(script4t.activeSelf)
        {
            script4t.SetActive(false);
            script5t.SetActive(true);
        }
        else if(script5t.activeSelf)
        {
            script5t.SetActive(false);
            script6t.SetActive(true);
        }
        else if(script6t.activeSelf)
        {
            script6t.SetActive(false);
            script7t.SetActive(true);
        }
        else if(script7t.activeSelf)
        {
            script7t.SetActive(false);
            script8t.SetActive(true);
        }
        else if(script8t.activeSelf)
        {
            script8t.SetActive(false);
            script9t.SetActive(true);
        }
        else if(script9t.activeSelf)
        {
            script9t.SetActive(false);
            script10t.SetActive(true);
        }
        else if(script10t.activeSelf)
        {
            script10t.SetActive(false);
            script11t.SetActive(true);
        }
        else if(script11t.activeSelf)
        {
            script11t.SetActive(false);
            script12t.SetActive(true);
            nextbutton2.gameObject.SetActive(false);
            continueBtn.gameObject.SetActive(true);
        }
    }
    private void OnDeclineButtonClicked(){
        questCanvas.SetActive(false);
        deniedQuest.SetActive(true);
        StartCoroutine(HideWarningAfterDelay(2f));
        movementControl.EnableMovement();
    }

    private void OnContinueButtonClicked()
    {
        FisherManPanel2.SetActive(false);
        movementControl.EnableMovement();
        QuestMarker.SetActive(false);
        SaveGameState();
        StartCoroutine(DelayedSceneSwitch());
        
    }

    private void OnYesButton3Clicked(){
        movementControl.DisableMovement();
        FisherManPanel3.SetActive(true);
        TalkToFisherman3.SetActive(false);
        scripts1.SetActive(true);
        Declinebtn1.gameObject.SetActive(false);
        Acceptbtn1.gameObject.SetActive(false); 

    }
    
    private void OnNoButton3Clicked(){
        TalkToFisherman3.SetActive(false);  
        DeniedQuest3.SetActive(true);
        StartCoroutine(HideWarningAfterDelay(2f));
    }

    private void OnNextButton3Clicked(){
        if(scripts1.activeSelf){
            scripts1.SetActive(false);  
            scripts2.SetActive(true);
        }
        else if(scripts2.activeSelf){
            scripts2.SetActive(false);
            scripts3.SetActive(true);
        }
        else if(scripts3.activeSelf){
            scripts3.SetActive(false);
            scripts4.SetActive(true);

            nextButton3.gameObject.SetActive(false);
            Acceptbtn1.gameObject.SetActive(true);
            Declinebtn1.gameObject.SetActive(true);
        }
        
    }

    private void OnAcceptButton1Clicked(){
        FisherManPanel3.SetActive(false);
        QuestMarker1.SetActive(false);
        movementControl.EnableMovement();
        questaccepted3 = false;
        TalkToFisherman3.SetActive(false);
        FishermanQuestCompleted = true;
        accepttheQuest.SetActive(true);
        QuestGuide2.SetActive(false);
        StartCoroutine(acceptedQuestDelay(2f));
    }

    private void OnDeclineButton1Clicked(){
        movementControl.EnableMovement();
        TalkToFisherman3.SetActive(false);
        DeniedQuest3.SetActive(true);
        StartCoroutine(HideWarningAfterDelay(2f));
    }
    private IEnumerator DelayedSceneSwitch()
    {
        transition.SetTrigger("Start");

        // Wait for the duration of the animation (in seconds)
        yield return new WaitForSeconds(1f);  // Change 5f to the duration of your animation

        // After delay, switch to the next scene
        SceneManager.LoadScene("cutscene2");
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

    
    private void OnAcceptButtonClicked()
    {  
        movementControl.EnableMovement();
        checkpoint.SetActive(false);
        questCanvas.SetActive(false);
        acceptQuest.SetActive(true);
        questAccepted = false;
        questFromFishermanAccepted = true; // Set this to true when the quest is accepted
        StartCoroutine(acceptedQuestDelay(2f));
        QuestMarker.SetActive(false);

        // Show Lolo's QuestMarker when the quest is accepted
        if (FishermanQuestMarker != null)
        {
            FishermanQuestMarker.SetActive(true);
        }
    }

   
}

