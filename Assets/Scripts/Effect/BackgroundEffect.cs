using UniRx;
using Input;
using UnityEngine;
using Zenject;

/// <summary>
/// 背景のエフェクトを管理する
/// </summary>
public class BackgroundEffect : MonoBehaviour
{
   /// <summary>
   /// ロボのモデル
   /// </summary>
   [SerializeField] private ParticleSystem _robotEffect;
   
   /// <summary>
   /// 三角コーンのモデル
   /// </summary>
   [SerializeField] private ParticleSystem _coneEffect;
   
   /// <summary>
   /// 段ボールのモデル
   /// </summary>
   [SerializeField] private ParticleSystem _cardBoardEffect;

   /// <summary>
   /// IInputEventProvider
   /// </summary>
   [Inject] private IInputEventProvider _input;
   
   private void Start()
   {
      //ゲームを始めたら、エフェクトの再生をやめる
      _input
         .IsGameStart
         .SkipLatestValueOnSubscribe()
         .Subscribe(_ =>
         {
            _robotEffect.Stop();
            _coneEffect.Stop();
            _cardBoardEffect.Stop();
         })
         .AddTo(this.gameObject);
   }
}
