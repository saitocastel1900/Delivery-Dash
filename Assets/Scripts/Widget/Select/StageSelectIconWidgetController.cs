using System.Collections.Generic;
using System.Linq;
using Commons.Extensions;
using Commons.Save;
using DG.Tweening;
using Input;
using UniRx;
using UnityEngine;
using Zenject;

namespace Widget.Select
{
    /// <summary>
    /// ステージ選択アイコンを管理する
    /// </summary>
    public class StageSelectIconWidgetController : MonoBehaviour
    {
        /// <summary>
        /// 現在いる要素の番号
        /// </summary>
        public int CurrentStageNumber => _currentStageNumber;
        private int _currentStageNumber = 0;
        
        /// <summary>
        /// StageSelectPanelList
        /// </summary>
        [SerializeField] List<RectTransform> _stageSelectIconList;

        /// <summary>
        /// IInputEventProvider
        /// </summary>
        [Inject] private IInputEventProvider _inputEventProvider;
        
        /// <summary>
        /// SaveManager
        /// </summary>
        [Inject] private SaveManager _saveManager;
        
        /// <summary>
        /// Tween
        /// </summary>
        private Tween _tween;
        
        private void Start()
        {
            _saveManager.Load();
            
            SetStageSelectIcon(_saveManager.Data.MaxStageClearNumber);
            
            SetStageSelectPanelListScale();

            //TODO: ここはinputでvector3を要れたらいいかも
            //入力に応じて、ステージ選択アイコンを回転させる
            _inputEventProvider
                .IsRight
                .SkipLatestValueOnSubscribe()
                .Select(_=>1)
                .Subscribe(Turn)
                .AddTo(this.gameObject);
            
            _inputEventProvider
                .IsLeft
                .SkipLatestValueOnSubscribe()
                .Select(_=>-1)
                .Subscribe(Turn)
                .AddTo(this.gameObject);
        }
        
        /// <summary>
        /// インタラクションを設定する
        /// </summary>
        private void SetStageSelectIcon(int clearStageNo)
        {
            foreach ((RectTransform stageSelectPane, int index) in _stageSelectIconList.Select((x, i) => (x, i)))
            {
                StageProgressState state = clearStageNo switch
                {
                    var n when n > index => StageProgressState.Completed,
                    var n when n == index => StageProgressState.InProgress,
                    _ => StageProgressState.Locked
                };

                stageSelectPane.GetComponent<StageSelectIconWidget>().SetView(state);
            }
        }

        /// <summary>
        /// 大きさを設定
        /// </summary>
        private void SetStageSelectPanelListScale()
        {
            foreach (var stageSelectIcon in _stageSelectIconList)
            {
                float distance = Mathf.Abs(stageSelectIcon.GetAnchoredPosX());
                float scale = Mathf.Clamp(1.0f - distance / (170.0f * 4.0f), 0.65f, 1.0f);
                
                //イージングをつける
                stageSelectIcon.SetLocalScaleXY(scale);
            }
        }

        /// <summary>
        /// 回転させる
        /// </summary>
        /// <param name="direction">方向</param>
        private void Turn(int direction)
        {
            int newStageNumber = _currentStageNumber + direction;
            
            if (newStageNumber < 0 || newStageNumber >= _stageSelectIconList.Count)
            {
                return;
            }
            
            _currentStageNumber = newStageNumber;
            
            TurnAnimation();
        }

        /// <summary>
        /// 回転するアニメーション
        /// </summary>
        private void TurnAnimation()
        {
            if (_tween != null)
            {
                _tween.Kill();     
                _tween.Complete();
            }

            RectTransform nextRectTransform = _stageSelectIconList[_currentStageNumber];
            float nextPositionX = -nextRectTransform.GetAnchoredPosX();

            foreach (var stageSelectPane in _stageSelectIconList)
            {
                var sequence = DOTween.Sequence();
                sequence.Append(stageSelectPane.DOAnchorPosX(stageSelectPane.GetAnchoredPosX() + nextPositionX, 0.1f));
                sequence.AppendCallback(SetStageSelectPanelListScale).SetLink(this.gameObject);
                _tween = sequence;
            }
        }
    }
}