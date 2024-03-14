using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
public class PlayerController : MonoBehaviour
{

    public float walkAccel, jumpStrength;
    private float verticalVel;
    public CharacterController controller;
    public Camera cam;

    [HideInInspector]
    public bool isGrounded;

    private float inputHorizontal, inputVertical;

    private void OnEnable()
    {
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerStats.Instance.alive) return;
        
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");

        transform.eulerAngles = new Vector3(transform.eulerAngles.x,cam.transform.eulerAngles.y,transform.eulerAngles.z); 

        controller.Move((transform.forward * inputVertical + transform.right * inputHorizontal) * walkAccel * Time.deltaTime);

        //if (isGrounded) verticalVel = 0;
        verticalVel += Time.deltaTime * Physics.gravity.y * 3;
        if (Input.GetKeyDown(KeyCode.Space)) OnSpacePress();
        controller.Move(verticalVel * Time.deltaTime * transform.up);
    }

    void OnSpacePress()
    {
        if (isGrounded)
        {
            isGrounded = false;
            verticalVel = jumpStrength;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        float slope = Vector3.Dot(hit.normal, transform.up);
        //Debug.Log("Slope: " + slope);
        if(slope > 0.65f)
        { 
        isGrounded = true;
        if(verticalVel<=0)verticalVel = 0;
        }
    }

    public Vector3 GetSpeed()
    {
        return controller.velocity;
    }

}
