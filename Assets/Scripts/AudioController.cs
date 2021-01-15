using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource _audioSource;

    public List<AudioClip> myClips;


    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(int clipIndex) {
        _audioSource.clip = myClips[clipIndex];
        _audioSource.Play();
    }

    public void Stop(float fadeTime) {
        StartCoroutine(FadeOut(_audioSource, fadeTime));
    }

    private static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

}
