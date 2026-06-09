using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class playerNameInput : MonoBehaviour
{
    public GameObject namePanel;
    public TMP_InputField nameInputField;
    public GameObject player;
    public GameObject FadeInCanvas;
    public GameObject QuestGuide;   
    public GameObject StoryPanel;
    public Button continuebutton;
    public GameObject nullName;

    private movementControl  movementControl;
    private GameData gameData;
    void Start()
    {
        movementControl = FindObjectOfType<movementControl>();
        namePanel.SetActive(false);
        StoryPanel.SetActive(false);
        StartCoroutine(NameInputDelay(2f));
        nullName.SetActive(false);
        QuestGuide.SetActive(false);
        continuebutton.onClick.AddListener(OnContinue);
        if (movementControl != null)
        {
            movementControl.DisableMovement(); //disable movement at the start
        }

        
    }

    IEnumerator NameInputDelay(float wait)
    {
        yield return new WaitForSeconds(wait); // Wait for the specified time
        StoryPanel.SetActive(true);
        namePanel.SetActive(false);
        nameInputField.Select();
        nameInputField.ActivateInputField();
    }


    public void done()
    {
        nameInputField.DeactivateInputField();

        string playerName = nameInputField.text;

        if (player != null)
        {
            TextMeshProUGUI playerNameText = player.GetComponentInChildren<TextMeshProUGUI>();
            if (playerNameText != null)
            {
                playerNameText.text = playerName;
            }
        }

        namePanel.SetActive(false);
        QuestGuide.SetActive(true);
        nameInputField.text = "";

        if (gameData != null)
        {
            gameData.playerName = playerName;
        }

        if (movementControl != null)
        {
            movementControl.EnableMovement(); //enable movement when the player has a name
        }
    }

    IEnumerator HideWarningAfterDelay(float wait)
    {
        yield return new WaitForSeconds(wait); // Wait for the specified time
        nullName.gameObject.SetActive(false); // Hide the warning
    }

    void Update()
    {
        string playerName = nameInputField.text;

        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(playerName))
        {
            done();
        }
        if (Input.GetKeyDown(KeyCode.Return) && string.IsNullOrEmpty(playerName))
        {
            nullName.SetActive(true);
            StartCoroutine(HideWarningAfterDelay(2f));
            nameInputField.Select();
            nameInputField.ActivateInputField();
        }
    }
    private void OnContinue(){
        namePanel.SetActive(true);
        StoryPanel.SetActive(false);
    }
}
