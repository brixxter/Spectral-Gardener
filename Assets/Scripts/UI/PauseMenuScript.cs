using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenu, hud;
    public static PauseMenuScript Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ClosePauseMenu();
                Resume();
            }
            else
            {
                OpenPauseMenu();
                Pause();
            }
        }
    }



    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Camera.main.gameObject.GetComponent<CameraController>().enabled = false;
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Camera.main.gameObject.GetComponent<CameraController>().enabled = true;
        Time.timeScale = 1;
        isPaused = false;
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        hud.SetActive(true);
    }

    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        hud.SetActive(false);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Time.timeScale = 1;
        isPaused = false;
        hud.SetActive(true);
    }
}
