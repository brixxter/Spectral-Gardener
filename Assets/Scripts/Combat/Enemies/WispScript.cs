using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispScript : MonoBehaviour
{
    public Transform player;
    public Rigidbody rb;
    public EnemyScript enemyScript;
    public float accel;
    void Start()
    {
        player = PlayerStats.Instance.player;
        enemyScript.enemyDeath += OnEnemyDeath;   
    }

    void Update()
    {
        Vector3 playerDir = EnemyScript.FindTargetHeading(player.position, transform.position);
        rb.AddForce(accel * Vector3.ClampMagnitude(playerDir, 15));
    }

    private void OnEnemyDeath()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        //ParticleSystem ps = gameObject.GetComponent<ParticleSystem>();
        //ParticleSystem.EmissionModule emission = ps.emission;
        
        //emission.rateOverTime = 0;
        //ps.Emit(20);
    }

    private void OnDestroy() {
        enemyScript.enemyDeath -= OnEnemyDeath; 
    }
}
