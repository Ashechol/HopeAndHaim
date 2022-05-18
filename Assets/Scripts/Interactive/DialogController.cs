using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    AudioSource _voice;

    public List<AudioClip> voiceClips;

    void Awake()
    {
        _voice = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (!SceneLoadManager.Instance.skipBegining)
            StartCoroutine(Begining());
    }

    IEnumerator Begining()
    {
        _voice.clip = voiceClips[0];
        _voice.Play();

        while (_voice.isPlaying)
            yield return null;

        _voice.clip = voiceClips[1];
        _voice.Play();
    }
}
