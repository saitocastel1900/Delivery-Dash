using Input;
using UniRx;
using Zenject;
using UnityEngine;

/// <summary>
/// タイトル画面のUIを管理する
/// </summary>
public class TitleScreenWidget : MonoBehaviour
{
    /// <summary>
    /// CanvasFader
    /// </summary>
    [SerializeField] private CanvasFader _canvasFader;

    /// <summary>
    /// IInputEventProvider
    /// </summary>
    [Inject] private IInputEventProvider _inputEventProvider;

    private void Start()
    {
        //ゲームが始まったら、タイトル画面のUIを非表示する
        _inputEventProvider
            .IsGameStart
            .SkipLatestValueOnSubscribe()
            .Subscribe(_ => StartCoroutine(_canvasFader.FadeOut()))
            .AddTo(this.gameObject);
    }
}