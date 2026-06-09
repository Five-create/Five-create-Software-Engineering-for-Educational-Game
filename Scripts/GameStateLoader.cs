using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class GameStateLoader : MonoBehaviour
{
    public GameObject UIManager; // Reference to the parent GameObject for all canvases/panels
    public GameObject UIManager2;
    private movementControl movementControl;
    public GameObject victorypanel;
    private string saveFilePath;
    private cameraMovement cameraMovement;
    private void Start()
    {
        victorypanel.SetActive(false);
        movementControl = FindObjectOfType<movementControl>();
        cameraMovement = FindObjectOfType<cameraMovement>();

        saveFilePath = Application.persistentDataPath + "/savefile.json";
        
        if (NPCInteraction.isScene2toGame && UIManager != null)
        {
            LoadGameState();
            UIManager.SetActive(false);
            // Enable specific UI elements you want to keep visible

            StartCoroutine(EnableMovementAfterDelay(1f));
        }
        if (NPCInteraction.isScene3toGame && UIManager2 != null)
        {
            LoadGameState();
            UIManager.SetActive(false);
            UIManager2.SetActive(false);
            // Enable specific UI elements you want to keep visible

            StartCoroutine(EnableMovementAfterDelay(1f));
        }

        if (!NPCInteraction.fight)
        {
            // Find all GameObjects with the "MagellanEnemy" tag
            GameObject[] magellanEnemies = GameObject.FindGameObjectsWithTag("magellanEnemy");

            // Find all GameObjects with the "LapulapuEnemy" tag
            GameObject[] lapulapuEnemies = GameObject.FindGameObjectsWithTag("lapulapuEnemy");

            // Destroy all MagellanEnemy NPCs
            foreach (GameObject npc in magellanEnemies)
            {
                Destroy(npc);
            }

            // Destroy all LapulapuEnemy NPCs
            foreach (GameObject npc in lapulapuEnemies)
            {
                Destroy(npc);
            }
        }

    }

    private void LoadGameState()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            if (!string.IsNullOrEmpty(json))
            {
                GameData gameData = JsonUtility.FromJson<GameData>(json);
                GameObject player = GameObject.FindWithTag("Player");

                if (player != null)
                {
                    player.transform.position = gameData.playerPosition; // Set the position
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
    private IEnumerator EnableMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        movementControl.EnableMovement();
    }

    private void Update()
    {
        // Check if there are any remaining MagellanEnemy NPCs in the scene
        GameObject[] magellanEnemies = GameObject.FindGameObjectsWithTag("magellanEnemy");

        // If no MagellanEnemy NPCs are found, trigger the victory panel
        if (magellanEnemies.Length == 0 && NPCInteraction.isScene4)
        {
            victorypanel.SetActive(true); // Show the victory panel
            StartCoroutine(victory(5f));  // Start the victory coroutine
        }
    }

    private bool AreAllChildrenDestroyed(GameObject parentObject)
    {
        foreach (Transform child in parentObject.transform)
        {
            if (child.gameObject.activeSelf) // Check if any child is still active
            {
                return false;
            }
        }
        return true; // All children are destroyed or inactive
    }

    private IEnumerator victory(float delay)
    {
        yield return new WaitForSeconds(delay);
        movementControl.EnableMovement();
        cameraMovement.UnlockCursor();
        SceneManager.LoadScene("cutScene4");
    }
}
