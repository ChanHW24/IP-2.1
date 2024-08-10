using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    public GameObject interactText;
    private bool playerInTrigger = false; // Flag to track if the player is in the trigger

    void Update()
    {
        // Check if the player is in the trigger and the 'E' key is pressed
        if (playerInTrigger && Input.GetKeyUp(KeyCode.E))
        {
            interactText.SetActive(false); // Hide the interaction text before changing the scene
            SceneManager.LoadScene("GamePlay"); // Targeted scene
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true; // Set the flag when the player enters the trigger
            interactText.SetActive(true); // Show the interaction text
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false; // Clear the flag when the player exits the trigger
            interactText.SetActive(false); // Hide the interaction text
        }
    }
}
