/*
 * Author: Chan Hong Wei
 * Date: 29/06/2024
 * Description: 
 * The script for the key to the advance door
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour
{
    /// <summary>
    /// The door that this key card unlocks
    /// </summary>
    public List<AdvanceDoor> linkedDoors; // List of doors that the keycard can unlock

    public GameObject pickUpText;

    private bool playerInTrigger = false; // Flag to track if the player is in the trigger

    private void Start()
    {
        foreach (AdvanceDoor door in linkedDoors)
        {
            door.SetLock(true);
        }
    }

    private void Update()
    {
        // Check if the player is within range and presses the interact key (e.g., "E")
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Collect();
        }
    }

    public void Collect()
    {
        // Tell the door to unlock itself.
        pickUpText.SetActive(false); // Remove the pick-up text
        foreach (AdvanceDoor door in linkedDoors)
        {
            door.SetLock(false); // Unlock each doors
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the Player is entering the trigger zone
        if (other.CompareTag("Player"))
        {
            pickUpText.SetActive(true); // Show the pick-up text
            playerInTrigger = true; // Set the flag when the player enters the trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the Player is exiting the trigger zone
        if (other.CompareTag("Player"))
        {
            pickUpText.SetActive(false); // Hide the pick-up text
            playerInTrigger = false; // Clear the flag when the player enters the trigger
        }
    }
}

