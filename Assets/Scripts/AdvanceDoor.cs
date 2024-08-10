/*
 * Author: Chan Hong Wei
 * Date: 29/06/2024
 * Description: 
 * The door that opens when the player is near it and presses the interact button.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceDoor : MonoBehaviour
{
    [SerializeField]
    public AudioSource openAudio;
    public GameObject openUpText, lockedText;

    // for door lerp time
    public float openDuration;
    protected float currentDuration;
    protected bool opening = false;
    protected Vector3 startRotation;
    protected Vector3 targetRotation;

    private bool playerInTrigger = false; // Flag to track if the player is in the trigger

    /// <summary>
    /// Flags if the door is open
    /// </summary>
    protected bool opened = false;

    /// <summary>
    /// Flags if the door is locked
    /// </summary>
    protected bool locked = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the obejct entering the trigger has the "Player" tag
        if (other.gameObject.tag == "Player")
        {
            if (!opened)
            {
                openUpText.SetActive(true);
            }
            playerInTrigger = true; // Set the flag when the player enters the trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the obejct exiting the trigger has the "Player" tag
        if (other.gameObject.tag == "Player")
        {
            openUpText.SetActive(false); //Remove pick up text
            lockedText.SetActive(false); // Hide the locked text when the player leaves
            playerInTrigger = false; // Clear the flag when the player enters the trigger
        }
    }

    /// <summary>
    /// Handles the door opening 
    /// </summary>
    public virtual void OpenDoor()
    {
        // Door should open only when it is not locked
        // and not already opened.
        if (!locked && !opened)
        {
            // Cannot directly modify the transform rotation.
            // transform.eulerAngles.y += 90f;

            startRotation = transform.eulerAngles;

            targetRotation = startRotation;

            // Add 90 degrees to the y axis rotation
            targetRotation.y += 90f;

            // Set the opened bool to true
            opening = true;
        }
        else if (locked)
        {
            openUpText.SetActive(false);
            lockedText.SetActive(true);
        }
    }

    /// <summary>
    /// Sets the lock status of the door.
    /// </summary>
    /// <param name="lockStatus">The lock status of the door</param>
    public void SetLock(bool lockStatus)
    {
        // Assign the lockStatus value to the locked bool.
        locked = lockStatus;
    }

    private void Start()
    {
        openUpText.SetActive(false);
        lockedText.SetActive(false);
    }

    private void Update()
    {
        if (opening)
        {
            currentDuration += Time.deltaTime;
            float t = currentDuration / openDuration;
            transform.eulerAngles = Vector3.Lerp(startRotation, targetRotation, t);

            if (currentDuration >= openDuration)
            {
                currentDuration = 0f;
                opening = false;
                transform.eulerAngles = targetRotation;
                opened = true;
                openUpText.SetActive(false);
            }
        }

        // Check if the player is within range and presses the interact key (e.g., "E")
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // Call the OpenDoor() function
            OpenDoor();
            // Plays sound effect
            openAudio.Play();
        }
    }
}