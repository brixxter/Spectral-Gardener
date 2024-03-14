using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePanelScript : MonoBehaviour
{

    public TextMeshProUGUI text;
    void Start()
    {
        PlayerStats.scoreChanged += OnScoreChanged;   
        text = gameObject.GetComponent<TextMeshProUGUI>(); 
        OnScoreChanged();
    }

    void OnScoreChanged()
    {
        text.text = "Score: " + PlayerStats.Instance.score;
    }

    private void OnDestroy() {
        PlayerStats.scoreChanged -= OnScoreChanged; 
    }
}
