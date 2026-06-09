using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  // Singleton instance
    public Vector3 playerPosition;      // Store player's position
    public GameObject player;           // Store the player GameObject

    private void Awake()
    {
        // Ensure only one instance of GameManager persists across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure GameManager persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManager
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            // If the player reference is not assigned, find the player in the scene
            if (player == null)
            {
                player = GameObject.FindWithTag("Player");  // Find player by tag
            }

            // When the Game Scene loads, move the player to the saved position
            if (player != null && playerPosition != null)
            {
                player.transform.position = playerPosition;
            }
        }
    }

    public void SetPlayerPosition(Vector3 position)
    {
        playerPosition = position;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerPosition;
    }
}
