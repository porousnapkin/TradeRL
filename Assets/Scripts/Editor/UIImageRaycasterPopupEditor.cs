using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

[CustomEditor(typeof(UIImageRaycasterPopup))]
public class UIImageRaycasterPopupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UIImageRaycasterPopup p = target as UIImageRaycasterPopup;
        p.defaultText = EditorGUILayout.TextArea(p.defaultText);
    }
}
