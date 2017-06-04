using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(TownTraitData))]
public class TownTraitDataEditor : Editor {
    List<Editor> citizenReputationBenefitEditors = new List<Editor>();

    public override void OnInspectorGUI()
    {
        var trait = target as TownTraitData;

        EditorHelper.DisplayBasicObjectEditorList(trait.cityActivities, "City Activities");
        EditorHelper.DisplayBasicObjectEditorList(trait.travelSupplies, "Travel Supplies");
        EditorHelper.DisplayBasicObjectEditorList(trait.hireableAllies, "Hireable Allies");

        int newCount = EditorGUILayout.IntField("Benefits For Citizen Reputation", trait.benefitsForCitizenReputation.Count);
        EditorHelper.UpdateList(ref trait.benefitsForCitizenReputation, newCount, () => null, (a) => { });
        EditorHelper.UpdateList(ref citizenReputationBenefitEditors, newCount, () => null, (t) => { });
        EditorGUI.indentLevel++;
        for(int i = 0; i < trait.benefitsForCitizenReputation.Count; i++)
        {
            var benefit = trait.benefitsForCitizenReputation[i];
            var editor = citizenReputationBenefitEditors[i];
            trait.benefitsForCitizenReputation[i] = EditorHelper.DisplayScriptableObjectWithEditor(trait, benefit, ref editor, (i+1).ToString());
        }
        EditorGUI.indentLevel--;

        EditorUtility.SetDirty(trait);
    }
}
