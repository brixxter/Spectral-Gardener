using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserScript : MonoBehaviour
{
    public int damage=20;

    private void Update() {
        if(WorldScript.OutOufBounds(gameObject.transform.position)) 
        {
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag=="player") PlayerStats.Instance.ChangeHealth(-damage);
        Destroy(gameObject);
    }
}
