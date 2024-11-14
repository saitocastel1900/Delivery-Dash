using System;
using UnityEngine;
using System.IO;
using UniRx;
using Zenject;

namespace Commons.Save
{
    /// <summary>
    /// セーブマネージャーを管理する
    /// </summary>
    public class SaveManager : IInitializable,IDisposable
    {
        /// <summary>
        /// セーブするデータ
        /// </summary>
        public SaveData Data { get; private set; }

        /// <summary>
        ///　現在のステージ番号
        /// </summary>
        public IReadOnlyReactiveProperty<int> CurrentStageNumber => _currentStageNumber;

        private ReactiveProperty<int> _currentStageNumber = new ReactiveProperty<int>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private SaveManager()
        {
            Load();
        }

        /// <summary>
        /// 保存場所
        /// </summary>
        private string Path =>Application.streamingAssetsPath + "/SaveData.json";
        
        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            _currentStageNumber
                .Subscribe(x=>Data.CurrentStageNumber = x);
            
            SetCurrentStageNumber(Const.Const.InitialStageNumber);
            
            Save();
        }
        
        /// <summary>
        /// 現在のステージ番号を設定
        /// </summary>
        /// <param name="stageNumber">現在のステージ番号</param>
        public void SetCurrentStageNumber(int stageNumber)
        {
            _currentStageNumber.Value = stageNumber;
        }
        
        /// <summary>
        /// セーブ
        /// </summary>
        public void Save()
        {
            string jsonData = JsonUtility.ToJson(Data);
            StreamWriter streamWriter = new StreamWriter(Path, false);
            streamWriter.WriteLine(jsonData);
            streamWriter.Flush();
            streamWriter.Close();
            DebugUtility.Log("セーブ完了");
        }

        /// <summary>
        /// ロード
        /// </summary>
        public void Load()
        {
            //ファイルが存在しなかったら、ファイルを作成する
            if (!File.Exists(Path))
            {
                DebugUtility.Log("ファイルが存在しません。初回起動です");
                Data = new SaveData(Const.Const.InitialStageNumber, Const.Const.InitialStageNumber);
                
                Save();
                return;
            }

            StreamReader streamReader = new StreamReader(Path);
            string jsonData = streamReader.ReadToEnd();
            Data = JsonUtility.FromJson<SaveData>(jsonData);
            streamReader.Close();
            DebugUtility.Log("ロードしました");
        }

        /// <summary>
        /// リセット
        /// </summary>
        public void Reset()
        {
            SetCurrentStageNumber(Const.Const.InitialStageNumber);
            Data.MaxStageClearNumber = Const.Const.InitialStageNumber;
            Save();
            DebugUtility.Log("データがリセットされました");
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _currentStageNumber?.Dispose();
        }
    }
}