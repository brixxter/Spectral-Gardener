using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarScript : MonoBehaviour
{
    public Slider healthBarSlider;
    public EnemyScript enemyScript;
    void Start()
    {
        enemyScript = gameObject.GetComponent<EnemyScript>();
        enemyScript.enemyHealthChanged += OnEnemyHealthChanged;
        enemyScript.enemyDeath += OnEnemyDeath;
        healthBarSlider.maxValue = enemyScript.health;
        OnEnemyHealthChanged();
    }

    private void OnEnemyHealthChanged()
    {
        healthBarSlider.value = enemyScript.health;
    }

    private void OnEnemyDeath()
    {
        Destroy(gameObject);
    }

    private void OnDestroy() {
        enemyScript.enemyHealthChanged -= OnEnemyHealthChanged;
        enemyScript.enemyDeath -= OnEnemyDeath;
    }
}
