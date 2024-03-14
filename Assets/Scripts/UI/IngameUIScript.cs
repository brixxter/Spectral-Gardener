using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameUIScript : MonoBehaviour
{
    public GameObject gameOverPanel;
    void Start()
    {
        PlayerStats.playerDeath += OnPlayerDeath;
    }

    void OnPlayerDeath()
    {
        Cursor.lockState = CursorLockMode.None;
        Camera.main.GetComponent<CameraController>().enabled = false;
        gameOverPanel.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

    private void OnDestroy() {
        PlayerStats.playerDeath -= OnPlayerDeath;
    }
}
