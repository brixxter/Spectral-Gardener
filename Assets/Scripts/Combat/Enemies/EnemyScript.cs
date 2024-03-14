using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public event Action enemyDeath, enemyHealthChanged;
    public int health, enemyID, damage;
    public bool alive = true;
    private bool inBounds=true;


    void Start()
    {  
        EnemySpawnScript.Instance.activeEnemies++;
        gameObject.transform.forward = Vector3.up;
        StartCoroutine(InBoundProcess());
    }

    private void Update() {
        if(WorldScript.OutOufBounds(transform.position)) inBounds = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (alive)
        {
            if (other.tag == "water") SetHealth(-1);
        }
    }

    public void SetHealth(int x)
    {
        if (alive)
        {
            Sounds.Instance.PlaySound(Sounds.Instance.slughit, gameObject.transform, 0.2f);
            if(enemyHealthChanged!=null) enemyHealthChanged();
            health += x;
            if (x < 0) 
            {
            PlayerStats.Instance.damageDealt -= x;
            if(enemyHealthChanged!=null) enemyHealthChanged();
            }

            DeathCheck();
        }
    }

    private void DeathCheck()
    {
        if (health <= 0 || !inBounds)
        {
            alive = false;
            PlayerStats.Instance.killCount++;
            enemyDeath();
            StartCoroutine(DestroyEnemy());
        }
    }

    public static Vector3 FindTargetHeading(Vector3 pos, Vector3 targetPos)
    {
        Vector3 dir = targetPos-pos;
        return new Vector3(dir.x,0,dir.z);
    }

    IEnumerator DestroyEnemy()
    {
        EnemySpawnScript.Instance.activeEnemies--;
        gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    IEnumerator InBoundProcess() //not happy with this but I couldn't come up with something better this quickly to prevent fallen off enemies from clogging up the spawn cap
    {
        while(inBounds)
        {
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Enemy out of bounds, deleting");
        StartCoroutine(DestroyEnemy());
    }
}
