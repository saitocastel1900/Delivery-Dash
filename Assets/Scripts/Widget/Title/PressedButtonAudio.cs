using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PressedButtonAudio : MonoBehaviour
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
