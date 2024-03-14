using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WorldScript : MonoBehaviour
{
    public float tickTime;
    public static event Action growthEvent;
    public static Vector3 screenCenter;
    
    void Start()
    {
        StartCoroutine(Growth());
        screenCenter = new Vector3(Screen.width/2,Screen.height/2, 0f);
    }

    IEnumerator Growth()
    {
        while (true)
        {
            if(growthEvent!=null) growthEvent();
            yield return new WaitForSeconds(tickTime);
        }
    }

    public static bool OutOufBounds(Vector3 pos)
    {
        return pos.magnitude>250;
    }
}
