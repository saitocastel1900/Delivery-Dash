using UniRx;
using UnityEngine;
using Widget.Result;

/// <summary>
/// リザルトのカットインを管理する
/// </summary>
public class ResultCutinWidget : MonoBehaviour
{
    /// <summary>
    /// CanvasFader
    /// </summary>
    [SerializeField] private CanvasFader _canvasFader;

    /// <summary>
    /// ResultWidgetController
    /// </summary>
    [SerializeField] private ResultWidgetController _resultDialogWidgetController;

    private void Start()
    {
        //ゲームをクリアしたら、カットインを表示する
        _resultDialogWidgetController
            .OnFinishResult
            .Subscribe(_ => StartCoroutine(_canvasFader.FadeOut()))
            .AddTo(this.gameObject);

        //カットインのアニメーションが終わったら、非表示にする
        _resultDialogWidgetController
            .OnShowResultWidgetAsObservable
            .Subscribe(_ => StartCoroutine(_canvasFader.FadeIn()))
            .AddTo(this.gameObject);
    }
}