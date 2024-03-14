using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScoreScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    void OnEnable()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = "Score: " + PlayerStats.Instance.score + "<br>Kills: " + PlayerStats.Instance.killCount + "<br>Crops Planted: " + PlayerStats.Instance.harvestedCrops;
    }
}
