using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemTextScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = PlayerStats.Instance.cropAmount[0] + "<br>" + PlayerStats.Instance.cropAmount[1] + "<br>" + PlayerStats.Instance.cropAmount[2];
    }
}
