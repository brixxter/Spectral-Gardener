using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HomingProjectileScript : MonoBehaviour
{
    public Transform target;
    public float speed = 3, explosionSize = 2, lifeTime = 10, scale = 1, growthTime = 2;
    public int damage;
    public GameObject explosion;
    private bool growing = true;
    public Rigidbody rb;

    private void Start()
    {
        StartCoroutine(GrowProjectile(scale,growthTime));
    }

    private void OnCollisionEnter(Collision other)
    {
        if(growing) return;
        if (other.gameObject.tag == "enemy") return;
        if (other.gameObject.tag == "projectile") return;
        DestroyProjectile();
    }


    private void DestroyProjectile()
    {
        ExplosionHandler.SpawnExplosion(transform.position, explosionSize, damage, explosion, true, false);
        Destroy(gameObject);
    }

    private IEnumerator GrowProjectile(float size, float time)
    {
        float scale = 0;
        while (scale < size)
        {
            scale = Mathf.Clamp(scale + Time.deltaTime, 0, size);
            transform.localScale = new Vector3(100, 100, 100) * scale*0.2f;
            yield return new WaitForEndOfFrame();
        }
        growing=false;
        StartCoroutine(Expiration());
        StartCoroutine(HomeIn());
    }

    private IEnumerator HomeIn()
    {
        while (true)
        {
            Vector3 targetDir = (target.position - transform.position).normalized;
            rb.velocity = targetDir * speed;
            yield return new WaitForEndOfFrame();
        }
    }


    IEnumerator Expiration()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroyProjectile();
    }
}
