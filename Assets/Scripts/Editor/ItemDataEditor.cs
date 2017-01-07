using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ItemData))]
public class ItemDataEditor : Editor
{
    Editor effectEditor = null;

    public override void OnInspectorGUI()
    {
        var item = target as ItemData;
        item.itemName = EditorGUILayout.TextField("Name", item.itemName);
        item.jamChance = EditorGUILayout.FloatField("Jam Chance", item.jamChance);
        item.effect = EditorHelper.DisplayScriptableObjectWithEditor(item, item.effect, ref effectEditor, "Effect");

        EditorUtility.SetDirty(item);
    }
}
