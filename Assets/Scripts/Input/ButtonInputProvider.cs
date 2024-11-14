using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Input
{
    /// <summary>
    /// ボタン入力を管理する
    /// </summary>
    public class ButtonInputProvider : IInputEventProvider, IInitializable, IDisposable
    {
        /// <summary>
        /// ゲームスタートが押されたか
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsGameStart => _isGameStart;
        private ReactiveProperty<bool> _isGameStart = new ReactiveProperty<bool>(false);

        /// <summary>
        /// 進行方向
        /// </summary>
        public IReadOnlyReactiveProperty<Vector3> MoveDirection => _moveDirection;
        private ReactiveProperty<Vector3> _moveDirection = new ReactiveProperty<Vector3>();
        
        /// <summary>
        /// 右移動が押されたか
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsRight => _isRight;
        private ReactiveProperty<bool> _isRight = new ReactiveProperty<bool>(false);

        /// <summary>
        /// 左移動が押されたか
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsLeft => _isLeft;
        private ReactiveProperty<bool> _isLeft = new ReactiveProperty<bool>(false);

        /// <summary>
        /// シーン遷移が押されたか
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsPlayGame => _isPlayGame;
        private ReactiveProperty<bool> _isPlayGame = new ReactiveProperty<bool>(false);
        
        /// <summary>
        /// Undoが押されたか
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsUndo => _isUndo;
        private ReactiveProperty<bool> _isUndo = new ReactiveProperty<bool>(false);
        
        /// <summary>
        /// Resetが押されたか
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsReset => _isReset;
        private ReactiveProperty<bool> _isReset = new ReactiveProperty<bool>(false);
        
        /// <summary>
        /// Quitが押されたか
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsQuit => _isQuit;
        private ReactiveProperty<bool> _isQuit = new ReactiveProperty<bool>(false);

        /// <summary>
        /// ゲームを開始するボタン
        /// </summary>
        private Button _gameStartButton;
        
        /// <summary>
        /// ステージセレクトを左に移動するボタン
        /// </summary>
        private Button _stageSelectLeftButton;
        
        /// <summary>
        /// ステージセレクトを右に移動するボタン
        /// </summary>
        private Button _stageSelectRightButton;
        
        /// <summary>
        /// 前に移動するボタン
        /// </summary>
        private Button _gameForwardButton;
        
        /// <summary>
        /// 右に移動するボタン
        /// </summary>
        private Button _gameRightButton;
        
        /// <summary>
        /// 左に移動するボタン
        /// </summary>
        private Button _gameLeftButton;
        
        /// <summary>
        /// 後進するボタン
        /// </summary>
        private Button _gameBackButton;
        
        /// <summary>
        /// ステージ攻略を開始するボタン
        /// </summary>
        private Button _playGameButton;
        
        /// <summary>
        /// 移動を取り消しボタン
        /// </summary>
        private Button _undoButton;
        
        /// <summary>
        /// リセットするボタン
        /// </summary>
        private Button _resetButton;
        
        /// <summary>
        /// ゲームをやめるボタン
        /// </summary>
        private Button _quitButton;
        
        /// <summary>
        /// CompositeDisposable
        /// </summary>
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="buttons">ボタン</param>
        public ButtonInputProvider(Button[] buttons)
        {
            _gameStartButton = buttons[0];
            _stageSelectLeftButton = buttons[1];
            _stageSelectRightButton = buttons[2];
            _gameForwardButton = buttons[3];
            _gameRightButton = buttons[4];
            _gameLeftButton = buttons[5];
            _gameBackButton = buttons[6];
            _playGameButton = buttons[7];
            _undoButton = buttons[8];
            _resetButton = buttons[9];
            _quitButton = buttons[10];
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {   
            //ボタンが押されたら、進行方向を設定する
            Observable.Merge(
                    _gameForwardButton.OnClickAsObservable()
                        .Select(_ => Vector3.forward),
                    _gameRightButton.OnClickAsObservable()
                        .Select(_ => Vector3.right),
                    _gameLeftButton.OnClickAsObservable()
                        .Select(_ => Vector3.left),
                    _gameBackButton.OnClickAsObservable()
                        .Select(_ => Vector3.back)
                )
                .Subscribe(value => _moveDirection.SetValueAndForceNotify(value))
                .AddTo(_compositeDisposable);
            
            //ボタンに応じて、フラグを立てる
            _gameStartButton
                .OnClickAsObservable()
                .Select(x => true)
                .Subscribe(value => _isGameStart.Value = value)
                .AddTo(_compositeDisposable);
            
            _stageSelectRightButton
                .OnClickAsObservable()
                .Select(x => true)
                .Subscribe(_isRight.SetValueAndForceNotify)
                .AddTo(_compositeDisposable);
            
            _stageSelectLeftButton
                .OnClickAsObservable()
                .Select(x => true)
                .Subscribe(_isLeft.SetValueAndForceNotify)
                .AddTo(_compositeDisposable);
            
            _playGameButton
                .OnClickAsObservable()
                .Select(x => true)
                .Subscribe(_isPlayGame.SetValueAndForceNotify)
                .AddTo(_compositeDisposable);
            
            _undoButton
                .OnClickAsObservable()
                .Select(x => true)
                .Subscribe(_isUndo.SetValueAndForceNotify)
                .AddTo(_compositeDisposable);
            
            _resetButton
                .OnClickAsObservable()
                .Select(x => true)
                .Subscribe(_isReset.SetValueAndForceNotify)
                .AddTo(_compositeDisposable);
            
            _quitButton
                .OnClickAsObservable()
                .Select(x => true)
                .Subscribe(_isQuit.SetValueAndForceNotify)
                .AddTo(_compositeDisposable);
        }

        /// <summary>
        /// リソースを開放する
        /// </summary>
        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}