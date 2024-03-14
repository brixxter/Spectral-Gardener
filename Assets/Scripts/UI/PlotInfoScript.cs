using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlotInfoScript : MonoBehaviour
{
    private TextMeshProUGUI text;
    private void Start() {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if(PlayerStats.Instance.availablePlots>0)
        {
           text.text =  "Available plots: " + PlayerStats.Instance.availablePlots;
        } else {
            text.text =  "More plots available in 1 day";
        }
    }
}
