using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージ選択アイコンの見た目を管理する
/// </summary>
public class StageSelectIconWidget : MonoBehaviour
{
    /// <summary>
    /// ステージ進行中のImage
    /// </summary>
    [SerializeField] private Image _unopenedCardboardBoxImage;
    
    /// <summary>
    /// ステージクリアのImage
    /// </summary>
    [SerializeField] private Image _openedCardboardBoxImage;
    
    /// <summary>
    /// ステージ未開放のImage
    /// </summary>
    [SerializeField] private Image _lockImage;

    /// <summary>
    /// 状態を設定
    /// </summary>
    /// <param name="state">現在の状況</param>
    public void SetView(StageProgressState state)
    {
        // 状態によって表示を変更
        switch (state)
        {
            case StageProgressState.Locked:
                _unopenedCardboardBoxImage.gameObject.SetActive(true);
                _openedCardboardBoxImage.gameObject.SetActive(false);
                _lockImage.gameObject.SetActive(true);
                break;
            case StageProgressState.InProgress:
                _unopenedCardboardBoxImage.gameObject.SetActive(true);
                _openedCardboardBoxImage.gameObject.SetActive(false);
                _lockImage.gameObject.SetActive(false);
                break;
            case StageProgressState.Completed:
                _unopenedCardboardBoxImage.gameObject.SetActive(false);
                _openedCardboardBoxImage.gameObject.SetActive(true);
                _lockImage.gameObject.SetActive(false);
                break;
        }
    }
}