using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(SkillData))]
public class SkillDataEditor : Editor
{
	bool showLevelUpBenefits = false;
	List<Editor> level1BenefitEditors = new List<Editor>();
	List<Editor> level2BenefitEditors = new List<Editor>();
	List<Editor> level3BenefitEditors = new List<Editor>();
	List<Editor> level4BenefitEditors = new List<Editor>();
	List<Editor> level5BenefitEditors = new List<Editor>();

	public override void OnInspectorGUI()
	{
		var skill = target as SkillData;
		skill.displayName = EditorGUILayout.TextField("Display Name", skill.displayName);
		skill.description = EditorGUILayout.TextField("Description", skill.description);
		showLevelUpBenefits = EditorGUILayout.Foldout(showLevelUpBenefits, "Level Up Benefits");
		if(showLevelUpBenefits)
		{
			EditorGUI.indentLevel++;

			ShowBenefits(1, level1BenefitEditors, skill.level1Benefits, skill);
			ShowBenefits(2, level2BenefitEditors, skill.level2Benefits, skill);
			ShowBenefits(3, level3BenefitEditors, skill.level3Benefits, skill);
			ShowBenefits(4, level4BenefitEditors, skill.level4Benefits, skill);
			ShowBenefits(5, level5BenefitEditors, skill.level5Benefits, skill);

			EditorGUI.indentLevel--;
		}


		EditorUtility.SetDirty(skill);
	}

	void ShowBenefits(int level, List<Editor> editors, List<SkillLevelBenefit> benefits, SkillData skill)
	{
		int newCount = EditorGUILayout.IntField("Level " + level + " benefits", benefits.Count);
		EditorHelper.UpdateList(ref benefits, newCount, () => null, (t) => GameObject.DestroyImmediate(t));
		EditorHelper.UpdateList(ref editors, newCount, () => null, (t) => { });
		EditorGUI.indentLevel++;

		for (int i = 0; i < benefits.Count; i++)
		{
			var benefit = benefits[i];
			var editor = editors[i];
			benefits[i] = EditorHelper.DisplayScriptableObjectWithEditor(skill, benefit, ref editor, "");
		}

		EditorGUI.indentLevel--;
	}
}