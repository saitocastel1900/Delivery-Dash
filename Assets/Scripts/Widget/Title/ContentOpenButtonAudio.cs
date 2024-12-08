using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

/// <summary>
/// コンテンツを開くボタンの音を管理する
/// </summary>
public class ContentOpenButtonAudio : MonoBehaviour
{
   /// <summary>
   /// Button
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
         .Subscribe(_=>_audioManager.PlaySoundEffect(SoundEffectData.SoundEffect.OpenContent))
         .AddTo(this.gameObject);
   }
}
