using System;
using Manager.Command;
using UniRx;
using Zenject;

/// <summary>
/// コマンド数のデータと見た目の橋渡しをする
/// </summary>
public class StepCounterPresenter : IDisposable, IInitializable
{
    /// <summary>
    /// Model
    /// </summary>
    private InGameCommandManager _inGameCommandManager;

    /// <summary>
    /// View
    /// </summary>
    private StepCounterView _view;
    
    /// <summary>
    /// Disposable
    /// </summary>
    private CompositeDisposable _compositeDisposable;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public StepCounterPresenter(StepCounterView view,InGameCommandManager inGameCommandManager)
    {
        _view = view;
        _inGameCommandManager = inGameCommandManager;
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        _compositeDisposable = new CompositeDisposable();
        Bind();
    }

    /// <summary>
    /// Bind
    /// </summary>
    private void Bind()
    {
        //移動したら、コマンド数を設定する
        _inGameCommandManager
            .BlockMovedList
            .ObserveAdd()
            .Select(x=>_inGameCommandManager.BlockMovedList.Count)
            .Subscribe(_view.SetText)
            .AddTo(_compositeDisposable);
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        _compositeDisposable.Dispose();
    }
}