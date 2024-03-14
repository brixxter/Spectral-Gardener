using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTiltScript : MonoBehaviour
{
    public float intensity=3, smoothing=5;
    private Quaternion originRot;

    private void Start() {
        originRot = transform.localRotation;
    }

    private void Update() {
        TiltTool();
    }

    private void TiltTool()
    {
        float inputX = Input.GetAxis("Horizontal");
        Quaternion targetXAdjusted = Quaternion.AngleAxis(intensity * inputX, Vector3.up);
        Quaternion targetRot = originRot * targetXAdjusted;

        gameObject.transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, Time.deltaTime*smoothing);
    }
    
}
