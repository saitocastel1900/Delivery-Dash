using UniRx;
using UnityEngine;

/// <summary>
/// 音Manager
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// AudioManagerComponent
    /// </summary>
    [SerializeField] private AudioManagerComponent _component;
    
    /// <summary>
    /// BGMの音量
    /// </summary>
    public IReactiveProperty<float> BgmMasterVolume => _component.BgmMasterVolume;
    
    /// <summary>
    /// SEの音量
    /// </summary>
    public IReactiveProperty<float> SeMasterVolume => _component.SeMasterVolume;

    /// <summary>
    /// BGMを流す
    /// </summary>
    /// <param name="bgm">流したいBGM</param>
    public void PlayBGM(BgmData.BGM bgm)
    {
        _component.PlayBGM(bgm);
    }

    /// <summary>
    /// SEを流す
    /// </summary>
    /// <param name="soundEffect">流したいSE</param>
    public void PlaySoundEffect(SoundEffectData.SoundEffect soundEffect)
    {
        _component.PlaySoundEffect(soundEffect);
    }
    
    /// <summary>
    /// BGMの音量を設定する
    /// </summary>
    /// <param name="volume">設定する音量</param>
    public void SetBgmVolume(float volume)
    {
        _component.SetBgmVolume(volume); ;
    }
    
    /// <summary>
    ///　SEの音量を設定する 
    /// </summary>
    /// <param name="volume">設定する音量</param>
    public void SetSoundEffectVolume(float volume)
    {
        _component.SetSoundEffectVolume(volume);
    }
}