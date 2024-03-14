using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternScript : MonoBehaviour
{
    public Light lantern;
    public Material glowMat;
    public Color glowCol;
    public float fadeSpeed;
    private float emissionStrength;
    private Rigidbody rb;
    void Start()
    {
        //transform.parent = null;
        rb = gameObject.GetComponent<Rigidbody>();
        glowMat = gameObject.GetComponent<MeshRenderer>().materials[0];
        lantern = gameObject.GetComponent<Light>();
        glowMat.SetColor("_EmissionColor", glowCol * 0);
        DayTimeScript.dayEvent += OnDayTimeStart;
        DayTimeScript.nightEvent += OnNightTimeStart;
        PlayerStats.playerDeath += OnPlayerDeath;
    }

    private void Update() {
        float inputWalkX = 5*Input.GetAxis("Horizontal"); //Lantern shake. Wish it was more straightforward but parent motion doesn't play nice with joints
        float inputMouseX = Input.GetAxis("Mouse X");
        float combinedForce = 10*inputMouseX+1.4f*inputWalkX;
        rb.AddForce(transform.up*combinedForce*50f*Time.deltaTime);
    }

    void OnDayTimeStart()
    {
        StopAllCoroutines();
        StartCoroutine(fadeBrightness(0));      
    }

    void OnNightTimeStart()
    {
        StopAllCoroutines();
        StartCoroutine(fadeBrightness(1.5f));
    }

    private void OnPlayerDeath()
    {
        gameObject.SetActive(false);
    }

    IEnumerator fadeBrightness(float target)
    {
        float initialTime = Time.time;
        target = Mathf.Clamp(target, 0, 2);

        while (emissionStrength != target)
        {
            emissionStrength = Mathf.Lerp(emissionStrength, target, fadeSpeed * (Time.time - initialTime));
            glowMat.SetColor("_EmissionColor", glowCol * emissionStrength);
            lantern.intensity = 1.5f * emissionStrength;

            yield return new WaitForEndOfFrame();
        }
    }

    private void OnDestroy() {
        DayTimeScript.dayEvent -= OnDayTimeStart;
        DayTimeScript.nightEvent -= OnNightTimeStart;
        PlayerStats.playerDeath -= OnPlayerDeath;
    }
}
