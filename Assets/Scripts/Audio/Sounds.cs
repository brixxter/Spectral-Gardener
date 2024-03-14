using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static Sounds Instance;
    public AudioSource sound;
    public AudioClip dig, plant, harvest, grow, riflereload, rifleshoot, bazookacharge, bazookashoot, bazookareload, explosion, 
    slugattack, slughit, slugdeath, slimattack, slimhit, slimdeath, simpleattack, simplehit, simpledeath, noammo,playerhurt;

    private void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlaySound(AudioClip audio, Transform targetTransform, float amp)
    {
        AudioSource audioSource = Instantiate(sound, targetTransform.position, Quaternion.identity);
        audioSource.clip = audio;
        audioSource.volume = amp;
        audioSource.pitch = Random.Range(0.9f,1.1f);
        //audioSource.spread = range;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
