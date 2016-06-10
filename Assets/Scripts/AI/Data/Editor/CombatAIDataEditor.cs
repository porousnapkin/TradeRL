using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(CombatAIData))]
public class CombatAIDataEditor : Editor
{
    List<Editor> combatAINodeEditors = new List<Editor>();
    List<bool> nodeFoldouts = new List<bool>();

    public override void OnInspectorGUI()
    {
        var data = target as CombatAIData;

    	int newSize = EditorGUILayout.IntField("size", data.nodes.Count);
   		EditorHelper.UpdateList(ref data.nodes, newSize, CreateNode, DestroyNode);
    	EditorHelper.UpdateList(ref combatAINodeEditors, newSize, () => null, (e) => {});
    	EditorHelper.UpdateList(ref nodeFoldouts, newSize, () => false, (b) => {});

    	EditorGUI.indentLevel++;

        for (int i = 0; i < data.nodes.Count; i++)
        {
            var e = combatAINodeEditors[i];
            if (e == null)
            {
                e = Editor.CreateEditor(data.nodes[i]);
                combatAINodeEditors[i] = e;
            }

            nodeFoldouts[i] = EditorGUILayout.Foldout(nodeFoldouts[i], "Node " + (i + 1));
            EditorGUI.indentLevel++;
            if(nodeFoldouts[i])
    			combatAINodeEditors[i].OnInspectorGUI();
            EditorGUI.indentLevel--;
        }

        data.fallbackAbility = EditorGUILayout.ObjectField("Fallback Ability", data.fallbackAbility, typeof(AIAbilityData), false) as AIAbilityData;

        EditorGUI.indentLevel--;
    }

    CombatAINodeData CreateNode()
    {
		var newElement = ScriptableObject.CreateInstance<CombatAINodeData>();
        newElement.name = "node";
		newElement.hideFlags = HideFlags.HideInHierarchy;
        var assetPath = AssetDatabase.GetAssetPath(target);

        AssetDatabase.AddObjectToAsset(newElement, assetPath);
		EditorUtility.SetDirty(target);
		EditorUtility.SetDirty(newElement);
		AssetDatabase.SaveAssets();

        combatAINodeEditors.Add(Editor.CreateEditor(newElement));

        return newElement;
    }

    void DestroyNode(CombatAINodeData n)
    {
        combatAINodeEditors.RemoveAt(combatAINodeEditors.Count-1);

        //TODO: Need to destroy all subcomponents
        GameObject.DestroyImmediate(n, true);
    }
}
