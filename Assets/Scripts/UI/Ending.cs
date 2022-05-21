using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    VideoPlayer _videoPlayer;
    GameObject _skipTip;

    public VideoClip newLife;
    public VideoClip exile;
    public VideoClip obey;
    public float skipTipTimer = 3;
    public bool timerRunning;


    void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _skipTip = FindObjectOfType<Canvas>().transform.GetChild(0).gameObject;
    }

    void Start()
    {
        switch (GameManager.Instance.gameEnding)
        {
            case GameManager.GameEnding.NewLife:
                _videoPlayer.clip = newLife;
                break;

            case GameManager.GameEnding.Obey:
                _videoPlayer.clip = obey;
                break;

            case GameManager.GameEnding.Exile:
                _videoPlayer.clip = exile;
                break;
        }

        StartCoroutine(PlayEndingMovie());

    }

    IEnumerator PlayEndingMovie()
    {
        _videoPlayer.Play();

        while (!_videoPlayer.isPlaying)
            yield return null;

        while (_videoPlayer.isPlaying && !Input.GetKeyDown(KeyCode.End))
        {

            if (!_skipTip.activeInHierarchy)
            {
                if (Input.anyKeyDown)
                    StartCoroutine(ShowSkipTip(skipTipTimer));
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                _videoPlayer.Stop();
                _skipTip.SetActive(false);
            }

            yield return null;
        }

        GameManager.Instance.gameMode = GameManager.GameMode.Gameplay;
        SceneLoadManager.Instance.skipBegining = false;

        SceneLoadManager.Instance.LoadMainMenu();
    }

    IEnumerator ShowSkipTip(float skipTipTimer)
    {

        if (GameManager.Instance.gameEnding == GameManager.GameEnding.NewLife)
            yield break;

        float timer = skipTipTimer;
        _skipTip.SetActive(true);

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        _skipTip.SetActive(false);
    }

}
