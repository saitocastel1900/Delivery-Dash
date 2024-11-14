using Commons.Save;
using DG.Tweening;
using Input;
using UniRx;
using UnityEngine;
using Zenject;

/// <summary>
/// ゲーム画面のUIを管理する
/// </summary>
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
    /// IInputEventProvider
    /// </summary>
    [Inject] private IInputEventProvider _inputEventProvider;

    /// <summary>
    /// SaveManager
    /// </summary>
    [Inject] private SaveManager _saveManager;
    
    private void Start()
    {
        _inputEventProvider
            .IsPlayGame
            .SkipLatestValueOnSubscribe()
            .Where(_=>_saveManager.Data.CurrentStageNumber<=_saveManager.Data.MaxStageClearNumber)
            .Subscribe(_ =>
            {
                _canvas.blocksRaycasts = true;
                AnimationUtility.FadeInCanvasGroupTween(_canvas, _fadeInDuration).SetLink(this.gameObject);
            })
            .AddTo(this.gameObject);
    }
}
