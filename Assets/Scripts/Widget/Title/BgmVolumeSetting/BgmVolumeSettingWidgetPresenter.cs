using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

/// <summary>
/// BGM音量設定のデータと見た目の橋渡しをする
/// </summary>
public class BgmVolumeSettingWidgetPresenter : MonoBehaviour
{
    /// <summary>
    /// View
    /// </summary>
    [SerializeField] private BgmVolumeSettingWidgetView _view;
    
    /// <summary>
    /// 音量設定のスライダー
    /// </summary>
    [SerializeField] private Slider _bgmVolumeSlider;

    /// <summary>
    /// AudioManager
    /// </summary>
    [Inject] private AudioManager _audioManager;

    private void Start()
    {
        //スライダーを動かしたら、音量を変更する
        _bgmVolumeSlider
            .OnValueChangedAsObservable()
            .Subscribe(x => _audioManager.SetBgmVolume(x/10))
            .AddTo(this);
        
        //音量が変更されたら、音量のテキスト表示を変更する
        _audioManager.BgmMasterVolume
            .Subscribe(volume=> _view.SetBGMVolumeText((int)Mathf.Round(volume*10)))
            .AddTo(this);
    }
}
