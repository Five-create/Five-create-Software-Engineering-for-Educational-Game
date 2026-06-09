using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LoadGame : MonoBehaviour
{
    public Animator transition;
    private string saveFilePath;
    private GameObject player;

    private void Start()
    {
        // Set the file path to where your save file is located
        saveFilePath = Application.persistentDataPath + "/savefile.json";
    }

    public void OnLoadGameButtonClicked()
    {
        StartCoroutine(DelayedSceneSwitch());
    }

    private IEnumerator DelayedSceneSwitch()
    {
        transition.SetTrigger("Start");

        // Wait for the transition animation to finish
        yield return new WaitForSeconds(1f);

        // Check if the save file exists
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);

            if (!string.IsNullOrEmpty(json))
            {
                // Deserialize the saved data
                GameData gameData = JsonUtility.FromJson<GameData>(json);

                // Check if the GameManager instance is valid
                if (GameManager.Instance != null)
                {
                    // Apply the saved quest data (if applicable)
                    NPCInteraction.isInQuest1 = gameData.isInQuest1;
                    NPCInteraction.isInQuest2 = gameData.isInQuest2;
                    NPCInteraction.isInQuest3 = gameData.isInQuest3;

                    // Store the player's saved position in the GameManager
                    GameManager.Instance.SetPlayerPosition(gameData.playerPosition);

                    // Load the Game scene
                    SceneManager.LoadScene("Game");
                }
            }
            else
            {
                Debug.Log("Saved game data is empty or corrupted!");
            }
        }
        else
        {
            Debug.Log("No saved game file found at: " + saveFilePath);
        }
    }
}
