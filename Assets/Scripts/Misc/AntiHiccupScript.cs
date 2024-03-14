using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiHiccupScript : MonoBehaviour
{
    public GameObject[] objects;
    void Start()
    {
        for(int i=0; i<objects.Length; i++)
        {
            var objectInstance = Instantiate(objects[i]);
            objectInstance.transform.position = new Vector3(1000,0,0);
            Destroy(objectInstance);
        }
    }

   
}
