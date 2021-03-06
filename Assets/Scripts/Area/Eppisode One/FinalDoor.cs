using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 第一幕最终大门门把
/// </summary>
public class FinalDoor : MonoBehaviour
{
    public int fontSize = 34;

    public AudioClip plotClip;

    //音响淡出器
    public AudioSourceFader soundFader;

    private Hope _hope;

    private bool _canInput;

    private void Start()
    {
        _hope = GameManager.Instance.hope;
        //淡出后才结束
        soundFader.endActions += () => {
            GameManager.Instance.isEpisodeOneEnd = true;
        };
    }

    private void Update()
    {
        if (GameManager.Instance.CanInput() && _canInput) {
            if (Input.GetKeyDown(KeyCode.E)) {
                //音响淡出
                soundFader.StartFading();

                StartCoroutine(GameManager.Instance.EpisodeOneEnd());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            //播放语音：语音不是正在播放 && 声音淡出没有结束，即第一幕场景没有结束
            if (!_hope.IsHearSourcePlaying() && !soundFader.isEnd) {
                _hope.HearSource.clip = plotClip;
                _hope.HearSource.Play();
            }
            //显示文字
            UIManager.Instance.DisplayDialogue("【请按下E键，拉动门把手。】", fontSize);
            //开启输入
            _canInput = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            //隐藏文字
            UIManager.Instance.CleanDialogue();

            _canInput = false;
        }
    }
}
