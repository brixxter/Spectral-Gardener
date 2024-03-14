using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Transform player;
    public static PlayerStats Instance { get; private set; }
    public int[] cropAmount;
    public int health, maxHealth, regenAmount, regenSpeed, score, nightsSurvived, killCount, availablePlots, plantedPlots, harvestedCrops, damageDealt, blossomDuration;
    public bool alive;
    public static event Action playerHealthChanged, playerMaxHealthChanged, playerDeath, playerRegenSpeedChanged, playerRegenAmountChanged, scoreChanged;
    public int currentBlossomDuration;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;

        StartCoroutine(GetCrops()); 
    }

    private void Start() {
        alive = true;
        health=100;
        maxHealth=100;
        regenAmount = 1;
        score=0;
        nightsSurvived = 0;
        killCount=0;
        availablePlots = 0;
        plantedPlots = 0;
        harvestedCrops = 0;
        damageDealt = 0;
        blossomDuration = 25;
    }

    public void ChangeScore(int x)
    {
        if (alive)
        {
            score += x;
            if(scoreChanged != null) scoreChanged();
        }
    }

    public void ChangeHealth(int x)
    {
        if (alive)
        {
            health = Mathf.Clamp(health+x,0,maxHealth);
            if (playerHealthChanged != null) playerHealthChanged();

            if (health <= 0)
            {
                alive = false;
                if (playerDeath != null) playerDeath();
                if(x<0)
                {
                    //player hurt sound
                    Sounds.Instance.PlaySound(Sounds.Instance.playerhurt, player, 1f);
                    StartCoroutine(Camera.main.GetComponent<EffectScript>().DamageEffect(-4*x/maxHealth, 1f));
                }
            }
        }
    }

    public void ChangeMaxHealth(int x)
    {
        if (alive)
        {
            maxHealth += x;
            if (playerMaxHealthChanged != null) playerMaxHealthChanged();
        }
    }

    public void ChangeRegenAmount(int x)
    {
        regenAmount += x;
        if (playerRegenAmountChanged != null) playerRegenAmountChanged();
    }

    public void ChangeRegenSpeed(int x)
    {
        regenSpeed += x;
        if (playerRegenSpeedChanged != null) playerRegenSpeedChanged();
    }

    IEnumerator GetCrops()
    {
        yield return new WaitUntil(CropsValid);
        cropAmount = new int[CropStats.Instance.crops.Length];

        //cropAmount[0] = 500;//REMOVE BEFORE BUILD
       // cropAmount[1] = 500;
       // cropAmount[2] = 500;
    }

    private bool CropsValid()
    {
        return CropStats.Instance!=null;
    }
}
