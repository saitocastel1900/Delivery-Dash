using Commons.Save;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

/// <summary>
/// データ削除ボタンを管理する
/// </summary>
public class DeleteDataButtonWidget : MonoBehaviour
{
   /// <summary>
   /// Button
   /// </summary>
   [SerializeField] private Button _deleteDataButton;

   /// <summary>
   /// SaveManager
   /// </summary>
   [Inject] private SaveManager _saveManager;
   
   private void Start()
   {
      //ボタンが押されたら、データを削除する
      _deleteDataButton
         .OnClickAsObservable()
         .Subscribe(_ =>
         {
            //え、これ前部アクセスできるくない
            _saveManager.Data.CurrentStageNumber = 0;
            _saveManager.Data.MaxStageClearNumber = 0;
            _saveManager.Save();
         })
         .AddTo(this.gameObject);
   }
}
