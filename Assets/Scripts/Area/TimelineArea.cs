using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// 播放 Timeline 的区域
/// 特点是只执行一次
/// </summary>
public class TimelineArea : MonoBehaviour
{
    protected PlayableDirector _director;
    protected bool _isFirst = false;

    public PlayableAsset asset;

    //是否进入剧情模式，即玩家无法操作
    public bool isInPlotMode = true;

    protected virtual void Awake()
    {
        _director = GameObject.FindGameObjectWithTag("Director").GetComponent<PlayableDirector>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //尽管不太可能，但还是检测一下
        if (!_isFirst && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _isFirst = true;
            if (isInPlotMode)
            {
                GameManager.Instance.gameMode = GameManager.GameMode.Timeline;
            }
            //播放 Timeline
            _director.playableAsset = asset;
            _director.Play();
        }
    }
}
