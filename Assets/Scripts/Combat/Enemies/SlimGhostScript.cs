using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimGhostScript : MonoBehaviour
{
    public Transform player;
    public Rigidbody rb;
    public EnemyScript enemyScript;
    public Vector3 targetVec, offset;
    public GameObject bullet;
    public int scoreYield = 75;
    public float accel, attackTime, bulletSpeed;
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
        transform.position = new Vector3(transform.position.x, 0.9f, transform.position.z);
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

    private IEnumerator Shoot()
    {
        for(int i= 0; i<4; i++)
        {
            Vector3 heading = player.transform.position -transform.position-3*Vector3.up;
            yield return new WaitForSeconds(0.3f);
            var laser = Instantiate(bullet);
            laser.transform.position = transform.position+3*Vector3.up;
            laser.GetComponent<EnemyLaserScript>().damage = enemyScript.damage;
            laser.GetComponent<Rigidbody>().velocity = heading.normalized*bulletSpeed;
            Sounds.Instance.PlaySound(Sounds.Instance.simpleattack, gameObject.transform, 0.2f);
        }
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
        StartCoroutine(Shoot());
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
        Sounds.Instance.PlaySound(Sounds.Instance.slimdeath, gameObject.transform, 1f);
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        PlayerStats.Instance.ChangeScore(scoreYield);
        KillEffect.Instance.DoSplat(transform.position,50);
    }

    private void OnDestroy()
    {
        enemyScript.enemyDeath -= OnEnemyDeath;
    }
}
