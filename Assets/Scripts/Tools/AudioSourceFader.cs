using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 控制 AudioSource 淡入淡出
/// </summary>
public class AudioSourceFader : Fader<AudioSource>
{
    //淡入目标值
    public float targetVolume = 1;

    protected override void FadeIn()
    {
        target.volume = Mathf.Lerp(target.volume, targetVolume, fSpeed * Time.deltaTime);
    }

    protected override void FadeOut()
    {
        target.volume = Mathf.Lerp(target.volume, 0, fSpeed * Time.deltaTime);
    }

    protected override void FadeInInit()
    {
        target.enabled = true;
        target.volume = 0;
    }

    protected override void FadeOutInit()
    {

    }

    protected override void FadeInStart()
    {
        base.FadeInStart();
        target.Play();
    }

    protected override bool IsFadeInEnd()
    {
        return target.volume >= targetVolume - threshold;
    }

    protected override bool IsFadeOutEnd()
    {
        return target.volume <= threshold;
    }

    protected override void FadeInEnd()
    {
        target.volume = targetVolume;
    }

    protected override void FadeOutEnd()
    {
        target.volume = 0;
        target.Stop();
    }
}
