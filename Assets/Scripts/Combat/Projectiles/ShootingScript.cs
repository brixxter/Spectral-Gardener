using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShootingScript : MonoBehaviour
{

    public static void FireBullet(GameObject bullet, Transform emissionPoint, float vel)
    {
        Vector3 aimPos, aimDir;
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(WorldScript.screenCenter);
        var bulletInstance = Instantiate(bullet);
        bulletInstance.transform.position = emissionPoint.position;

        if (Physics.Raycast(ray, out hit))
        {
            aimPos = hit.point;
            aimDir = Vector3.Dot(aimPos-emissionPoint.position, emissionPoint.transform.up)<0.2f ? emissionPoint.transform.up : (aimPos-emissionPoint.position).normalized; //making sure bullet hits the target unless it's too close to player, we don't want bullets flying off in awkward directions
        }
        else
        {
            aimDir = (Camera.main.transform.position+Camera.main.transform.forward*500-emissionPoint.transform.position).normalized;
        }
        bulletInstance.GetComponentInChildren<Rigidbody>().velocity = vel * aimDir;
        bulletInstance.transform.forward = aimDir;
    }
}
