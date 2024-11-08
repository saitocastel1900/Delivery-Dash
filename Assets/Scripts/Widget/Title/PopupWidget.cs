using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ポップアップを管理する
/// </summary>
public class PopupWidget : MonoBehaviour
{
    /// <summary>
    /// CanvasFader
    /// </summary>
    [SerializeField] private CanvasFader _canvasFader;  
    
    /// <summary>
    /// 表示させるボタン
    /// </summary>
    [SerializeField] private Button _openButton;
    
    /// <summary>
    /// 非表示にするボタン
    /// </summary>
    [SerializeField] private Button _closeButton;
    
    private void Start()
    {
        //ボタンに応じて表示・非表示を行う
        _openButton
            .OnClickAsObservable()
            .Subscribe(_=> StartCoroutine(_canvasFader.FadeIn()))
            .AddTo(this.gameObject);
        
        _closeButton
            .OnClickAsObservable()
            .Subscribe(_=> StartCoroutine(_canvasFader.FadeOut()))
            .AddTo(this.gameObject);
    }
}
