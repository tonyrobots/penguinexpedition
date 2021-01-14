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


}
