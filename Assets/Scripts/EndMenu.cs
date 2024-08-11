using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public AudioSource endSound;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true; // Show cursor
        Cursor.lockState = CursorLockMode.None; // Unlock cursor
        endSound.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MainMenu()
    {
        Debug.Log("Menu");
        SceneManager.LoadScene("MainMenu");
    }
}
