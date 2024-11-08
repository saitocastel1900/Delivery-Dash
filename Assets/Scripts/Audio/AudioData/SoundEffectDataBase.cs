using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// SEを管理する
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/SoundEffectDataBase")]
public class SoundEffectDataBase : ScriptableObject
{
    /// <summary>
    /// SEを管理するリスト
    /// </summary>
    [SerializeField]
    List<SoundEffectData> _SoundEffectDataList = new List<SoundEffectData>();

    /// <summary>
    /// SEを取得する
    /// </summary>
    /// <param name="se">取得したいS</param>
    /// <returns></returns>
    public SoundEffectData GetSoundEffectData(SoundEffectData.SoundEffect se)
    {
        var soundEffectData = _SoundEffectDataList.FirstOrDefault(clip => clip.se == se);
        return soundEffectData;
    }
}
