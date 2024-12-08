using UniRx;
using UnityEngine;
using Zenject;

namespace Player
{
   /// <summary>
   /// Playerの音を管理する
   /// </summary>
   public class PlayerAudio : MonoBehaviour
   {
      /// <summary>
      /// PlayerMover
      /// </summary>
      [SerializeField] private PlayerMover _mover;

      /// <summary>
      /// PlayerAnimator
      /// </summary>
      [SerializeField] private PlayerAnimator _animator;

      /// <summary>
      /// AudioManager
      /// </summary>
      [Inject] private AudioManager _audioManager;

      private void Start()
      {
         //移動したら、音を鳴らす
         _mover
            .OnMove
            .Subscribe(_ => _audioManager.PlaySoundEffect(SoundEffectData.SoundEffect.MovePlayer))
            .AddTo(this.gameObject);

         //登場したら、音を鳴らす
         _animator
            .OnAppear
            .Subscribe(_ => _audioManager.PlaySoundEffect(SoundEffectData.SoundEffect.Appeared))
            .AddTo(this.gameObject);
      }
   }
}