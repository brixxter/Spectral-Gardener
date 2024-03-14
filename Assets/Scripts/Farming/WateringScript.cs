using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringScript : MonoBehaviour
{
    public ParticleSystem droplets;

    void Start()
    {
        PlayerStats.playerDeath += OnPlayerDeath;
        droplets.tag = "water";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            StopAllCoroutines();
            StartCoroutine(Watering());
        }
    }

    private void OnPlayerDeath()
    {
        gameObject.SetActive(false);
    }

    IEnumerator Watering()
    {
        Animator anim = gameObject.GetComponent<Animator>();
        anim.Play("watering");
        while (Input.GetKey(KeyCode.Mouse1))
        {
            yield return new WaitForEndOfFrame();
            droplets.Emit(1);
        }
        anim.Play("watering stop");
        yield return new WaitForSeconds(0.3f);
        anim.Play("New State");
    }

    private void OnDestroy()
    {
        PlayerStats.playerDeath -= OnPlayerDeath;
    }


}
