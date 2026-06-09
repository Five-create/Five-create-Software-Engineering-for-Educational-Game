using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class saveAndLoadGame : MonoBehaviour
{
    public GameObject player; // Assign your player GameObject here
    public List<GameObject> panels; // Assign each panel you want to save in the Inspector

    private string saveFilePath;

    private void Start()
    {
        // Set the file path to where your save file is located
        saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
        LoadGame(); // Automatically load the game when the script starts
    }

    // Method to save the game state
    public void SaveGame()
    {
        GameData gameData = new GameData();

        // Check if the player is not in quest 3 before saving position
        if (!NPCInteraction.isInQuest3)
        {
            gameData.playerPosition = player.transform.position;
        }

        // Save the quest statuses
        gameData.isInQuest1 = NPCInteraction.isInQuest1;
        gameData.isInQuest2 = NPCInteraction.isInQuest2;
        gameData.isInQuest3 = NPCInteraction.isInQuest3;

        // Convert the game data to JSON
        string json = JsonUtility.ToJson(gameData);
        
        // Write the JSON string to the file
        File.WriteAllText(saveFilePath, json);

        Debug.Log("Game saved at: " + saveFilePath);
    }

    // Method to load the game state
    public void LoadGame()
    {
        // Check if the save file exists
        if (File.Exists(saveFilePath))
        {
            // Read the content of the save file
            string json = File.ReadAllText(saveFilePath);

            // Ensure the JSON is valid and not empty
            if (!string.IsNullOrEmpty(json))
            {
                // Deserialize the JSON string into GameData
                GameData gameData = JsonUtility.FromJson<GameData>(json);

                // Restore the player's position
                player.transform.position = gameData.playerPosition;

                // Restore the quest states
                NPCInteraction.isInQuest1 = gameData.isInQuest1;
                NPCInteraction.isInQuest2 = gameData.isInQuest2;
                NPCInteraction.isInQuest3 = gameData.isInQuest3;

                // Handle quest transitions (this should be based on saved quest data)
                if (gameData.isInQuest1 || gameData.isInQuest2)
                {
                    cutScene2toGame sceneTransition = FindObjectOfType<cutScene2toGame>();
                    if (sceneTransition != null)
                    {
                        sceneTransition.GoToPlay();
                    }
                }
                else if (gameData.isInQuest3)
                {
                    cutScene3toGame sceneTransition = FindObjectOfType<cutScene3toGame>();
                    if (sceneTransition != null)
                    {
                        sceneTransition.GoToPlay();
                    }
                }
            }
            else
            {
                Debug.LogError("Error: The saved game data is empty or corrupted.");
            }
        }
        else
        {
            Debug.LogError("Error: No saved game data file found.");
        }
    }
}