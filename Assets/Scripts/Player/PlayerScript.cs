using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Coroutine healthRegen;
    void Start()
    {
        PlayerStats.playerDeath += OnPlayerDeath;
        healthRegen = StartCoroutine(HealthRegen());
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "basicEnemyBullet") other.GetComponent<EnemyScript>();
    }

    void OnPlayerDeath()
    {
        StopCoroutine(healthRegen);
    }

    private IEnumerator HealthRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(PlayerStats.Instance.regenSpeed);

            if (PlayerStats.Instance.currentBlossomDuration > 0)
            {
                PlayerStats.Instance.currentBlossomDuration--;
                PlayerStats.Instance.ChangeHealth(PlayerStats.Instance.regenAmount);
            }
            else
            {
                if (PlayerStats.Instance.cropAmount[2] > 0)
                {
                    PlayerStats.Instance.cropAmount[2]--;
                    PlayerStats.Instance.currentBlossomDuration = PlayerStats.Instance.blossomDuration;
                }
            }
        }
    }

    private void OnDestroy()
    {
        PlayerStats.playerDeath -= OnPlayerDeath;
    }


}
