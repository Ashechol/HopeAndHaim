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
    public AudioClip openDoorClip;

    private Hope _hope;

    private bool _canInput;

    private void Start()
    {
        _hope = GameManager.Instance.hope;
    }

    private void Update()
    {
        if (GameManager.Instance.CanInput() && _canInput)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("第一幕最后演出");
                GameManager.Instance.gameMode = GameManager.GameMode.Timeline;
                _hope.StopHope();
                //播放开门声
                _hope.HearSource.clip = openDoorClip;
                _hope.HearSource.loop = false;
                _hope.HearSource.Play();

                if (!_hope.HearSource.isPlaying)
                {
                    Debug.Log("第一幕结束");
                    //TODO: 播放间幕
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //播放语音
            if (!_hope.HearSource.isPlaying)
            {
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //隐藏文字
            UIManager.Instance.CleanDialogue();

            _canInput = false;
        }
    }
}
