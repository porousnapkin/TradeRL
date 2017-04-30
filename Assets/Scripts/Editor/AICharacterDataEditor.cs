using UnityEditor;

[CustomEditor(typeof(AICharacterData))]
public class AICharacterDataEditor : Editor
{
    Editor specialEffectEditor;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var data = target as AICharacterData;
        data.specialEffectData = EditorHelper.DisplayScriptableObjectWithEditor(data, data.specialEffectData, ref specialEffectEditor, "Special Effect");

        EditorUtility.SetDirty(data);
    }
}
