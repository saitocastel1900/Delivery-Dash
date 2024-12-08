using System;
using System.Collections;
using Commons.Save;
using DG.Tweening;
using Input;
using UnityEngine;
using UniRx;
using Zenject;

namespace Player
{
    /// <summary>
    /// Playerのアニメーションを管理する
    /// </summary>
    public class PlayerAnimator : MonoBehaviour
    {
        /// <summary>
        /// 登場したら呼ばれる
        /// </summary>
        public IObservable<Unit> OnAppear => _appearSubject;
        private Subject<Unit> _appearSubject = new Subject<Unit>();
        
        /// <summary>
        /// PlayerMover
        /// </summary>
        [SerializeField] private PlayerMover _mover;
        
        /// <summary>
        /// Transform
        /// </summary>
        [SerializeField] private Transform _transform;
        
        /// <summary>
        /// 回転アニメーション時間
        /// </summary>
        [SerializeField] private float _moveDuration = 0.35f;
        
        /// <summary>
        /// 登場アニメーション時間
        /// </summary>
        [SerializeField] private float _appearDuration = 0.35f;
        
        /// <summary>
        /// 登場アニメーション待ち時間
        /// </summary>
        [SerializeField] private float _appearWaitTime = 0.5f;
        
        /// <summary>
        /// SaveManager
        /// </summary>
        [Inject] private SaveManager _saveManager;
        
        /// <summary>
        /// IInputEventProvider
        /// </summary>
        [Inject] IInputEventProvider _inputEventProvider;

        private void Start()
        {
            //登場アニメーションを流す
            if (_inputEventProvider.IsPlayGame.Value)
            {
                StartCoroutine(AppearAnimation());
            }
            
            _inputEventProvider
                .IsPlayGame
                .SkipLatestValueOnSubscribe()
                .Where(x => x) 
                .Where(_=>_saveManager.Data.CurrentStageNumber <=_saveManager.Data.MaxStageClearNumber)
                .Subscribe(_ => StartCoroutine(AppearAnimation()))
                .AddTo(this);
            
            //移動したときに回転する
            _mover
                .OnMove
                .Subscribe(MoveAnimation)
                .AddTo(this.gameObject);
        }

        /// <summary>
        /// 回転アニメーション
        /// </summary>
        /// <param name="direction">進行方向</param>
        private void MoveAnimation(Vector3 direction)
        {
            Quaternion targetRotation = Quaternion.LookRotation(-direction);
            
            //移動アニメーション
            _transform
                .DOMove(direction, _moveDuration)
                .SetRelative(true)
                .SetEase(Ease.OutQuart)
                .SetLink(this.gameObject);
            
            //回転アニメーション
            _transform
                .DORotateQuaternion(targetRotation, _moveDuration)
                .SetEase(Ease.OutQuart)
                .SetLink(this.gameObject);
        }

        /// <summary>
        /// 登場アニメーション
        /// </summary>
        private IEnumerator AppearAnimation()
        {
            yield return new WaitForSeconds(_appearWaitTime);

            _appearSubject.OnNext(Unit.Default);
            
            _transform
                .DOJump(_transform.position, 0.5f, 1, _appearDuration)
                .SetEase(Ease.OutQuart)
                .SetLink(this.gameObject);
        }
    }
}