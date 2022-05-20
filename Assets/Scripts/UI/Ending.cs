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

        videoPlayer.Play();
    }

    void Update()
    {
        Debug.Log(videoPlayer.isPlaying);

        //TODO: 过场动画播放完回到主菜单
        // if (!videoPlayer.isPlaying)
        //     SceneLoadManager.Instance.LoadMainMenu();
    }

}
