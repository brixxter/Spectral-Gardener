using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlotPreviewScript : MonoBehaviour
{
    public Material previewMat;
    public static bool validPlacement;
    public bool plotAvailable;

    private void Start() 
    {
        previewMat = gameObject.GetComponent<MeshRenderer>().material;
        previewMat.color = new Color(0,0,1);
        validPlacement = true;
    }

    private void OnTriggerEnter(Collider other) {
        previewMat.color = new Color(1,0,0);
        validPlacement = false;
    }   

    private void OnTriggerExit(Collider other) {
        previewMat.color = new Color(0,0,1);
        validPlacement = true;
    }

}
