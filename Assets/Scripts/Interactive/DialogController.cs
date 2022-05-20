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
        GameManager.Instance.dialog = this;
        if (!SceneLoadManager.Instance.skipBegining)
            StartCoroutine(Begining());
    }

    void OnDisable()
    {
        GameManager.Instance.dialog = null;
    }

    IEnumerator Begining()
    {
        GameManager.Instance.gameMode = GameManager.GameMode.Dialog;

        _voice.clip = voiceClips[0];
        _voice.Play();

        while (_voice.isPlaying && !Input.GetKeyDown(KeyCode.E))
            yield return null;

        _voice.Stop();

        _voice.clip = voiceClips[1];
        _voice.Play();

        while (_voice.isPlaying && !Input.GetKeyDown(KeyCode.E))
            yield return null;

        _voice.Stop();

        UIManager.Instance.ShowBeginingTip();

        GameManager.Instance.gameMode = GameManager.GameMode.Gameplay;
    }

    public void PlayDiaglog(AudioClip dialog)
    {
        _voice.clip = dialog;
        _voice.Play();
    }
}
