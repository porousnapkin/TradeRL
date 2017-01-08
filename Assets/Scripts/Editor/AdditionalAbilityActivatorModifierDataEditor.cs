using UnityEditor;

[CustomEditor(typeof(AdditionalAbilityActivatorModifierData))]
public class AdditionalAbilityActivatorModifierDataEditor : Editor
{
    Editor activatorEditor = null;
    Editor animationEditor = null;

    public override void OnInspectorGUI()
    {
        var abilityData = target as AdditionalAbilityActivatorModifierData;
        abilityData.activator = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.activator, ref activatorEditor, "Activator");
        abilityData.animationData = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.animationData, ref animationEditor, "Animation");

        EditorUtility.SetDirty(abilityData);
    }
}

