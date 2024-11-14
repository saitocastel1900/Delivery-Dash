using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// AnimationのUtility
/// </summary>
public static class AnimationUtility
{
    /// <summary>
    /// フェードアウトのTween
    /// </summary>
    public static Tween FadeOutImageTween(Image image,float duration= 2.5f)
    {
        return image.DOFade(0f, duration).SetEase(Ease.Linear);
    }

    /// <summary>
    /// フェードインのTween
    /// </summary>
    public static Tween FadeInCanvasGroupTween(CanvasGroup canvas,float duration= 2.5f)
    {
        return canvas.DOFade(1.0f, duration)
            .SetEase(Ease.Linear);
    }
    
    /// <summary>
    /// フェードインのTween
    /// </summary>
    public static Tween FadeOutCanvasGroupTween(CanvasGroup canvas,float duration= 2.5f)
    {
        return canvas.DOFade(0.0f, duration)
            .SetEase(Ease.Linear);
    }

    /// <summary>
    /// フェードインを繰り返すTween
    /// </summary>
    public static Tween FadeImageLoopTween(Image image)
    {
        return image.DOFade(0.0f, 1f)
            .SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
    
    /// <summary>
    /// 画像のY座標をアニメーションで移動するTween
    /// </summary>
    public static Tween MoveImageY(Image image, float endY, float duration = 0.5f)
    {
        return image.rectTransform.DOAnchorPosY(endY, duration)
            .SetEase(Ease.OutCubic);
    }
}