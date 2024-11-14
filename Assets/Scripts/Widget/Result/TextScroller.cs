using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// テキストのスクロールを管理する
/// </summary>
public class TextScroller : BaseMeshEffect
{
    /// <summary>
    /// Text
    /// </summary>
    [SerializeField] private Text _text;
    
    /// <summary>
    /// スクロールスピード
    /// </summary>
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }
    [SerializeField] private float _speed = 1500;

    /// <summary>
    /// 文字の感覚
    /// </summary>
    [SerializeField] private float _space;
    
    /// <summary>
    /// Graphic
    /// </summary>
    private Graphic _cacheGraphic;
    
    /// <summary>
    /// UIVertexのリスト
    /// </summary>
    private List<UIVertex> _uiVertexes = new List<UIVertex>();
    
    /// <summary>
    /// オフセット
    /// </summary>
    private float _offset;
    
    /// <summary>
    /// テキストの表示領域の幅
    /// </summary>
    private float _rectWidth;
    
    /// <summary>
    /// ピボットのX座標
    /// </summary>
    private float _pivotX;
    
    protected override void Awake()
    {
        Initialize();
    }
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        Initialize();
        _cacheGraphic.SetVerticesDirty();
    }
#endif
    
    private void Update()
    {
        _offset += Time.deltaTime * _speed;
        _cacheGraphic.SetVerticesDirty();
    }
    
    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Initialize()
    {
        _text.horizontalOverflow = HorizontalWrapMode.Overflow;
        _cacheGraphic = GetComponent<Graphic>();
        _pivotX = (transform as RectTransform).pivot.x;
        _rectWidth = (transform as RectTransform).rect.width;
    }
    
    /// <summary>
    /// テキストの頂点データを変更する
    /// </summary>
    /// <param name="vertex">頂点データ</param>
    public override void ModifyMesh(VertexHelper vertex)
    {
        _uiVertexes.Clear();
        vertex.GetUIVertexStream(_uiVertexes);
        var count = _uiVertexes.Count;
        
        if (count > 5)
        {
            var textWidth = _uiVertexes[count - 3].position.x - _uiVertexes[0].position.x;
            
            var textCount = _uiVertexes.Count / 6;
            
            var charaWidth = textWidth / textCount;
            if (textWidth - charaWidth + _space > _rectWidth)
            {
                var offset = _offset % (textWidth + _space);
                var leftValue = Mathf.Lerp(0, _rectWidth * -1, _pivotX);
                for (var i = 0; i < count; i += 6)
                {
                    var checkVert = _uiVertexes[i + 3];
                    checkVert.position.x -= offset;
                    var isAddValue = checkVert.position.x < leftValue;
                    if (isAddValue)
                        checkVert.position.x += textWidth + _space;
                
                    _uiVertexes[i + 3] = checkVert;
                    foreach (var index in new[]{0, 1, 2, 4, 5})
                    {
                        var vert = _uiVertexes[i + index];
                        vert.position.x -= offset;
                        if (isAddValue)
                            vert.position.x += textWidth + _space;
                        _uiVertexes[i + index] = vert;
                    }
                }
            }
        }
        
        vertex.Clear();
        vertex.AddUIVertexTriangleStream(_uiVertexes);
    }
}
