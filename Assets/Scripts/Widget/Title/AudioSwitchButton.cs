using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 音量オン・オフボタンを管理する
/// </summary>
public class AudioSwitchButton : MonoBehaviour
{
    /// <summary>
    /// ボタンが押されたら呼ばれる
    /// </summary>
    public IObservable<Unit> OnClickAsObservable => _button.OnClickAsObservable();
    
    /// <summary>
    /// Button
    /// </summary>
    [SerializeField] private Button _button;
    
    /// <summary>
    /// 再生ボタン画像
    /// </summary>
    [SerializeField] private Sprite _playButtonSprite;
    
    /// <summary>
    /// 再生ボタン押し込み画像
    /// </summary>
    [SerializeField] private Sprite _playButtonPressedSprite;
    
    /// <summary>
    /// 消音ボタン画像
    /// </summary>
    [SerializeField] private Sprite _muteButtonSprite;
    
    /// <summary>
    /// 消音ボタン押し込み画像
    /// </summary>
    [SerializeField] private Sprite _muteButtonPressedSprite;

    /// <summary>
    /// 見た目を設定
    /// </summary>
    /// <param name="isPlay">音量再生しているか</param>
    public void SetView(bool isPlay)
    {
        if (isPlay)
        {
            var state = _button.spriteState;
            state.pressedSprite = _playButtonPressedSprite;
            _button.image.sprite = _playButtonSprite;
            _button.spriteState = state;
        }
        else
        {
            var state = _button.spriteState;
            state.pressedSprite = _muteButtonPressedSprite;
            _button.image.sprite = _muteButtonSprite;
            _button.spriteState = state;
        }
    }
}