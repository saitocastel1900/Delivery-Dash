using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

/// <summary>
/// コンテンツを閉じるボタンの音を管理する
/// </summary>
public class ContentCloseButtonAudio : MonoBehaviour
{
    /// <summary>
    ///Button
    /// </summary>
    [SerializeField] private Button _button;

    /// <summary>
    /// AudioManager
    /// </summary>
    [Inject] private AudioManager _audioManager;
   
    private void Start()
    {
        _button
            .OnClickAsObservable()
            .Subscribe(_=>_audioManager.PlaySoundEffect(SoundEffectData.SoundEffect.CloseContent))
            .AddTo(this.gameObject);
    }
}
