using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

/// <summary>
/// SE音量設定のデータと見た目の橋渡しをする
/// </summary>
public class SeVolumeSettingWidgetPresenter : MonoBehaviour
{
    /// <summary>
    /// View
    /// </summary>
    [SerializeField] private SeVolumeSettingWidgetView _view;
    
    /// <summary>
    /// 音量設定のスライダー
    /// </summary>
    [SerializeField] private Slider _seVolumeSlider;

    /// <summary>
    /// 音量オン・オフボタン
    /// </summary>
    [SerializeField] private AudioSwitchButton _seSwitchButton;
    
    /// <summary>
    /// AudioManager
    /// </summary>
    [Inject] private AudioManager _audioManager;

    private void Start()
    {
        //スライダーを動かしたら、音量を変更する
        _seVolumeSlider
            .OnValueChangedAsObservable()
            .Subscribe(x => _audioManager.SetSoundEffectVolume(x/10))
            .AddTo(this);
        
        //音量が変更されたら、音量のテキスト表示を変更する
        _audioManager.SeMasterVolume
            .Subscribe(volume=> _view.SetSoundEffectVolumeText((int)Mathf.Round(volume*10)))
            .AddTo(this);
        
        //音量の音量オン・オフを切り替える
        _seSwitchButton
            .OnClickAsObservable
            .Subscribe(_ => _audioManager.SwitchPlaySeState())
            .AddTo(this.gameObject);

        //音量オン・オフに応じて、ボタンの見た目を切り替える
        _audioManager
            .IsPlaySe
            .Subscribe(_seSwitchButton.SetView)
            .AddTo(this.gameObject);
    }
}
