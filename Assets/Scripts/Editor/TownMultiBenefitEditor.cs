using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(TownMultiBenefitData))]
public class TownMultiBenefitEditor : Editor
{
    List<Editor> benefits = new List<Editor>();

    public override void OnInspectorGUI()
    {
        var benefit = target as TownMultiBenefitData;

        int newCount = EditorGUILayout.IntField("Benefits", benefit.townBenefits.Count);
        EditorHelper.UpdateList(ref benefit.townBenefits, newCount, () => null, (a) => { });
        EditorHelper.UpdateList(ref benefits, newCount, () => null, (t) => { });
        EditorGUI.indentLevel++;
        for (int i = 0; i < benefit.townBenefits.Count; i++)
        {
            var b = benefit.townBenefits[i];
            var editor = benefits[i];
            benefit.townBenefits[i] = EditorHelper.DisplayScriptableObjectWithEditor(benefit, b, ref editor, "");
        }
        EditorGUI.indentLevel--;

        EditorUtility.SetDirty(benefit);
    }
}
