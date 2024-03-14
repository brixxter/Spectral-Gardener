using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSceneScript : MonoBehaviour
{
    public Color[] skyboxTint, ambientlightCol, fogCol, dirLightCol;
    public Light directionalLight, pointLight;
    public static int menuLightingIndex;
    private void Start()
    {
        //RefreshSkybox();
    }

    private void OnEnable()
    {
        RefreshSkybox();
    }

    private void RefreshSkybox()
    {

        if(menuLightingIndex == 1)
        {
            pointLight.enabled = true;
        } else {
            pointLight.enabled = false;
        }

        RenderSettings.fogColor = fogCol[menuLightingIndex];
        RenderSettings.ambientLight = ambientlightCol[menuLightingIndex];

        directionalLight.color = dirLightCol[menuLightingIndex];
        directionalLight.intensity = 1.3f;

        RenderSettings.skybox.SetColor("_Tint", skyboxTint[menuLightingIndex]);
        menuLightingIndex++;
        menuLightingIndex = (int)Mathf.Repeat(menuLightingIndex, skyboxTint.Length);
    }
}
