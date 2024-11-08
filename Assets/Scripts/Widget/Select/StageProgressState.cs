/// <summary>
/// ステージの攻略状況を管理する
/// </summary>
[System.Serializable]
public enum StageProgressState
{
    /// <summary>
    /// ステージ未開放
    /// </summary>
    Locked,   
    
    /// <summary>
    /// ステージ進行中
    /// </summary>
    InProgress,
    
    /// <summary>
    /// ステージクリア
    /// </summary>
    Completed 
}
