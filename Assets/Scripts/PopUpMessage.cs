/*
 * Author: Chan Hong Wei
 * Date: 29/06/2024
 * Description: 
 * This script trigger the pop up messages
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopUpMessage : MonoBehaviour
{
    [SerializeField] bool destroyOnTriggerEnter;
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;

    public GameObject popUpUI;
    public GameObject backGround;

    private void Start()
    {
        popUpUI.SetActive(false);
        backGround.SetActive(false);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        onTriggerEnter.Invoke();
        if (destroyOnTriggerEnter)
        {
            Destroy(gameObject);
        }
        Pause();
    }

    private void OnTriggerExit(Collider other)
    {
        onTriggerExit.Invoke();
        Resume();
    }

    public void Resume()
    {
        popUpUI.SetActive(false);
        backGround.SetActive(false);
        Time.timeScale = 1f; // Resume time
        PauseMenu.GameIsPause = false;
        Cursor.visible = false; // Hide cursor
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor
    }

    public void Pause()
    {
        popUpUI.SetActive(true);
        backGround.SetActive(true);
        Time.timeScale = 0f; // Freeze time
        PauseMenu.GameIsPause = true;
        Cursor.visible = true; // Show cursor
        Cursor.lockState = CursorLockMode.None; // Unlock cursor
    }
}
