using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugScript : MonoBehaviour
{
    public Transform player;
    public Rigidbody rb;
    public EnemyScript enemyScript;
    private Vector3 targetVec, offset;
    public int scoreYield;
    public float accel, attackTime;
    private bool cooldownActive;
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
        transform.position = new Vector3(transform.position.x, 0.4f, transform.position.z);
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
        while (true)
        {
            offset = new Vector3(Random.Range(-4, 4), 0, Random.Range(-4, 4));
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
        yield return new WaitForSeconds(2);
        StartCoroutine(Chase());
    }

    private IEnumerator Cooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(2);
        cooldownActive = false;
    }

    private void OnCollisionEnter(Collision other) {
        if(cooldownActive) return;
        if(other.gameObject.tag=="player") PlayerStats.Instance.ChangeHealth(-enemyScript.damage);
        StartCoroutine(Cooldown());
    }

    private void PursuePlayer()
    {
        Vector3 playerDir = 4 * targetVec.normalized - 0.5f * rb.velocity;
        rb.AddForce(accel * Vector3.ClampMagnitude(playerDir, 10));
    }

    private void OnEnemyDeath()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        PlayerStats.Instance.ChangeScore(scoreYield);
        KillEffect.Instance.DoSplat(transform.position,50);
        Sounds.Instance.PlaySound(Sounds.Instance.slugdeath, gameObject.transform, 1f);
    }

    private void OnDestroy()
    {
        enemyScript.enemyDeath -= OnEnemyDeath;
    }
}
