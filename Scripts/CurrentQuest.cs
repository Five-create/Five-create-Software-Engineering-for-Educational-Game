using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for Button

public class CurrentQuest : MonoBehaviour
{
    public GameObject Quest;


    void Start()
    {
        Quest.SetActive(false);                     
    }

    public void displayObjectives()
    {
        Quest.SetActive(true);                     // Show the Quest panel
    }


   
}
