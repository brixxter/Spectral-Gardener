using System.Collections;
using UnityEngine;

public class CropScript : MonoBehaviour
{
    public GameObject[] stateObject;
    public GameObject worldHandler;
    public int growthTime, itemYield, particleAmount, moistureConsumption, moistureTotal, cropID;
    private GameObject currentInstance;
    private ParticleSystem harvestEffect;
    private PlotScript plotScript;

    private int growthState;

    private void Start()
    {
        WorldScript.growthEvent += OnGrowthTick;
        plotScript = gameObject.GetComponentInParent<PlotScript>();
        harvestEffect = gameObject.GetComponentInChildren<ParticleSystem>();
        UpdateObject();
    }

    private void UpdateObject()
    {
        bool ripe = growthState >= stateObject.Length;
        if (currentInstance != null) Destroy(currentInstance);
        if (!ripe)
        {
            moistureTotal = 0;
            currentInstance = Instantiate(stateObject[growthState]);
            currentInstance.transform.parent = transform;
            currentInstance.transform.localPosition = new Vector3(0, 0, 0);
        }
        else
        {
            YieldDrops();
        }
    }

    private void OnGrowthTick()
    {
        if (plotScript.ChangeMoisture(-moistureConsumption))
        {
            moistureTotal += moistureConsumption;
            if (moistureTotal > growthTime)
            {
                growthState++;
                UpdateObject();
            }
        }
    }

    private void YieldDrops()
    {
        PlayerStats.Instance.cropAmount[cropID] += itemYield;
        PlayerStats.Instance.harvestedCrops++;
        //Debug.Log("Player earned " + itemYield + " of item " + cropID + " for a total of " + PlayerStats.Instance.cropAmount[cropID]); 
        plotScript.cropPlanted = false;
        harvestEffect.Emit(particleAmount);
        WorldScript.growthEvent -= OnGrowthTick;
        StartCoroutine(DeleteDelay());
    }


    IEnumerator DeleteDelay()
    {
        yield return new WaitForSeconds(1.4f);
        Destroy(gameObject);     
    }

    private void OnDestroy()
    {
        WorldScript.growthEvent -= OnGrowthTick;
    }
}
