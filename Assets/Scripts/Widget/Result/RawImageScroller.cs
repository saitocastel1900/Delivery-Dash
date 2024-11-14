using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// RawImageをスクロールを管理する
/// </summary>
public class RawImageScroller : MonoBehaviour
{
    /// <summary>
    /// RawImage
    /// </summary>
    [SerializeField] RawImage _backgroundRawImage;
    
    /// <summary>
    /// X軸のスクロールスピード
    /// </summary>
    public float ScrollSpeedX
    {
        get => _scrollSpeedX;
        set => _scrollSpeedX = value;
    }
    [SerializeField] private float _scrollSpeedX = 0.04f;
    
    /// <summary>
    /// Y軸のスクロールスピード
    /// </summary>
    [SerializeField] private float _scrollSpeedY = 0.0f;

    private void Start()
    {
        Observable
            .EveryUpdate()
            .Subscribe(_ => Scroll())
            .AddTo(this);
    }

    /// <summary>
    /// スクロール
    /// </summary>
    private void Scroll()
    {
        var rect = _backgroundRawImage.uvRect;
        rect.x = Mathf.Repeat(rect.x + _scrollSpeedX * Time.unscaledDeltaTime, 1f);
        rect.y = Mathf.Repeat(rect.y + _scrollSpeedY * Time.unscaledDeltaTime, 1f);
        _backgroundRawImage.uvRect = rect;
    }
}