using DG.Tweening;
using Input;
using UniRx;
using UnityEngine;
using Zenject;

public class InGameScreenWidget : MonoBehaviour
{
    /// <summary>
    /// Canvas
    /// </summary>
    [SerializeField] private CanvasGroup _canvas;

    /// <summary>
    /// 表示する時間
    /// </summary>
    [SerializeField] private float _fadeInDuration = 1.0f;
    
    /// <summary>
    /// 
    /// </summary>
    [Inject] private IInputEventProvider _inputEventProvider;
    
    private void Start()
    {
        _inputEventProvider
            .IsPlayGame
            .SkipLatestValueOnSubscribe()
            .Subscribe(_ =>
            {
                _canvas.blocksRaycasts = true;
                AnimationUtility.FadeInCanvasGroupTween(_canvas, _fadeInDuration).SetLink(this.gameObject);
            })
            .AddTo(this.gameObject);
    }
}
