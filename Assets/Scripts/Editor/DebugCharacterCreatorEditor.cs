using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DebugCharacterCreator))]
public class DebugCharacterCreatorEditor : Editor {
    public override void OnInspectorGUI()
    {
        var d = target as DebugCharacterCreator;
        var skillsDb = SkillsDatabase.Instance;
        EditorHelper.UpdateList(ref d.skillLevelsIndexed, skillsDb.allSkills.Count, () => 0, (i) => { });

        EditorGUILayout.LabelField("Skills");
        for(int i = 0; i < d.skillLevelsIndexed.Count; i++)
        {
            var skillName = skillsDb.allSkills[i].displayName;
            d.skillLevelsIndexed[i] = EditorGUILayout.IntField(skillName, d.skillLevelsIndexed[i]);
        }

        EditorUtility.SetDirty(d);
    }
}
