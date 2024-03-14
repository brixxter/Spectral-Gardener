using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSwayScript : MonoBehaviour
{
    public float intensity=3, smoothing=5;
    private Quaternion originRot;

    private void Start() {
        originRot = transform.localRotation;
    }

    void Update()
    {
        SwayTool();
    }

    private void SwayTool()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Quaternion targetXAdjusted = Quaternion.AngleAxis(intensity * mouseX, Vector3.up);
        Quaternion targetYAdjusted = Quaternion.AngleAxis(-intensity * mouseY, Vector3.right);
        Quaternion targetRot = originRot * targetXAdjusted * targetYAdjusted;

        gameObject.transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, Time.deltaTime*smoothing);
    }
}
