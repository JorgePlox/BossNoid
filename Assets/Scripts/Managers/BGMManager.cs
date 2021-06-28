using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static bool isFading = false;

    bool changingClip = false;
    float currentVolume = 1.0f;

    public static BGMManager sharedInstance;
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    public void ChangeMusic(AudioSource audioSource, AudioClip newSong)
    {
        changingClip = true;
        if (audioSource != null && newSong != null)
        {
            audioSource.volume = currentVolume;
            audioSource.clip = newSong;
            audioSource.Play();
        }
    }

    public IEnumerator FadeSound(AudioSource audioSource , float fadeTime)
    {
        currentVolume = audioSource.volume;
        isFading = true;
        float t = fadeTime;
        while (t > 0)
        {
            yield return null;
            t -= Time.deltaTime;
                if (!changingClip)
                    audioSource.volume = t / fadeTime;
        }
        changingClip = false;
        yield break;
    }
}
