using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// 对话框片段
/// </summary>
public class DialogueClip : PlayableAsset
{
    [SerializeField]
    private DialogueBehaviour template = new DialogueBehaviour();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DialogueBehaviour>.Create(graph, template);
        return playable;
    }
}
