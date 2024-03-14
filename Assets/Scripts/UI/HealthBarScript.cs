using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider healthBarSlider;
    void Start()
    {
        PlayerStats.playerHealthChanged += OnPlayerHealthChanged;
        PlayerStats.playerMaxHealthChanged += OnPlayerMaxHealthChanged;
        OnPlayerHealthChanged();
        OnPlayerMaxHealthChanged();
    }

    private void OnPlayerHealthChanged()
    {
        healthBarSlider.value = PlayerStats.Instance.health;
    }

    private void OnPlayerMaxHealthChanged()
    {
        healthBarSlider.maxValue = PlayerStats.Instance.maxHealth;
    }

    private void OnDestroy() {
        PlayerStats.playerHealthChanged -= OnPlayerHealthChanged;
        PlayerStats.playerMaxHealthChanged -= OnPlayerMaxHealthChanged;
    }
}
