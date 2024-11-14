using Commons.Const;
using Commons.Save;
using Input;
using Manager.Command;
using Manager.Stage;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Widget.Result;
using Zenject;

/// <summary>
/// ゲームの進行を管理する
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 現在のゲームの状態
    /// </summary>
    public IReactiveProperty<GameEnum.State> CurrentStateProp => _currentState;
    private ReactiveProperty<GameEnum.State> _currentState = new ReactiveProperty<GameEnum.State>();

    /// <summary>
    /// InGameMoveCommandManager
    /// </summary>
    [SerializeField] private InGameCommandManager _commandManager;

    /// <summary>
    /// StageGenerator
    /// </summary>
    [SerializeField] private StageGenerator _stageGenerator;

    /// <summary>
    /// ResultWidgetController
    /// </summary>
    [SerializeField] private ResultWidgetController _resultWidget;

    /// <summary>
    /// CanvasFader
    /// </summary>
    [SerializeField] private CanvasFader _canvasFader;
  
    /// <summary>
    /// IInputEventProvider
    /// </summary>
    [Inject] private IInputEventProvider _inputEventProvider;
    
    /// <summary>
    /// SaveManager
    /// </summary>
    [Inject] private SaveManager _saveManager;
    
    /// <summary>
    /// AudioManager
    /// </summary>
    [Inject] private AudioManager _audioManager;
    
    private void Start()
    {
        //状態に応じて対応した各々のメソッドが呼ばれるｓ
        _currentState
            .DistinctUntilChanged()
            .Subscribe(OnStateChanged)
            .AddTo(this.gameObject);
        
        _inputEventProvider
            .IsReset
            .SkipLatestValueOnSubscribe()
            .Subscribe(_=>
            {
                _stageGenerator.Reset();
                _commandManager.Reset();
            })
            .AddTo(this.gameObject);
        
        //Quite
        //TODO:ここは変える必要がある
        //SceneManagerをつかわないリセット
        _inputEventProvider
            .IsQuit
            .SkipLatestValueOnSubscribe()
            .Subscribe(_=>SceneManager.LoadScene(SceneManager.GetActiveScene().name))
            .AddTo(this.gameObject);
        
        //フェードアウトしたら、ゲームスタート
        _inputEventProvider
            .IsGameStart
            .SkipLatestValueOnSubscribe()
            .Subscribe(_=>_currentState.Value = GameEnum.State.Ready)
            .AddTo(this.gameObject);
        
        StartCoroutine(_canvasFader.FadeOut());
        
        _audioManager.PlayBGM(BgmData.BGM.Battle);
    }
    
    /// <summary>
    /// 状態が変化した
    /// </summary>
    /// <param name="currentState">現在の状態</param>
    private void OnStateChanged(GameEnum.State currentState)
    {
        switch (currentState)
        {
            case GameEnum.State.Ready:
                Ready();
                break;
            case GameEnum.State.Finished:
                Finished();
                break;
            case GameEnum.State.Next:
                Next();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 準備
    /// </summary>
    private void Ready()
    {
        _commandManager.Initialize();

        //荷物を配達できたら、リザルトを表示する
        _commandManager
            .IsDelivered
            .Where(x=>x==true)
            .Subscribe(_=>_resultWidget.StartResult())
            .AddTo(this.gameObject);
        
        //リザルトを表示できたら、ゲーム終了
        _resultWidget.
            OnFinishResult
            .Subscribe(_ => _currentState.Value = GameEnum.State.Finished)
            .AddTo(this.gameObject);
        
        _stageGenerator.CreateStage();
    }
    
    /// <summary>
    /// ゲーム終了
    /// </summary>
    private void Finished()
    {
        _saveManager.Load();
        
        if (_saveManager.Data.CurrentStageNumber < Const.StagesMaxNumber)
        {
            _saveManager.SetCurrentStageNumber(_saveManager.Data.CurrentStageNumber+1);
        }

        //クリアしたゲームに応じて、次のステージを開放する
        if (_saveManager.Data.CurrentStageNumber > _saveManager.Data.MaxStageClearNumber &&
            _saveManager.Data.MaxStageClearNumber< Const.StagesMaxNumber)
        {
            _saveManager.Data.MaxStageClearNumber++;
        }

        _saveManager.Save();
        
        _currentState.Value = GameEnum.State.Next;
    }

    /// <summary>
    /// 次のステージへ
    /// </summary>
    private void Next()
    {
        _stageGenerator.NextStage();
        _commandManager.Reset();
    }
}
