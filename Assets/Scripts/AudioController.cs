using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource _audioSource;

    public AudioClip music1;
    public AudioClip music2;
    public AudioClip music3;

    public AudioClip ambient1;
    public AudioClip ambient2;
    public AudioClip ambient3;



    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(int clipIndex) {
        _audioSource.clip = music1;
        _audioSource.Play();
    }

    public void PlayAmbient(int clipIndex) {
        _audioSource.clip = ambient2;
        _audioSource.Play();
    }

}
