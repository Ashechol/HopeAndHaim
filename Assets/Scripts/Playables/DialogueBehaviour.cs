using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// 对话框行为
/// </summary>
[System.Serializable]
public class DialogueBehaviour : PlayableBehaviour
{
    private PlayableDirector director;

    //对话框内容
    [TextArea(8, 1)] public string dialogueLine;
    //字体尺寸
    public int dialogueSize = 34;

    //当前片段是否在播放
    private bool isClipPlayer = false;

    //是否需要按下按键继续
    public bool requirePause = false;
    //暂停判断
    private bool pauseScheduled = false;

    public override void OnPlayableCreate(Playable playable)
    {
        //获得 director
        director = playable.GetGraph().GetResolver() as PlayableDirector;
    }

    //每一帧调用
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        //没有播放且权重足够
        if (!isClipPlayer && info.weight > 0) {
            //写入 UI 中
            UIManager.Instance.DisplayDialogue(dialogueLine, dialogueSize);

            //暂停需要
            if (requirePause) {
                pauseScheduled = true;
            }

            isClipPlayer = true;
        }
    }

    //暂停行为
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (pauseScheduled) {
            pauseScheduled = false;
            //暂停 Timeline
            GameManager.Instance.PauseTimeline();
        }
        else {
            //关闭对话框
            UIManager.Instance.CleanDialogue();
        }
    }
}
