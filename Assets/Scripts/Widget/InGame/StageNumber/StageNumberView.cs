using TMPro;
using UnityEngine;

namespace Widget.InGame.StageNumber
{
    /// <summary>
    /// ステージ番号の見た目を管理する
    /// </summary>
    public class StageNumberView : MonoBehaviour
    {
        /// <summary>
        /// TextMeshProUGUI
        /// </summary>
        [SerializeField] private TextMeshProUGUI _text;

        /// <summary>
        /// ステージ番号を設定
        /// </summary>
        /// <param name="number">設定したい番号</param>
        public void SetText(int number)
        {
            _text.text = number.ToString();
        }
    }
}