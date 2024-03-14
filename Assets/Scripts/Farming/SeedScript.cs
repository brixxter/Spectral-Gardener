using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedScript : MonoBehaviour
{
    public ParticleSystem seeds;
    public static int seedID;
    public GameObject selectionMarker;
    public GameObject[] seedIcons;

    void Start() {
        seedID=0;
    }
    
    void Update()
    {
        selectionMarker.transform.position = seedIcons[seedID].transform.position;
        if(Input.GetKeyDown(KeyCode.Q)) seedID++;
        seedID = (int)Mathf.Repeat(seedID,CropStats.Instance.crops.Length);
        if (Input.GetKeyDown(KeyCode.E)) StartCoroutine(throwseeds());
        if (Input.GetKeyUp(KeyCode.E)) StopAllCoroutines();
    }

    IEnumerator throwseeds()
    {
        while (true)
        {
            seeds.Emit(1);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
