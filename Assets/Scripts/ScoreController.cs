using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    AudioSource source;
    AudioController audioController;
    bool songQueued = false;

    // Songs cheatsheet
    // this is sooo fragile, but time is short!
    // 0 = somber viola, starting song
    // 1 = renaissance strings, hopeful
    // 2 = duduk with orch, optimistic
    // 3 = sweeping sad, dark

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        audioController = GetComponent<AudioController>();
        source.loop = false;
            
    }

    // Update is called once per frame
    void Update()
    {
        // Yes, this is janky, but...
        // when song ends, determine which song to play next...
        if (!source.isPlaying && !songQueued) {
            Debug.Log("music ended...");
            int songIndex;
            float m = Game.Instance.MoralePct();
            if (m < 25 ) {
                songIndex = 3;
            } else if (m < 50) {
                songIndex=0;
            } else if (m < 80) {
                songIndex = 0;
            } else {
                songIndex = 2;
            }
            Debug.Log("morale is " + m + "%, selecting song " + songIndex);
            // and play it after a 2 second delay.
            StartCoroutine(PlayMusicAfterDelay(songIndex, 2));
        } 


        
    }

    IEnumerator PlayMusicAfterDelay (int songIndex, int secs=0)
    {
        songQueued = true;
        yield return new WaitForSeconds(secs);
        Debug.Log("starting song " + songIndex);
        audioController.PlayClip(songIndex);
        songQueued = false;
    }
}
