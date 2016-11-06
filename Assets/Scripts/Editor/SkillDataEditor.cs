using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(SkillData))]
public class SkillDataEditor : Editor
{
	bool showLevelUpBenefits = false;
    List<List<Editor>> levelBenefitsEditors = new List<List<Editor>>();

	public override void OnInspectorGUI()
	{
		var skill = target as SkillData;
		skill.displayName = EditorGUILayout.TextField("Display Name", skill.displayName);
		skill.description = EditorGUILayout.TextField("Description", skill.description);
	    skill.effortType = (Effort.EffortType)(EditorGUILayout.EnumPopup("Effort Type", skill.effortType));
		showLevelUpBenefits = EditorGUILayout.Foldout(showLevelUpBenefits, "Level Up Benefits");
		if(showLevelUpBenefits)
		{
			EditorGUI.indentLevel++;

            int numLevelsOfSkill = EditorGUILayout.IntField("Num Skill Levels", skill.levelBenefits.Count);
            EditorHelper.UpdateList(ref skill.levelBenefits, numLevelsOfSkill, () => new List<SkillLevelBenefit>(), (a) => { });
            EditorHelper.UpdateList(ref levelBenefitsEditors, numLevelsOfSkill, () => new List<Editor>(), (a) => { });

            EditorGUI.indentLevel++;
            for (int i = 0; i < numLevelsOfSkill; i++)
            {
                var benefits = skill.levelBenefits[i];
                var editors = levelBenefitsEditors[i];
                int newCount = EditorGUILayout.IntField("Level " + (i + 1) + " benefits", benefits.Count);
                EditorHelper.UpdateList(ref benefits, newCount, () => null, (t) => GameObject.DestroyImmediate(t));
                EditorHelper.UpdateList(ref editors, newCount, () => null, (t) => { });

                EditorGUI.indentLevel++;
                for (int j = 0; j < benefits.Count; j++)
                {
                    var benefit = benefits[j];
                    var editor = editors[j];
                    benefits[j] = EditorHelper.DisplayScriptableObjectWithEditor(skill, benefit, ref editor, "");
                }
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;

			EditorGUI.indentLevel--;
		}

		EditorUtility.SetDirty(skill);
	}
}