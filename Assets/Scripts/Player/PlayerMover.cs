using System;
using Manager.Command;
using UniRx;
using UnityEngine;
using DG.Tweening;

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
        public IObservable<Vector3> OnMove => _moveSubject;
        private Subject<Vector3> _moveSubject = new Subject<Vector3>();
     
        /// <summary>
        /// Transform
        /// </summary>
        [SerializeField] private Transform _transform;

        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="direction">移動方向</param>
        /// <param name="isUndo">Undoで実行したか</param>
        public void Move(Vector3 direction,bool isUndo = false)
        {
            //Undoのときは、アニメーションをしないで動かす
            if (isUndo)
            {
                _transform.position += direction;
                _transform.forward = direction;
            }
            else
            {
                _moveSubject.OnNext(direction);   
            }
        }
    }
}