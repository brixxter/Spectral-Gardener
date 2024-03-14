using System.Collections;
using UnityEngine;

public class PlotScript : MonoBehaviour
{
    public Material plotMat;
    private Material mat;
    public int moisture;
    private Color initialCol;
    public bool cropPlanted;
    private GameObject currentCrop;

    void Start()
    {
        cropPlanted = false;
        initialCol = plotMat.color;
        mat = this.GetComponent<MeshRenderer>().material;
        ChangeMoisture(0);
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "water") ChangeMoisture(5);
        if (other.tag == "seed" && cropPlanted == false) SetCrop(SeedScript.seedID);
    }

    public bool ChangeMoisture(int x)
    {
        bool moistureAvailable = (x<0) ? moisture>-x : true;
        moisture = Mathf.Clamp(moisture + x, 0, 50);
        mat.color = initialCol - 0.7f * initialCol * moisture / 50; //changing the plot material "wetness"
        //if (!moistureAvailable) Debug.Log("Moisture depleted, plant can't grow!");
        return moistureAvailable;
    }

    void SetCrop(int id)
    {
        currentCrop = Instantiate(CropStats.Instance.crops[id], transform);
        if (currentCrop != null)
        {
            cropPlanted = true;
        }
    }

}
