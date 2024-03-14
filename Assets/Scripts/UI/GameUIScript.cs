using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIScript : MonoBehaviour
{
    public GameObject GameOverMessage, CrossHair, HealthBar;
    void Awake()
    {
        PlayerStats.playerDeath += OnPlayerDeath;
        GameOverMessage.SetActive(false);
        CrossHair.SetActive(true);
        HealthBar.SetActive(true);
    }

    private void OnPlayerDeath()
    {
        GameOverMessage.SetActive(true);
        CrossHair.SetActive(false);
        HealthBar.SetActive(false);
    }

    private void OnDestroy() {
        PlayerStats.playerDeath -= OnPlayerDeath;
    }
}
