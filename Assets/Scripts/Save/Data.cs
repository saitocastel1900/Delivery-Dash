using System;

namespace Commons.Save
{
    /// <summary>
    /// セーブデータを管理する
    /// </summary>
    [Serializable]
    public class SaveData
    {
        /// <summary>
        /// 現在のステージ番号
        /// </summary>
        public int CurrentStageNumber = 0;
        
        /// <summary>
        /// クリアした最大のステージ番号
        /// </summary>
        public int MaxStageClearNumber = 0;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="currentStageNumber"></param>
        /// <param name="maxStageClearNumber"></param>
        public SaveData(int currentStageNumber, int maxStageClearNumber)
        {
            CurrentStageNumber = currentStageNumber;
            MaxStageClearNumber = maxStageClearNumber;
        }
    }
}
