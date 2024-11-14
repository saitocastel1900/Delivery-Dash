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
        /// リザルト表示を開始するか
        /// </summary>
        public IObservable<Unit> OnShowResultWidgetAsObservable => _resultWidgetSubject;
        private Subject<Unit> _resultWidgetSubject = new Subject<Unit>();
        
        /// <summary>
        ///　リザルト表示が終了したか
        /// </summary>
        public IObservable<Unit> OnFinishResult => _cutinWidgetSubject;
        private Subject<Unit> _cutinWidgetSubject = new Subject<Unit>();
   
        /// <summary>
        /// リザルト表示を開始する
        /// </summary>
        public void StartResult() => _resultWidgetSubject.OnNext(Unit.Default);
        
        /// <summary>
        /// ガイドのアニメーションが終了した
        /// </summary>
        public void FinishCutinAnimation() => _cutinWidgetSubject.OnNext(Unit.Default);
    }
}