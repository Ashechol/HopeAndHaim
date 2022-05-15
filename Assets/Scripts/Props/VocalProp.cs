using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocalProp : TriggerProp
{
    private AudioClip clip;
    public AudioClip Clip
    {
        get { return clip; }
        set
        {
            clip = value;
            audioSource.clip = value;
        }
    }

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public override void OnHit()
    {
        base.OnHit();
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
