/*
 * Author: Chan Hong Wei
 * Date: 26/05/2024
 * Description: 
 * Contain script to programme the Pause Menu
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;

    public GameObject pauseMenuUI; // Reference to the pause menu

    public GameObject backgroundImage; // Reference to the background image

    public GameObject crossHair; // Reference to the crosshair

    public MonoBehaviour playerCameraController;

    void Start()
    {
        pauseMenuUI.SetActive(false); // Close pause menu
        backgroundImage.SetActive(false); // Close background image

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Resume game function
    public void Resume()
    {
        Debug.Log("Resume.");
        pauseMenuUI.SetActive(false); // Close pause menu
        backgroundImage.SetActive(false); // Close background image
        crossHair.SetActive(true); // Turn on crosshair
        Time.timeScale = 1f; // Resume time
        GameIsPause = false;
        Cursor.visible = false; // Hide cursor
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor
        playerCameraController.enabled = true; // Enable camera movement
    }

    // Pause game function
    public void Pause()
    {
        Debug.Log("Pause");
        pauseMenuUI.SetActive(true); // Show pause menu
        backgroundImage.SetActive(true); // Show background image
        crossHair.SetActive(false); // Turn off crosshair
        Time.timeScale = 0f; // Freeze time
        GameIsPause = true;
        Cursor.visible = true; // Show cursor
        Cursor.lockState = CursorLockMode.None; // Unlock cursor
        playerCameraController.enabled = false; // Disable camera movement
    }

    public void Restart()
    {
        Debug.Log("Restart");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Debug.Log("Menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}