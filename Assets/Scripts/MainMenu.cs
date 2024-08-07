/*
 * Author: Tan Tock Beng, Chan Hong Wei, Lee Ming Zhe
 * Date: 05/08/2024
 * Description: 
 * Contains main menu button functions.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit.");
        Application.Quit();
    }
}