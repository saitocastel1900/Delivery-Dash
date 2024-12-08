using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Block
{
    public class BlockAnimator : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private BlockMover _mover;
        
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private Transform _transform;
        
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private float _duration = 0.5f;

        private void Start()
        {
            _mover
                .OnMove
                .Subscribe(PlayAnimation)
                .AddTo(this.gameObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        private void PlayAnimation(Vector3 direction)
        {
            Quaternion targetRotation = Quaternion.LookRotation(-direction);

            //移動アニメーション
            _transform
                .DOMove(direction, _duration)
                .SetRelative(true)
                .SetEase(Ease.OutQuart)
                .SetLink(this.gameObject);
        }
    }
}