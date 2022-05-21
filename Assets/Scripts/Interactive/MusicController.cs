using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    AudioSource _source;

    public AudioClip normal;
    public AudioClip dead;

    void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    void Start()
    {
        GameManager.Instance.music = this;
        _source.clip = normal;
        _source.loop = true;
        _source.Play();
    }

    public void PlayDeadMusic()
    {
        _source.clip = dead;
        _source.Play();
    }

    public void PlayNormalMusic()
    {
        _source.clip = normal;
        _source.Play();
    }
}
