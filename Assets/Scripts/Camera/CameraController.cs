using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float cameraDistance, inputSensitivity;
    private float inputX, inputY;

    private void Start() 
    {   
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position;
        inputX += inputSensitivity*Input.GetAxis("Mouse X");
        inputY -= inputSensitivity*Input.GetAxisRaw("Mouse Y");

        inputY = Mathf.Clamp(inputY,-70f,70f);

        transform.eulerAngles = new Vector3(inputY, inputX, 0);
    }
}
