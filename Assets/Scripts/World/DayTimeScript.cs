using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimeScript : MonoBehaviour
{
    [Header("Gradients")]
    [SerializeField] private Gradient fogGradient;
    [SerializeField] private Gradient ambientGradient;
    [SerializeField] private Gradient directionalLightGradient;
    [SerializeField] private Gradient skyboxTintGradient;

    [Header("Environmental Assets")]
    [SerializeField] private Light directionalLight;
    [SerializeField] private Material skyboxMat;

    [Header("Variables")]
    [SerializeField] private float dayDuration = 60f;
    [SerializeField] private float skyboxRotationSpeed = 1f;

    public static float currentTime = 0;
    public float daySpeed, nightSpeed;
    private float timeSpeed;
    public static event Action dayEvent;
    public static event Action nightEvent;


    private void Start()
    {
        currentTime = 0.8f;
        StartCoroutine(EventTracker());
    }
    private void Update()
    {
        UpdateTime();
        UpdateDayCycle();
        RotateSkybox();
    }

    private void UpdateTime()
    {
        currentTime += Time.deltaTime*timeSpeed / dayDuration;
        currentTime = Mathf.Repeat(currentTime, 1f);
    }

    private void UpdateDayCycle()
    {
        float sunPos = Mathf.Repeat(currentTime + 0.25f, 1f);
        directionalLight.transform.rotation = Quaternion.Euler(sunPos * 360f, 0f, 0f);

        RenderSettings.fogColor = fogGradient.Evaluate(currentTime);
        RenderSettings.ambientLight = ambientGradient.Evaluate(currentTime);

        directionalLight.color = directionalLightGradient.Evaluate(currentTime);

        skyboxMat.SetColor("_Tint", skyboxTintGradient.Evaluate(currentTime));
    }

    private void RotateSkybox()
    {
        float currentRotation = skyboxMat.GetFloat("_Rotation");
        float newRotation = currentRotation + skyboxRotationSpeed * Time.deltaTime;
        newRotation = Mathf.Repeat(newRotation, 360f);

        skyboxMat.SetFloat("_Rotation", newRotation);
    }

    private void OnApplicationQuit()
    {
        skyboxMat.SetColor("_Tint", new Color(0.5f, 0.5f, 0.5f));
    }

    IEnumerator EventTracker()
    {
        while (true)
        {
            yield return new WaitUntil(isDayTime);
            timeSpeed = daySpeed;
            PlayerStats.Instance.availablePlots += 5;
            if (dayEvent != null) dayEvent();  //daytime begins
            yield return new WaitUntil(isNightTime);
            timeSpeed = nightSpeed;
            PlayerStats.Instance.nightsSurvived++;
            
            if (nightEvent != null) nightEvent();  //nighttime begins   
        }
    }

    private bool isNightTime()
    {
        return currentTime>0.25f&&currentTime<0.75f;
    }

    private bool isDayTime()
    {
        return currentTime<0.25f | currentTime>0.75f;
    }

}
