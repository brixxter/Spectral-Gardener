using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyProjectileScript : MonoBehaviour
{
    public float scale=30;
    public int damage;
    public GameObject explosionObject;

    private void Update() {
        if(WorldScript.OutOufBounds(gameObject.transform.position)) 
        {
            Debug.Log("Projectile out of bounds, deleting...");
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter(Collision other) {
        ExplosionHandler.SpawnExplosion(other.contacts[0].point, scale, damage, explosionObject, false, true);
        Destroy(gameObject);
    }
}
