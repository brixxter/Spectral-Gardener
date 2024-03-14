using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SimpleGhostScript : MonoBehaviour
{
    public Transform player;
    public Rigidbody rb;
    public EnemyScript enemyScript;
    public float accel, attackTime;
    public int scoreYield=100;
    public GameObject projectile, sprinkler;
    private Vector3 targetVec, offset;

    void Start()
    {
        player = PlayerStats.Instance.player;
        enemyScript.enemyDeath += OnEnemyDeath;
        StartCoroutine(Chase());
        StartCoroutine(RandomizeOffset());
    }

    void Update()
    {
        targetVec = EnemyScript.FindTargetHeading(transform.position, player.transform.position+offset);
        transform.right = targetVec.normalized;
        transform.eulerAngles = new Vector3(-90, 0, transform.eulerAngles.z);
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    }

    private IEnumerator Chase()
    {
            targetVec = EnemyScript.FindTargetHeading(transform.position, player.transform.position);
            while (targetVec.magnitude > 6) //chasing after player
            {
                yield return new WaitForEndOfFrame();
                PursuePlayer();
            }
            StartCoroutine(LingerAndAttack(attackTime));
    }

    private IEnumerator RandomizeOffset()
    {
        while(true)
        {
        offset = new Vector3(Random.Range(-4,4),0,Random.Range(-4,4));
        yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator LingerAndAttack(float time)
    {
        float deltaTime=0;
        while(deltaTime<time)
        {
            deltaTime+=Time.deltaTime;
            rb.velocity = rb.velocity*0.97f;
            yield return new WaitForEndOfFrame();
        }
        Sounds.Instance.PlaySound(Sounds.Instance.simpleattack, gameObject.transform, 1f);
        var projectileInstance = Instantiate(projectile);
        var projectilescript = projectileInstance.GetComponent<HomingProjectileScript>();
        projectilescript.target = player;
        projectilescript.damage = enemyScript.damage;
        projectileInstance.transform.position = transform.position+Vector3.up*2;
        yield return new WaitForSeconds(2);
        StartCoroutine(Chase());
    }

    private void PursuePlayer()
    {
        Vector3 playerDir = 4*targetVec.normalized - 0.5f * rb.velocity;
        rb.AddForce(accel * Vector3.ClampMagnitude(playerDir, 10));
    }

    private void OnEnemyDeath()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        Sounds.Instance.PlaySound(Sounds.Instance.simpledeath, gameObject.transform, 1f);
        PlayerStats.Instance.ChangeScore(scoreYield);
        KillEffect.Instance.DoSplat(transform.position,50);
        float rand = Random.Range(0,100);
        if(rand<5) 
        {
            var sprinklerInstance = Instantiate(sprinkler);
            sprinklerInstance.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    private void OnDestroy()
    {
        enemyScript.enemyDeath -= OnEnemyDeath;
    }
}
