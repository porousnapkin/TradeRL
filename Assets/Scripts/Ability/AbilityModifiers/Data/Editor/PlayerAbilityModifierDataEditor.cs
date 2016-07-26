using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(PlayerAbilityModifierData))]
public class PlayerAbilityModifierDataEditor : Editor
{
	Editor abilityEditor = null;

	public override void OnInspectorGUI()
	{
		var abilityData = target as PlayerAbilityModifierData;
		abilityData.cooldown = EditorGUILayout.IntField("Cooldown", abilityData.cooldown);
		abilityData.abilityModifier = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.abilityModifier, ref abilityEditor, "Ability Modifier");
		abilityData.name = EditorGUILayout.TextField("Name", abilityData.name);

		EditorUtility.SetDirty(abilityData);
	}
}