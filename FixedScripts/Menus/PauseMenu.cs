using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseMenuUI;
    public string txtMenuScene;
    //public AudioManager sound;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        //Hard coded, fix later
        //sound.PlaySound("SchoolMusic");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        //Hard coded, fix later
        //sound.PauseSound("SchoolMusic");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading main menu");
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        isPaused = false;
        SceneManager.LoadScene(txtMenuScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
