using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(TownData))]
public class TownDataEditor : Editor {
    Dictionary<int, List<Editor>> citizenEditorsByTrack = new Dictionary<int, List<Editor>>();
    bool citizensUnfolded = false;
    Dictionary<int, List<Editor>> politicianEditorsByTrack = new Dictionary<int, List<Editor>>();
    bool politicianUnfolded = false;

    public override void OnInspectorGUI()
    {
        var data = target as TownData;

        data.displayName = EditorGUILayout.TextField("Name", data.displayName);
        citizensUnfolded = EditorGUILayout.Foldout(citizensUnfolded, "Citizen Influence Upgrades");
        if(citizensUnfolded)
            DisplayUpgradeTracks(data, data.citizenUpgradeOptions, citizenEditorsByTrack);
        politicianUnfolded = EditorGUILayout.Foldout(politicianUnfolded, "Political Influence Upgrades");
        if(politicianUnfolded)
            DisplayUpgradeTracks(data, data.politicalUpgradeOptions, politicianEditorsByTrack);

        EditorUtility.SetDirty(data);
    }

    void DisplayUpgradeTracks(TownData data, List<TownData.ListOfTownUpgradeOptions> upgradeTracks, Dictionary<int, List<Editor>> editorsByTrack)
    {
        EditorGUI.indentLevel++;
        int numTracks = upgradeTracks.Count;
        int newNumTracks = EditorGUILayout.IntField("Num Upgrade Tracks", numTracks);

        //TODO: Readdress destructor...
        EditorHelper.UpdateList(ref upgradeTracks, newNumTracks, () => new TownData.ListOfTownUpgradeOptions(),
            (a) => { a.list.ForEach(b => DestroyImmediate(b, true)); });
        for (int track = 0; track < newNumTracks; track++)
        {
            EditorGUI.indentLevel++;
            if (!editorsByTrack.ContainsKey(track))
                editorsByTrack[track] = new List<Editor>();

            var editors = editorsByTrack[track];
            var upgrades = upgradeTracks[track].list;

            int numUpgrades = upgrades.Count;
            int newNumUpgrades = EditorGUILayout.IntField("Num Options", numUpgrades);
            EditorHelper.UpdateList(ref upgrades, newNumUpgrades, () => null, (a) => DestroyImmediate(a, true));
            EditorHelper.UpdateList(ref editors, newNumUpgrades, () => null, (a) => { });
            for (int upgradeIndex = 0; upgradeIndex < newNumUpgrades; upgradeIndex++)
            {
                EditorGUI.indentLevel++;
                var editor = editors[upgradeIndex];
                upgrades[upgradeIndex] = EditorHelper.CreateAndDisplaySpecificScriptableObjectType(upgrades[upgradeIndex], data, ref editor);
                EditorGUI.indentLevel--;
            }

            EditorGUI.indentLevel--;
        }

        EditorGUI.indentLevel--;
    }

}
