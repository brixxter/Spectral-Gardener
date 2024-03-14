using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    public GameObject explosion;
    public static void SpawnExplosion(Vector3 pos, float scale, int damage, GameObject explosion, bool friendlyFire, bool enemyFire)
    {
        var explosionInstance = Instantiate(explosion);
        explosionInstance.transform.position = pos;
        explosionInstance.transform.localScale = new Vector3(scale,scale,scale);
        var explosionScript = explosionInstance.GetComponent<ExplosionScript>();
        explosionScript.damage = damage;
        explosionScript.friendlyFire = friendlyFire;
        explosionScript.enemyFire = enemyFire;
      
    }
}
