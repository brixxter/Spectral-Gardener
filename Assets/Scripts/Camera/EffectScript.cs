using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[ExecuteInEditMode]

public class EffectScript : MonoBehaviour
{
    public static EffectScript Instance; //I should really rely less on singletons
    public Material pixelMat;
    public float resolution, colorDepth;

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        SetFilter(resolution, colorDepth);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, pixelMat);
        pixelMat.SetFloat("_PixelCountU", 16 * resolution);
        pixelMat.SetFloat("_PixelCountV", 9 * resolution);
        pixelMat.SetFloat("_ColorDepth", colorDepth);
    }

    public void SetFilter(float res, float cDepth)
    {
        resolution = res;
        colorDepth = cDepth;
    }

    public IEnumerator DamageEffect(float intensity, float duration)
    {
        float tempres = resolution;
        float tempcdepth = colorDepth;

        resolution *= 1 / intensity;
        colorDepth *= 1 / intensity;
        yield return new WaitForSeconds(duration);
        resolution = tempres;
        colorDepth = tempcdepth;
    }
}
