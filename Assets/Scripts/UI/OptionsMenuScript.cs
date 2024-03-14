using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuScript : MonoBehaviour
{
    public Slider master, sound, music;
    void Start()
    {
      /* master.value = Mathf.Pow(10, MixerHandler.masterVol/20);
       sound.value = Mathf.Pow(10, MixerHandler.soundVol/20);
       music.value = Mathf.Pow(10, MixerHandler.musicVol/20)*/ //this shit doesn't seem to be working properly so I just disabled it. Audio should retain its settings regardless, it just won't be reflected by the sliders


       
    }
}
