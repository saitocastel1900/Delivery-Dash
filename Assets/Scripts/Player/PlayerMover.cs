using System;
using Input;
using Manager.Command;
using UniRx;
using UnityEngine;
using Zenject;

namespace Player
{
    /// <summary>
    /// プレイヤーの移動を管理する
    /// </summary>
    public class PlayerMover : MonoBehaviour, IReceiver
    {
        /// <summary>
        /// 移動したら呼ばれる
        /// </summary>
        public IObservable<Unit> OnMove => _moveSubject;
        private Subject<Unit> _moveSubject = new Subject<Unit>();

        /// <summary>
        /// Input
        /// </summary>
        [Inject] protected IInputEventProvider _input;
        
        private void Start()
        {
            //入力に応じて、移動する
            _input
                .MoveDirection
                .Subscribe()
                .AddTo(this.gameObject);
        }

        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="direction">移動方向</param>
        public void Move(Vector3 direction)
        {
            transform.position += direction;
            _moveSubject.OnNext(Unit.Default);
        }
    }
}