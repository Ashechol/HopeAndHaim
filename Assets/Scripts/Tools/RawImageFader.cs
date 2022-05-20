using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 持有 RawImage 设置透明度以实现淡入淡出
/// </summary>
public class RawImageFader : Fader<RawImage>
{
    protected override void FadeInInit()
    {
        target.enabled = true;
        target.color = Color.black;
    }

    protected override void FadeOutInit()
    {
        target.enabled = true;
        target.color = Color.clear;
    }

    //淡入：渐渐透明
    protected override void FadeIn()
    {
        target.color = Color.Lerp(target.color, Color.clear, fSpeed * Time.deltaTime);
    }

    //淡出：渐渐变黑
    protected override void FadeOut()
    {
        target.color = Color.Lerp(target.color, Color.black, fSpeed * Time.deltaTime);
    }

    protected override bool IsFadeInEnd()
    {
        return target.color.a <= threshold;
    }

    protected override bool IsFadeOutEnd()
    {
        return target.color.a >= 1 - threshold;
    }

    protected override void FadeInEnd()
    {
        target.color = Color.clear;
        target.enabled = false;
    }

    protected override void FadeOutEnd()
    {
        target.color = Color.black;
    }
}
