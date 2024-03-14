using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanProjectileScript : MonoBehaviour
{
    public int damage=20;

    private void Update() {
        if(WorldScript.OutOufBounds(gameObject.transform.position)) 
        {
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag=="enemy") other.gameObject.GetComponent<EnemyScript>().SetHealth(-damage);
        SplatterEffect.Instance.DoSplat(other.contacts[0].point, 10);
        Destroy(gameObject);
    }
}
