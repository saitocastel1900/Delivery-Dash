using UniRx;
using UnityEngine;

namespace Input
{
    /// <summary>
    /// 入力を管理する
    /// </summary>
    public interface IInputEventProvider
    {
        /// <summary>
        /// ゲームスタートが押されたか
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsGameStart { get; }
        
        /// <summary>
        /// 進行方向
        /// </summary>
        public IReadOnlyReactiveProperty<Vector3> MoveDirection { get; }
        
        /// <summary>
        /// 右移動が押されたか
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsRight { get; }
        
        /// <summary>
        /// 左移動が押されたか
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsLeft { get; }
        
        /// <summary>
        /// シーン遷移が押されたか
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsPlayGame { get; }
        
        /// <summary>
        /// Undoが押されたか
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsUndo { get; }

        /// <summary>
        /// Resetが押されたか
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsReset { get; }

        /// <summary>
        /// Quitが押されたか
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsQuit { get; }
    }
}