using UnityEngine;

namespace Manager.Command
{
    /// <summary>
    /// 実行される命令を管理する
    /// </summary>
    public interface IReceiver
    {
        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="direction">移動方向</param>
        public void Move(Vector3 direction);
    }
}