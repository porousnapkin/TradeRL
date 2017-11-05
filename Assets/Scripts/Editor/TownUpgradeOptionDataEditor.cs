using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(TownUpgradeOptionData))]
public class TownUpgradeOptionDataEditor : Editor {
    List<Editor> benefits = new List<Editor>();

    public override void OnInspectorGUI()
    {
        var data = target as TownUpgradeOptionData;
        data.storyDescription = EditorGUILayout.TextField("Story", data.storyDescription);
        data.gameDescription = EditorGUILayout.TextField("Gameplay Desc", data.gameDescription);

        int newCount = EditorGUILayout.IntField("Benefits", data.benefits.Count);
        EditorHelper.UpdateList(ref data.benefits, newCount, () => null, (a) => DestroyImmediate(a, true));
        EditorHelper.UpdateList(ref benefits, newCount, () => null, (t) => { });
        EditorGUI.indentLevel++;
        for (int i = 0; i < data.benefits.Count; i++)
        {
            var b = data.benefits[i];
            var editor = benefits[i];
            data.benefits[i] = EditorHelper.DisplayScriptableObjectWithEditor(data, b, ref editor, "");
        }
        EditorGUI.indentLevel--;

        EditorUtility.SetDirty(data);
    }
}
