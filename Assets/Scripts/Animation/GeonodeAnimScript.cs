using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeonodeAnimScript : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private Mesh skinnedMesh;
    private int blendShapeCount;
    private int playIndex = 0;

    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        skinnedMesh = skinnedMeshRenderer.sharedMesh;
        blendShapeCount = skinnedMesh.blendShapeCount;
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        while(skinnedMeshRenderer.GetBlendShapeWeight(blendShapeCount-1)<100)
        {
            yield return new WaitForEndOfFrame();
            if (playIndex > 0) skinnedMeshRenderer.SetBlendShapeWeight(playIndex - 1, 0);
            if (playIndex == 0) skinnedMeshRenderer.SetBlendShapeWeight(blendShapeCount - 1, 0);

            skinnedMeshRenderer.SetBlendShapeWeight(playIndex, 100f);

            playIndex++;

            if (playIndex > blendShapeCount - 1) playIndex = 0;

            skinnedMeshRenderer.SetBlendShapeWeight(0, 100);
        }
    }
}
