using Zenject;
using UnityEngine;
using UniRx;

namespace Block
{
    /// <summary>
    /// ブロックの音を管理する
    /// </summary>
    public class BlockAudio : MonoBehaviour
    {
        /// <summary>
        /// BlockMover
        /// </summary>
        [SerializeField] private BlockMover _mover;

        /// <summary>
        /// AudioManager
        /// </summary>
        [Inject] private AudioManager _audioManager;

        private void Start()
        {
            //移動したら、音を流す
            _mover
                .OnMove
                .Subscribe(_ => _audioManager.PlaySoundEffect(SoundEffectData.SoundEffect.MoveBlock))
                .AddTo(this.gameObject);
        }
    }
}