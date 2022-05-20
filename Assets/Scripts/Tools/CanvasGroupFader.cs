using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Panel 淡入淡出
/// </summary>
public class CanvasGroupFader : Fader<CanvasGroup>
{
    protected override void FadeIn()
    {
        target.alpha = Mathf.Lerp(target.alpha, 1f, fSpeed * Time.deltaTime);
    }

    protected override void FadeOut()
    {
        target.alpha = Mathf.Lerp(target.alpha, 0f, fSpeed * Time.deltaTime);
    }

    protected override void FadeInInit()
    {
        target.enabled = true;
        target.alpha = 0;
    }

    protected override void FadeOutInit()
    {

    }

    protected override bool IsFadeInEnd()
    {
        return target.alpha >= 1 - threshold;
    }

    protected override bool IsFadeOutEnd()
    {
        return target.alpha <= threshold;
    }

    protected override void FadeInEnd()
    {
        target.alpha = 1;
    }

    protected override void FadeOutEnd()
    {
        target.alpha = 0;
    }
}
