using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Glide : MonoBehaviour
{
    //public GameObject pickUpText; // show pickup message
    private StarterAssets.FirstPersonController targetPlayer;
    private bool isPlayerInTrigger = false;

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            ActivateGlide();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetPlayer = other.gameObject.GetComponent<StarterAssets.FirstPersonController>();
            if (targetPlayer != null)
            {
                //pickUpText.SetActive(true);
                isPlayerInTrigger = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //pickUpText.SetActive(false);
            isPlayerInTrigger = false;
        }
    }

    private void ActivateGlide()
    {
        if (targetPlayer != null)
        {
            targetPlayer.EnableGlide();
            //pickUpText.SetActive(false);
            Destroy(gameObject);
        }
    }
}