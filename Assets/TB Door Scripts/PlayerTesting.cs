//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;

//public class PlayerTesting : MonoBehaviour
//{
//    //SerializeField makes private variables visible in the inspector and allows access and modification to private fields indirectly
//    [SerializeField] Transform playerCamera;

//    //to determine how far the player can interact
//    [SerializeField] float interactionDistance;

//    [SerializeField] TextMeshProUGUI interactionText;

//    /// <summary>
//    /// The current Interactable of the player
//    /// </summary>
//    InteractableTesting currentInteractable;

//    /// <summary>
//    /// Store the current door in front of the player
//    /// </summary>
//    Door currentDoor;


//    private void Update()
//    {
//        Debug.DrawLine(playerCamera.position, playerCamera.position + (playerCamera.forward * interactionDistance), Color.red);
//        RaycastHit hitInfo;
//        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hitInfo, interactionDistance))
//        {
//            //print out the name of whatever my ray hit
//            //Debug.Log(hitInfo.transform.name);

//            //TryGetComponent will try to get whatever that is infront of it, it will act as a bool
//            //if it finds something, it will return true. likewise, false when nothing is found
//            //<> whatever that is inside is something you want to find
//            //requires a parameter with a output keyword so that it can save whatever that is found in TryGetComponent
//            if (hitInfo.transform.TryGetComponent<InteractableTesting>(out currentInteractable))
//            {
//                //display some interaction text
//                interactionText.gameObject.SetActive(true);
//            }
//            else
//            {
//                currentInteractable = null;
//                interactionText.gameObject.SetActive(false);
//            }
//        }
//        else
//        {
//            currentInteractable = null;
//            interactionText.gameObject.SetActive(false);
//        }
//    }
//    //public void UpdateDoor(DoorTesting newDoor)
//    //{
//    //    currentDoor = newDoor;
//    //}

//    /// <summary>
//    /// Callback function for the Interact input action
//    /// </summary>
//    void OnInteract()
//    {
//        // Check if the current Interactable is null
//        if (currentInteractable != null)
//        {
//            // Interact with the object
//            currentInteractable.Interact(this);
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTesting : MonoBehaviour
{
    //SerializeField makes private variables visible in the inspector and allows access and modification to private fields indirectly
    [SerializeField] Transform playerCamera;

    //to determine how far the player can interact
    [SerializeField] float interactionDistance;

    [SerializeField] TextMeshProUGUI interactionText;

    /// <summary>
    /// The current Interactable of the player
    /// </summary>
    InteractableTesting currentInteractable;

    /// <summary>
    /// Store the current door in front of the player
    /// </summary>
    Door currentDoor;


    private void Update()
    {
        Debug.DrawLine(playerCamera.position, playerCamera.position + (playerCamera.forward * interactionDistance), Color.red);
        RaycastHit hitInfo;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hitInfo, interactionDistance))
        {
            //print out the name of whatever my ray hit
            //Debug.Log(hitInfo.transform.name);

            //TryGetComponent will try to get whatever that is infront of it, it will act as a bool
            //if it finds something, it will return true. likewise, false when nothing is found
            //<> whatever that is inside is something you want to find
            //requires a parameter with a output keyword so that it can save whatever that is found in TryGetComponent
            if (hitInfo.transform.TryGetComponent<InteractableTesting>(out currentInteractable))
            {
                //display some interaction text
                interactionText.gameObject.SetActive(true);
            }
            else
            {
                currentInteractable = null;
                interactionText.gameObject.SetActive(false);
            }
        }
        else
        {
            currentInteractable = null;
            interactionText.gameObject.SetActive(false);
        }
    }
    //public void UpdateDoor(DoorTesting newDoor)
    //{
    //    currentDoor = newDoor;
    //}

    /// <summary>
    /// Callback function for the Interact input action
    /// </summary>
    void OnInteract()
    {
        // Check if the current Interactable is null
        if (currentInteractable != null)
        {
            // Interact with the object
            currentInteractable.Interact(this);
        }
    }
}