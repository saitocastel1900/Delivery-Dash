using TMPro;
using UnityEngine;

/// <summary>
/// BGM音量設定の見た目を管理する
/// </summary>
public class BgmVolumeSettingWidgetView : MonoBehaviour
{
    /// <summary>
    /// TextMeshProUGUI
    /// </summary>
    [SerializeField] private TextMeshProUGUI _bgmVolumeText;
   
    /// <summary>
    /// BGMの音量を設定する
    /// </summary>
    /// <param name="volume">設定する音量</param>
    public void SetBGMVolumeText(int volume)
    {
        _bgmVolumeText.text = volume.ToString();
    }
}
