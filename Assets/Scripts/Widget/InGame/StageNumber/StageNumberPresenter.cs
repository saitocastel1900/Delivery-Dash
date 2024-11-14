using System;
using Commons.Save;
using Input;
using UniRx;
using Zenject;

namespace Widget.InGame.StageNumber
{
    /// <summary>
    /// ステージ番号のデータと見た目の橋渡しをする
    /// </summary>
    public class StageNumberPresenter : IDisposable, IInitializable
    {
        /// <summary>
        /// View
        /// </summary>
        private StageNumberView _view;

        /// <summary>
        /// IInputEventProvider
        /// </summary>
        private IInputEventProvider _input;
        
        /// <summary>
        /// SaveManager
        /// </summary>
        private SaveManager _saveManager;

        /// <summary>
        /// Disposable
        /// </summary>
        private CompositeDisposable _compositeDisposable;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StageNumberPresenter(StageNumberView view, SaveManager saveManager, IInputEventProvider input)
        {
            _view = view;
            _saveManager = saveManager;
            _input = input;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            _compositeDisposable = new CompositeDisposable();
            _saveManager.Load();
            Bind();
        }
        
        /// <summary>
        /// Bind
        /// </summary>
        private void Bind()
        {
            //ステージに移動したら、そのステージの番号を反映する
            _saveManager
                .CurrentStageNumber
                .Subscribe(x=>_view.SetText(x))
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
}