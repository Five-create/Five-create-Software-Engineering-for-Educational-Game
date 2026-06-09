using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    // Save the game state
    public void SaveGameState()
    {
        // Save player position
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
            PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
        }

        // Ensure changes are saved
        PlayerPrefs.Save();
        Debug.Log("Game Saved");
    }
}
