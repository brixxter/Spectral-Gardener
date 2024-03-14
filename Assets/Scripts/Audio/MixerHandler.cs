using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerHandler : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    public static float masterVol=1f, soundVol=1f, musicVol=1f;

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(level)*20f);
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(level)*20f);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(level)*20f);
    }
}
