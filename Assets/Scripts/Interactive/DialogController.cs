using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    AudioSource _voice;

    public List<AudioClip> beginingClips;
    public List<AudioClip> idleClips;
    public List<AudioClip> dialog;
    public float dialogPlayChance = 0.2f;
    public float diceChanceTime = 20;
    public bool dialogReady;
    bool canRandom;

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

        _voice.clip = beginingClips[0];
        _voice.Play();

        while (_voice.isPlaying && !Input.GetKeyDown(KeyCode.E))
            yield return null;

        _voice.Stop();

        _voice.clip = beginingClips[1];
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

    public void PlayRandomDialog()
    {
        if (dialog.Count == 0)
            return;

        if (canRandom == false)
            StartCoroutine(NextDialogTimer());

        if (canRandom == true)
        {
            canRandom = false;
            dialogReady = Random.Range(0f, 1f) < dialogPlayChance;
            Debug.Log("Dice! " + dialogReady);
        }

        if (dialogReady && !_voice.isPlaying)
        {
            dialogReady = false;

            int clipIndex = Random.Range(0, dialog.Count);

            PlayDiaglog(dialog[clipIndex]);

            dialog.Remove(dialog[clipIndex]);
        }
        else
            dialogReady = false;
    }

    public void PlayRandomIdle()
    {
        if (!_voice.isPlaying)
        {
            int clipIndex = Random.Range(0, 2);

            PlayDiaglog(idleClips[clipIndex]);
        }
    }

    IEnumerator NextDialogTimer()
    {
        float timer = diceChanceTime;

        while (timer > 0)
        {
            // Debug.Log("random倒计时: " + timer);
            timer -= Time.deltaTime;
            yield return null;
        }

        canRandom = true;
    }
}
