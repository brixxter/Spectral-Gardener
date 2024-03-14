using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class XPChoices : MonoBehaviour
{
    public GameObject[] healthBoost, rifleBoost, lilyBoost;
    public GameObject ChoicePanel, rifle, bazooka;

    private void Start() {
        DayTimeScript.dayEvent += OnDayStart;
    }

    private void OnDayStart()
    {
        if(!PlayerStats.Instance.alive) return;
        if(PlayerStats.Instance.nightsSurvived<=0) return;
        OpenUpgradePanel();
    }

    public void OpenUpgradePanel()
    {
        PauseMenuScript.Instance.Pause();
        ChoicePanel.SetActive(true);
        int rand1 = Random.Range(0, healthBoost.Length);
        int rand2 = Random.Range(0, rifleBoost.Length);
        int rand3 = Random.Range(0, lilyBoost.Length);

        healthBoost[rand1].SetActive(true);
        rifleBoost[rand2].SetActive(true);
        lilyBoost[rand3].SetActive(true);
    }

    public void CloseUpgradePanel()
    { 
        for(int i=0; i<healthBoost.Length; i++) healthBoost[i].SetActive(false);
        for(int i=0; i<rifleBoost.Length; i++) rifleBoost[i].SetActive(false);
        for(int i=0; i<lilyBoost.Length; i++) lilyBoost[i].SetActive(false);
        ChoicePanel.SetActive(false);
        PauseMenuScript.Instance.Resume();
    }

    public void MaxHealthBoost()
    {
        PlayerStats.Instance.ChangeMaxHealth(50);
    }

    public void RegenBoost()
    {
        PlayerStats.Instance.regenAmount++;
    }

    public void BlossomBoost()
    {
        PlayerStats.Instance.blossomDuration+=25;
    }

    public void RifleClipBoost()
    {
        rifle.GetComponent<RifleScript>().BoostClipSize();
    }

    public void RifleDMGBoost()
    {
        rifle.GetComponent<RifleScript>().BoostDamage();
    }

    public void BeanYieldBoost()
    {
       CropStats.Instance.crops[0].GetComponent<CropScript>().itemYield+=10;
    }

    public void LilySizeBoost()
    {
        bazooka.GetComponent<BazookaScript>().BoostSize();
    }

    public void LilyDMGBoost()
    {
        bazooka.GetComponent<BazookaScript>().BoostDamage();
    }

    public void LilyYieldBoost()
    {
        CropStats.Instance.crops[1].GetComponent<CropScript>().itemYield+=1;
    }

    private void OnDestroy() {
        DayTimeScript.dayEvent -= OnDayStart;
    }



}
