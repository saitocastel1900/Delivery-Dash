using System;
using UniRx;
using UnityEngine;

namespace Widget.Result
{
    /// <summary>
    /// リザルトを管理する
    /// </summary>
    public class ResultWidgetController : MonoBehaviour
    {
        /// <summary>
        /// ダイアログを表示するか
        /// </summary>
        public IObservable<Unit> OnShowResultWidgetAsObservable => _resultWidgetSubject;
        private Subject<Unit> _resultWidgetSubject = new Subject<Unit>();
        
        /// <summary>
        /// マーカー読み取りのガイドが終了したか
        /// </summary>
        public IObservable<Unit> OnFinishResult => _stageClearAndNextStageWidgetSubject;
        private Subject<Unit> _stageClearAndNextStageWidgetSubject = new Subject<Unit>();
   
        /// <summary>
        /// リザルト表示を開始する
        /// </summary>
        public void StartResult() => _resultWidgetSubject.OnNext(Unit.Default);
        
        /// <summary>
        /// ガイドのアニメーションが終了した
        /// </summary>
        public void FinishStageClearAndNextStageAnimation() => _stageClearAndNextStageWidgetSubject.OnNext(Unit.Default);
    }
}