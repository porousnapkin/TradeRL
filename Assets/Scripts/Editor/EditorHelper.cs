using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System;

public class EditorHelper
{
    public static T DisplayScriptableObjectWithEditor<T>(ScriptableObject owner, T scriptObject, ref Editor editor,
        string displayTitle) where T : ScriptableObject
    {
        scriptObject = EditorHelper.DisplayScriptableObjectData(scriptObject, owner, displayTitle);

        if (scriptObject != null && (editor == null || editor.target != scriptObject))
            editor = Editor.CreateEditor(scriptObject);

        EditorGUI.indentLevel++;
        if (editor != null)
            editor.OnInspectorGUI();
        EditorGUI.indentLevel--;

        return scriptObject;
    }

    public static T DisplayScriptableObjectData<T>(T currentElement, ScriptableObject owner, string displayTitle)
        where T : ScriptableObject
    {
        var types = GetNonAbstractSubtypesOfType<T>();
        var typeNames = types.ConvertAll(t => t.ToString()).ToArray();
        int myIndex = -1;
        if (currentElement != null)
            myIndex = types.FindIndex(t => t == currentElement.GetType());

        int newIndex = EditorGUILayout.Popup(displayTitle, myIndex, typeNames);

        if (myIndex != newIndex && newIndex >= 0)
        {
            if (currentElement != null)
                GameObject.DestroyImmediate(currentElement, true);
            T newElement = ScriptableObject.CreateInstance(types[newIndex]) as T;
            newElement.name = typeNames[newIndex];
            newElement.hideFlags = HideFlags.HideInHierarchy;
            var assetPath = AssetDatabase.GetAssetPath(owner);

            AssetDatabase.AddObjectToAsset(newElement, assetPath);
            EditorUtility.SetDirty(owner);
            EditorUtility.SetDirty(newElement);
            AssetDatabase.SaveAssets();

            return newElement;
        }

        return currentElement;
    }

    public static T CreateAndDisplaySpecificScriptableObjectType<T>(T currentElement, ScriptableObject owner, ref Editor editor)
        where T : ScriptableObject
    {
        if (currentElement == null)
        {
            T newElement = ScriptableObject.CreateInstance<T>();
            newElement.name = typeof(T).Name;
            newElement.hideFlags = HideFlags.HideInHierarchy;
            var assetPath = AssetDatabase.GetAssetPath(owner);

            AssetDatabase.AddObjectToAsset(newElement, assetPath);
            EditorUtility.SetDirty(owner);
            EditorUtility.SetDirty(newElement);
            AssetDatabase.SaveAssets();

            currentElement = newElement;
        }

        if(editor == null)
        {
            editor = Editor.CreateEditor(currentElement);
        }

        editor.OnInspectorGUI();

        return currentElement;
    }

    public static List<Type> GetNonAbstractSubtypesOfType<T>() where T : ScriptableObject
    {
        return Assembly.GetAssembly(typeof(T)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))).ToList();
    }

    public static void UpdateList<T>(ref List<T> list, int newSize, System.Func<T> constructor, System.Action<T> destroyCallback)
    {
        while (list.Count < newSize)
            list.Add(constructor());

        while (list.Count > newSize)
        {
            T entry = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            destroyCallback(entry);
        }
    }

    public static void DisplayBasicObjectEditorList<T>(List<T> list, string description) where T : UnityEngine.Object
    {
        int newCount = EditorGUILayout.IntField(description, list.Count);
        UpdateList(ref list, newCount, () => default(T), (a) => {});

        EditorGUI.indentLevel++;
        for(int i = 0; i < list.Count; i++)
            list[i] = EditorGUILayout.ObjectField(i.ToString(), list[i], typeof(T), false) as T;

        EditorGUI.indentLevel--;
    }

    public static void Save(GameObject toSave, string baseFolder)
    {
        GameObject eventPrefab = PrefabUtility.InstantiatePrefab(toSave) as GameObject;
        PrefabUtility.ReplacePrefab(eventPrefab, toSave, ReplacePrefabOptions.ReplaceNameBased);
        GameObject.DestroyImmediate(eventPrefab, true);

        AssetDatabase.ImportAsset(baseFolder + toSave.name);
        AssetDatabase.SaveAssets();
    }
}
