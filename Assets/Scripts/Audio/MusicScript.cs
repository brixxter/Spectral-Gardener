using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioSource day, night;
    void Start()
    {
        DayTimeScript.dayEvent += OnDayStart;
        DayTimeScript.nightEvent += OnNightStart;
        night.volume = 0;
        day.Stop();
        night.Stop();
    }

    void OnDayStart()
    {
        StartCoroutine(FadeClip(2, night, day));
    }

    void OnNightStart()
    {
        StartCoroutine(FadeClip(2, day, night));
    }

    public IEnumerator FadeClip(float duration, AudioSource old, AudioSource fresh)
    {
        Debug.Log("Fade music");
        float currentTime = 0;
        float start = old.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            old.volume = Mathf.Lerp(start, 0, currentTime / duration);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Fading complete");
        
        fresh.volume = 1;
        fresh.Play();
        old.Stop();
        yield break;
    }

    private void OnDestroy()
    {
        DayTimeScript.dayEvent -= OnDayStart;
        DayTimeScript.nightEvent -= OnNightStart;
    }
}
