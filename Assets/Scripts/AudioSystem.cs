using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem
{
    AudioSource source;
    AudioClip[] clips;
    public AudioSystem(AudioSource source, AudioClip[] clips)
    {
        this.source = source;
        this.clips = clips;
    }

    public void PlayAudio(int id)
    {
        source.clip = clips[id];
        source.Play();
    }
}
