using UnityEditor;

[CustomEditor(typeof(EnemyAmbushAbilityData))]
public class EnemyAmbushAbilityDataEditor : Editor
{
    Editor activatorEditor = null;

    public override void OnInspectorGUI()
    {
        var abilityData = target as EnemyAmbushAbilityData;
        abilityData.abilityName = EditorGUILayout.TextField("Name", abilityData.abilityName);
        abilityData.description = EditorGUILayout.TextField("Description", abilityData.description);
        abilityData.activator = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.activator,
            ref activatorEditor, "Activator");

        EditorUtility.SetDirty(abilityData);
    }
}