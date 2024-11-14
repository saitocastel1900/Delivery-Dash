using TMPro;
using UnityEngine;

/// <summary>
/// SE音量設定の見た目を管理する
/// </summary>
public class SeVolumeSettingWidgetView : MonoBehaviour
{
    /// <summary>
    /// TextMeshProUGUI
    /// </summary>
    [SerializeField] private TextMeshProUGUI _seVolumeText;
   
    /// <summary>
    /// BGMの音量を設定する
    /// </summary>
    /// <param name="volume">設定する音量</param>
    public void SetSoundEffectVolumeText(int volume)
    {
        _seVolumeText.text = volume.ToString();
    }
}
