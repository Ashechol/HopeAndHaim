using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 图片淡入淡出，淡入是让图片逐渐清晰
/// </summary>
public class ImageFader : Fader<Image>
{
    public Color targetColor = Color.white;

    //淡入：渐渐变白
    protected override void FadeIn()
    {
        target.color = Color.Lerp(target.color, targetColor, fSpeed * Time.deltaTime);
    }

    //淡出：渐渐透明
    protected override void FadeOut()
    {
        target.color = Color.Lerp(target.color, Color.clear, fSpeed * Time.deltaTime);
    }

    protected override void FadeInInit()
    {
        target.enabled = true;
        target.color = Color.clear;
    }

    protected override void FadeOutInit()
    {

    }

    protected override bool IsFadeInEnd()
    {
        return target.color.a >= targetColor.a - threshold;
    }

    protected override bool IsFadeOutEnd()
    {
        return target.color.a <= threshold;
    }

    protected override void FadeInEnd()
    {
        target.color = targetColor;
    }

    protected override void FadeOutEnd()
    {
        target.color = Color.clear;
        target.enabled = false;
    }
}
