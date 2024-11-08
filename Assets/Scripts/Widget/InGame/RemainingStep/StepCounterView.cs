using TMPro;
using UnityEngine;

/// <summary>
/// コマンド数の見た目を管理する
/// </summary>
public class StepCounterView : MonoBehaviour
{
    /// <summary>
    /// TextMeshProUGUI
    /// </summary>
    [SerializeField] private TextMeshProUGUI _text;

    /// <summary>
    /// コマンド数を設定
    /// </summary>
    /// <param name="number">設定したい番号</param>
    public void SetText(int number)
    {
        _text.text = number.ToString();
    }
}
