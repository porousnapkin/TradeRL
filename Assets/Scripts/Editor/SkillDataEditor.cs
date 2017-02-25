using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(SkillData))]
public class SkillDataEditor : Editor
{
    List<List<Editor>> levelBenefitsEditors = new List<List<Editor>>();

	public override void OnInspectorGUI()
	{
		var skill = target as SkillData;
		skill.displayName = EditorGUILayout.TextField("Display Name", skill.displayName);
		skill.description = EditorGUILayout.TextField("Description", skill.description);
	    skill.effortType = (Effort.EffortType)(EditorGUILayout.EnumPopup("Effort Type", skill.effortType));
		{

            int numLevelsOfSkill = EditorGUILayout.IntField("Num Skill Levels", skill.levelBenefits.Count);
            EditorHelper.UpdateList(ref skill.levelBenefits, numLevelsOfSkill, () => new ListOfSkillBenefits(), (a) => { });
            EditorHelper.UpdateList(ref levelBenefitsEditors, numLevelsOfSkill, () => new List<Editor>(), (a) => { });

            EditorGUI.indentLevel++;
            for (int i = 0; i < numLevelsOfSkill; i++)
            {
                var benefits = skill.levelBenefits[i];
                var editors = levelBenefitsEditors[i];
                int newCount = EditorGUILayout.IntField("Level " + (i + 1) + " benefits", benefits.listOfBenefits.Count);
				EditorHelper.UpdateList(ref benefits.listOfBenefits, newCount, () => null, (t) => GameObject.DestroyImmediate(t, true));
                EditorHelper.UpdateList(ref editors, newCount, () => null, (t) => { });

                EditorGUI.indentLevel++;
                for (int j = 0; j < benefits.listOfBenefits.Count; j++)
                {
                    var benefit = benefits.listOfBenefits[j];
                    var editor = editors[j];
                    benefits.listOfBenefits[j] = EditorHelper.DisplayScriptableObjectWithEditor(skill, benefit, ref editor, "");
                }
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;

		}

		EditorUtility.SetDirty(skill);
	}
}