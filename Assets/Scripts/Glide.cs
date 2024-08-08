using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Glide : MonoBehaviour
{
    // Text after picking up glider
    public GameObject gliderActivationText;  // The GameObject to activate
    public float duration = 10f;         // Duration before deactivating
    private bool isActivated = false;    // To check if text is activated

    public GameObject pickUpText; // show pickup message
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
                pickUpText.SetActive(true);
                isPlayerInTrigger = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickUpText.SetActive(false);
            isPlayerInTrigger = false;
        }
    }

    private void ActivateGlide()
    {
        if (targetPlayer != null)
        {
            targetPlayer.EnableGlide();
            pickUpText.SetActive(false);
            gliderActivationText.SetActive(true);
            StartCoroutine(OffGliderActivationText());
            Destroy(gameObject);
        }
    }

    private IEnumerator OffGliderActivationText()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Deactivate the GameObject
        gliderActivationText.SetActive(false);
    }
}