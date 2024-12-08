using Commons.Save;
using Input;
using UniRx;
using Zenject;
using UnityEngine;

/// <summary>
/// ステージセレクト画面のUIを管理する
/// </summary>
public class StageSelectScreenWidget : MonoBehaviour
{
    /// <summary>
    /// CanvasFader
    /// </summary>
    [SerializeField] private CanvasFader _canvasFader;

    /// <summary>
    /// IInputEventProvider
    /// </summary>
    [Inject] private IInputEventProvider _inputEventProvider;

    /// <summary>
    /// SaveManager
    /// </summary>
    [Inject] private SaveManager _saveManager;
    
    /// <summary>
    /// AudioManager
    /// </summary>
    [Inject] private AudioManager _audioManager;

    private void Start()
    {
        //ゲームが始まったら、ステージセレクト画面のUIを表示する
        _inputEventProvider
            .IsGameStart
            .SkipLatestValueOnSubscribe()
            .Subscribe(_ => StartCoroutine(_canvasFader.FadeIn()))
            .AddTo(this.gameObject);

        //ステージを選択したら、ステージセレクト画面のUIを非表示にする
        _inputEventProvider
            .IsPlayGame
            .SkipLatestValueOnSubscribe()
            .Where(_=>_saveManager.Data.CurrentStageNumber <=_saveManager.Data.MaxStageClearNumber)
            .Subscribe(_ =>
            {
                StartCoroutine(_canvasFader.FadeOut());
                _audioManager.PlaySoundEffect(SoundEffectData.SoundEffect.DeterminedStage);
            })
            .AddTo(this.gameObject);
        
        //遊べないステージを選択したら、エラー音を再生する
        _inputEventProvider
            .IsPlayGame
            .SkipLatestValueOnSubscribe()
            .Where(_=>!(_saveManager.Data.CurrentStageNumber <=_saveManager.Data.MaxStageClearNumber))
            .Subscribe(_ => _audioManager.PlaySoundEffect(SoundEffectData.SoundEffect.CannotSelectStage))
            .AddTo(this.gameObject);
    }
}