using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    public Transform cam;

    private void Start() {
        cam = Camera.main.transform;
    }
    void LateUpdate()
    {
        transform.LookAt(cam.transform);
        transform.Rotate(0,180,0);
    }
}
