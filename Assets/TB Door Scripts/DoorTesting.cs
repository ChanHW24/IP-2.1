using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTesting : InteractableTesting
{
    [SerializeField] private AudioSource openAudio;

    public float openDuration;
    private float currentDuration;
    private bool opening = false;

    private Vector3 startRotation;
    private Vector3 targetRotation;

    /// <summary>
    /// Flags if the door is open
    /// </summary>
    private bool opened = false;

    /// <summary>
    /// Flags if the door is locked
    /// </summary>
    private bool locked = false;

    private void Update()
    {
        if (opening)
        {
            currentDuration += Time.deltaTime;
            // Calculate the interpolation factor
            float t = currentDuration / openDuration;
            // Lerp to the target rotation
            transform.eulerAngles = Vector3.Lerp(startRotation, targetRotation, t);

            // Check if the door has fully opened
            if (currentDuration >= openDuration)
            {
                // Reset values
                currentDuration = 0f;
                opening = false;
                transform.eulerAngles = targetRotation;
                opened = true; // Set opened to true
            }
        }
    }

    /// <summary>
    /// Handles the door opening 
    /// </summary>
    public void OpenDoor()
    {
        // Door should open only when it is not locked and not already opened.
        if (!locked && !opened && !opening)
        {
            startRotation = transform.eulerAngles;
            targetRotation = startRotation;
            targetRotation.y += 90f; // Set target rotation to 90 degrees

            // Start the opening process
            opening = true;
            openAudio.Play();
        }
    }

    /// <summary>
    /// Handles closing the door
    /// </summary>
    public void CloseDoor()
    {
        // Reset the opened state to allow reopening
        if (opened)
        {
            startRotation = transform.eulerAngles;
            targetRotation = startRotation;
            targetRotation.y -= 90f; // Set target rotation to 0 degrees

            // Start the closing process
            opening = true;
            opened = false; // Set opened to false
            openAudio.Play();
        }
    }

    /// <summary>
    /// Sets the lock status of the door.
    /// </summary>
    /// <param name="lockStatus">The lock status of the door</param>
    public void SetLock(bool lockStatus)
    {
        locked = lockStatus;
    }

    /// <summary>
    /// Handles the door's interaction
    /// </summary>
    /// <param name="thePlayer">The player that interacted with the door</param>
    public override void Interact(PlayerTesting thePlayer)
    {
        // Call the Interact function from the base Interactable class.
        base.Interact(thePlayer);


        if (!locked)
        {
            // Call the OpenDoor() function
            OpenDoor();
        }

        else
        {
            Debug.Log("The door is locked");
        }
    }
}