using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSelectionScript : MonoBehaviour
{
    public GameObject[] tools;
    public GameObject[] panels;
    public GameObject selectionPanel;
    public int toolSelected;
    private float currentScroll;
    public static bool switchLocked=false;
    

    private void Start()
    {
        switchLocked = false;
        PlayerStats.playerDeath += OnPlayerDeath;
        SetTool(0);
        toolSelected = 0;  
    }

    void Update()
    {
        int keyPressed = KeyPressed();
        if(keyPressed!=-1) SetTool(keyPressed-1);
        currentScroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if (currentScroll != 0) ChangeInput();
    }

    private void ChangeInput()
    {
        
        if (currentScroll > 0f) toolSelected--; 
        if (currentScroll < 0f) toolSelected++;
        if(toolSelected<0) toolSelected = 2;
        if(toolSelected>2) toolSelected = 0;
        SetTool(toolSelected);
    }

    private void SetTool(int toolID)
    {
        if(switchLocked) return; // I didn't have time to make cooldowns for deselected weapons work 
        if (toolID < tools.Length) // so I had to keep them force equipped until the countdown runs out so players couldn't just switch tools and skip cooldowns
        {
            for (int i = 0; i < tools.Length; i++)
            {
                tools[i].SetActive(i == toolID ? true : false);
            }
            if(toolID!=-1)selectionPanel.transform.position = panels[toolID].transform.position;
        }
        else
        {
            Debug.Log("Selected key has no assigned tool");
        }
    }

    private int KeyPressed()
    {
        int keyPressed = -1;

        for (int number = 0; number <= 9; number++)
        {
            if (Input.GetKeyDown(number.ToString()))
            {
                keyPressed = number;
            }
        }

        return keyPressed;
    }

    private void OnPlayerDeath()
    {
        SetTool(-1); //deselect all tools
        switchLocked = true;
    }

    private void OnDestroy()
    {
        PlayerStats.playerDeath -= OnPlayerDeath;
    }
}

