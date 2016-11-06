using System.Collections.Generic;
using UnityEditor;

public class DesertEditorTools
{
    public static void DisplayLabelList(List<AbilityLabel> labels, string name)
    {
        int newCount = EditorGUILayout.IntField(name, labels.Count);
        EditorHelper.UpdateList(ref labels, newCount, () => AbilityLabel.Attack, (a) => { });
        EditorGUI.indentLevel++;
        for (int i = 0; i < labels.Count; i++)
            labels[i] = (AbilityLabel)EditorGUILayout.EnumPopup(labels[i]);
        EditorGUI.indentLevel--;
    }
}

