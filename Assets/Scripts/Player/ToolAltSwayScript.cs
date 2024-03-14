using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAltSwayScript : MonoBehaviour
{
    public float intensity=3, smoothing=5;
    private Quaternion originRot;

    private void Start() {
        originRot = transform.localRotation;
    }

    void Update()
    {
        AltSwayTool();
    }

    private void AltSwayTool()
    {
        float inputX = Input.GetAxis("Horizontal");
        Quaternion targetXAdjusted = Quaternion.AngleAxis(-intensity * inputX, Vector3.forward);
        Quaternion targetRot = originRot * targetXAdjusted;

        gameObject.transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, Time.deltaTime*smoothing);
    }
}
