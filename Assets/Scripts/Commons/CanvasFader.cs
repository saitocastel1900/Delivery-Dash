using System;
using System.Collections;
using DG.Tweening;
using UniRx;
using UnityEngine;

/// <summary>
/// CanvasGroupのフェードイン・フェードアウトを管理する
/// </summary>
public class CanvasFader : MonoBehaviour
{
    /// <summary>
    /// 表示たら呼ばれる
    /// </summary>
    public IObservable<Unit> OnShow => _showSubject;
    private Subject<Unit> _showSubject = new Subject<Unit>();
    
    /// <summary>
    /// 非表示したら呼ばれる
    /// </summary>
    public IObservable<Unit> OnHide => _hideSubject;
    private Subject<Unit> _hideSubject = new Subject<Unit>();
    
    /// <summary>
    /// CanvasGroup
    /// </summary>
    [SerializeField] private CanvasGroup _canvas;

    /// <summary>
    /// フェードイン・アウトする時間
    /// </summary>
    [SerializeField] private float _fadeDuration = 1.0f;

    /// <summary>
    /// フェードイン
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeIn()
    {
        _canvas.blocksRaycasts = true;
        yield return  AnimationUtility.FadeInCanvasGroupTween(_canvas, _fadeDuration).SetLink(this.gameObject).WaitForCompletion();;
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeOut()
    {
        _canvas.blocksRaycasts = false;
        yield return  AnimationUtility.FadeOutCanvasGroupTween(_canvas, _fadeDuration).SetLink(this.gameObject).WaitForCompletion();;
    }
}