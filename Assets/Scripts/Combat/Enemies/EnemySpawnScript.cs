using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    public static EnemySpawnScript Instance;
    public Transform[] spawnPoints;
    public GameObject[] enemies;
    private Coroutine enemySpawning;

    public int activeEnemies, enemiesSpawnedTotal, enemySpawnLimit, difficulty;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        enemies[0].GetComponent<EnemyScript>().damage = 30;
        enemies[0].GetComponent<EnemyScript>().health = 80;

        enemies[1].GetComponent<EnemyScript>().damage = 12;
        enemies[1].GetComponent<EnemyScript>().health = 120;

        enemies[2].GetComponent<EnemyScript>().damage = 40;
        enemies[2].GetComponent<EnemyScript>().health = 250;

        DayTimeScript.nightEvent += OnNightTimeStart;
        DayTimeScript.dayEvent += OnDayTimeStart;
    }

    void OnNightTimeStart()
    {
        difficulty++;
        for(int i= 0; i<enemies.Length; i++)
        {
            var enemyscript = enemies[i].GetComponent<EnemyScript>();
            enemyscript.health += (int)(enemyscript.health*0.1f);
            enemyscript.damage += (int)(enemyscript.damage*0.1f);
        }
        enemySpawnLimit = 4*difficulty;
        enemySpawning = StartCoroutine(EnemySpawning());
    }

    void OnDayTimeStart()
    {
        enemiesSpawnedTotal = 0;
        if (enemySpawning != null) StopCoroutine(enemySpawning);
    }

    IEnumerator EnemySpawning()
    {
        while(!EnemyLimitReached())
        {
            yield return new WaitForSeconds(5f);
            SpawnEnemy(difficulty);
        }
        Debug.Log("No more enemies available");
    }

    void SpawnEnemy(int difficulty)
    {
        GameObject enemy;
        AudioSource audioSource;

        enemiesSpawnedTotal++;

        Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length-1)];
        int randomID = Random.Range(0, enemies.Length);
        enemy = Instantiate(enemies[randomID], randomSpawn.position, randomSpawn.localRotation);
        enemy.transform.parent = null;
        audioSource = enemy.gameObject.GetComponent<AudioSource>();
        //audioSource.clip = Sounds.Instance.enemyNoise[randomID];
        //audioSource.pitch = Random.Range(0.9f,1.1f);
        //audioSource.Play();
    }

    bool EnemyLimitReached()
    {
        return enemiesSpawnedTotal>=enemySpawnLimit;
    }

    private void OnDestroy()
    {
        DayTimeScript.nightEvent -= OnNightTimeStart;
        DayTimeScript.dayEvent -= OnDayTimeStart;
    }
}
