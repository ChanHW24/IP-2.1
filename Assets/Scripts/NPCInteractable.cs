/*
 * Author: Tan Tock Beng, Chan Hong Wei, Lee Ming Zhe
 * Date: 05/08/2024
 * Description: 
 * Contains NPC speeches functions.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCInteractable : MonoBehaviour
{

    private Animator animator;

    // Dialogue variables.
    public GameObject dialoguePanel;
    public TMP_Text nameText, dialogueText;
    public string characterName;
    public string[] dialogue;
    private int index;
    public float wordSpeed;
    public bool playerIsClose;
    public GameObject interactText, contText;

    private bool isTyping; // Track if the typing coroutine is active

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && !isTyping)
        {

            Interact(); // Talking Animation.

            if (dialoguePanel.activeInHierarchy)
            {
                NextLine();
            }
            else
            {
                dialoguePanel.SetActive(true);
                nameText.text = characterName + ":";
                StartCoroutine(Typing());
            }
        }

        if (dialogueText.text == dialogue[index])
        {
            contText.SetActive(true); // Set active the text prompt to prompt player to press E
        }
    }

    public void Interact()
    {
        Debug.Log("Interact!");

        animator.SetTrigger("Talk");
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        isTyping = true; // Start typing
        dialogueText.text = ""; // Clear text before typing

        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

        isTyping = false; // Typing is complete
    }

    public void NextLine()
    {

        contText.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            //dialogueText.text = ""; // Clear text before typing
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.SetActive(true);
            playerIsClose = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.SetActive(false);
            playerIsClose = false;
            zeroText();
        }
    }
}
