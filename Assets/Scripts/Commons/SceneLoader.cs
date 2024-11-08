using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneField Scene;

    private AsyncOperation _scene;
    
    private void Start()
    {
        _scene = SceneManager.LoadSceneAsync(Scene);
        _scene.allowSceneActivation = false;
    }

    public void SceneTransition()
    {
        _scene.allowSceneActivation = true;
    }

    [System.Serializable]
    public class SceneField
    {
        [SerializeField] private Object _sceneAsset;

        [SerializeField] private string _sceneName = "";

        public string SceneName => _sceneName;

        public static implicit operator string(SceneField sceneField)
        {
            return sceneField.SceneName;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SceneField))]
    public class SceneFieldPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            EditorGUI.BeginProperty(_position, GUIContent.none, _property);
            SerializedProperty sceneAsset = _property.FindPropertyRelative("_sceneAsset");
            SerializedProperty sceneName = _property.FindPropertyRelative("_sceneName");
            _position = EditorGUI.PrefixLabel(_position, GUIUtility.GetControlID(FocusType.Passive), _label);
            if (sceneAsset != null)
            {
                sceneAsset.objectReferenceValue = EditorGUI.ObjectField(_position, sceneAsset.objectReferenceValue,
                    typeof(SceneAsset), false);

                if (sceneAsset.objectReferenceValue != null)
                {
                    sceneName.stringValue = (sceneAsset.objectReferenceValue as SceneAsset).name;
                }
            }

            EditorGUI.EndProperty();
        }
    }
#endif
}