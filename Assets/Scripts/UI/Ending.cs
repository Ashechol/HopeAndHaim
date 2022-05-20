using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Ending : MonoBehaviour
{
    VideoPlayer videoPlayer;

    public VideoClip newLife;
    public VideoClip exile;
    public VideoClip obey;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Start()
    {
        switch (GameManager.Instance.gameEnding)
        {
            case GameManager.GameEnding.NewLife:
                videoPlayer.clip = newLife;
                break;

            case GameManager.GameEnding.Obey:
                videoPlayer.clip = obey;
                break;

            case GameManager.GameEnding.Exile:
                videoPlayer.clip = exile;
                break;
        }

        StartCoroutine(PlayEndingMovie());

    }

    IEnumerator PlayEndingMovie()
    {
        videoPlayer.Play();

        while (!videoPlayer.isPlaying)
            yield return null;

        while (videoPlayer.isPlaying && !Input.GetKeyDown(KeyCode.Space))
            yield return null;

        SceneLoadManager.Instance.LoadMainMenu();
    }

}
