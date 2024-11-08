using UniRx;
using System.Collections.Generic;
using Commons.Const;
using Commons.Save;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Widget.Result;
using Zenject;

/// <summary>
/// ステージクリアと次のステージ番号の表示を管理する
/// </summary>
public class StageClearAndNextStageWidget : MonoBehaviour
{
    /// <summary>
    /// Text
    /// </summary>
    [SerializeField] private Text _text;

    /// <summary>
    /// ResultWidgetController
    /// </summary>
    [SerializeField] ResultWidgetController _resultWidgetController;
    
    /// <summary>
    /// 表示するメッセージ
    /// </summary>
    private List<string> _message = new List<string>()
    {
        "Completed!",
        "Next.. "
    };

    /// <summary>
    /// AudioManager
    /// </summary>
    [Inject] private AudioManager _audioManager;

    /// <summary>
    /// SaveManager
    /// </summary>
    [Inject] private SaveManager _saveManager;

    private void Start()
    {
        _resultWidgetController
            .OnShowResultWidgetAsObservable
            .Subscribe(_ =>PlayAnimation());
    }

    /// <summary>
    /// リザルトアニメーション
    /// </summary>
    private void PlayAnimation()
    {
        //TODO:効果音を追加しよう
        DOTween.Sequence()
            //.AppendCallback(()=>_audioManager.PlaySoundEffect(SoundEffect.Result1))
            .Append(_text.DOText(_message[0], 0.5f, scrambleMode: ScrambleMode.None).SetEase(Ease.Linear))
            .Append(_text.DOFade(0f, 0.1f).SetEase(Ease.Linear))
            .Append(_text.DOFade(1f, 0.1f).SetEase(Ease.Linear))
            //.AppendCallback(()=>_audioManager.PlaySoundEffect(SoundEffect.Result2))
            .AppendInterval(0.5f)
            .Append(_text.transform.DORotate(new Vector3(90, 0, 0), 0.2f).SetEase(Ease.Linear))
            //.AppendCallback(()=>_audioManager.PlaySoundEffect(SoundEffect.Result3))
            .Append(_text
                .DOText(_message[1] + _saveManager.Data.CurrentStageNumber + "/" + Const.StagesMaxNumber, 0.0f,
                    scrambleMode: ScrambleMode.None).SetEase(Ease.Linear)).SetLink(this.gameObject)
            .Append(_text.transform.DORotate(new Vector3(0, 0, -8), 0.2f).SetEase(Ease.Linear))
            .AppendInterval(0.25f)
            .OnComplete(() => _resultWidgetController.FinishStageClearAndNextStageAnimation());
    }
}