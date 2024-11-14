using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Input
{
    /// <summary>
    /// 入力を注入する
    /// </summary>
    public class InputEventInstaller : MonoInstaller
    {
        /// <summary>
        /// ゲームを開始するボタン
        /// </summary>
        [SerializeField] private Button _gameStartButton;

        /// <summary>
        /// ステージセレクトを左に移動するボタン
        /// </summary>
        [SerializeField] private Button _stageSelectLeftButton;
        
        /// <summary>
        /// ステージセレクトを右に移動するボタン
        /// </summary>
        [SerializeField] private Button _stageSelectRightButton;
        
        /// <summary>
        /// 前に移動するボタン
        /// </summary>
        [SerializeField] private Button _gameForwardButton;
        
        /// <summary>
        /// 右に移動するボタン
        /// </summary>
        [SerializeField] private Button _gameRightButton;
        
        /// <summary>
        /// 左に移動するボタン
        /// </summary>
        [SerializeField] private Button _gameLeftButton;
        
        /// <summary>
        /// 後進するボタン
        /// </summary>
        [SerializeField] private Button _gameBackButton;
        
        /// <summary>
        /// ステージ攻略を開始するボタン
        /// </summary>
        [SerializeField] private Button _playGameButton;
        
        /// <summary>
        /// 移動を取り消しボタン
        /// </summary>
        [SerializeField]　private Button _undoButton;
        
        /// <summary>
        /// リセットするボタン
        /// </summary>
        [SerializeField]　private Button _resetButton;
        
        /// <summary>
        /// ゲームをやめるボタン
        /// </summary>
        [SerializeField]　private Button _quitButton;
        
        public override void InstallBindings()
        {
            Button[] buttons = new Button[]
            {
                _gameStartButton, _stageSelectLeftButton,_stageSelectRightButton,_gameForwardButton, _gameRightButton, _gameLeftButton, _gameBackButton,
                _playGameButton, _undoButton, _resetButton, _quitButton
            };
            
            Container.Bind(typeof(IInputEventProvider), typeof(IInitializable),
                    typeof(IDisposable)).To<ButtonInputProvider>()
                .AsSingle().WithArguments(buttons);
        }
    }
}