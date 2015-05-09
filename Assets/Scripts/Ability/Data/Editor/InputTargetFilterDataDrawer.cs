using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(InputTargetFilterData), true)]
class InputTargetFilterDataDrawer : PropertyDrawer {
	Editor myEditor;

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty (position, label, property);

        var baseScriptableObject = property.serializedObject.targetObject as ScriptableObject;
        var filter = property.objectReferenceValue as InputTargetFilterData;

        property.objectReferenceValue = EditorHelper.DisplayScriptableObjectWithEditor(baseScriptableObject, filter, myEditor, label.text);

        EditorGUI.EndProperty ();
    }
}
