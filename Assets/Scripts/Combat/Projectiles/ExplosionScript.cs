using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public int damage;
    public bool friendlyFire, enemyFire;
    public string damageTag;
    private void Start() {
        StartCoroutine(ExecuteExplosion());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(enemyFire) if (other.gameObject.tag == "enemy") other.gameObject.GetComponent<EnemyScript>().SetHealth(-damage);
        if(friendlyFire) if (other.gameObject.tag == "player") PlayerStats.Instance.ChangeHealth(-damage);
       
    }

    IEnumerator ExecuteExplosion()
    {
        Sounds.Instance.PlaySound(Sounds.Instance.explosion, transform, 1f);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject.GetComponent<SphereCollider>());
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
