using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System;
using UnityUtility.Utils.Editor;

namespace UnityUtility.CustomAttributes.Editor
{
    [CustomPropertyDrawer(typeof(DisableAttribute))]
    public class DisableAttributeDrawer : PropertyDrawer
    {
        #region IMGUI
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(property, label);
            EditorGUI.EndDisabledGroup();
        }
        #endregion

        #region UIElements
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            PropertyField propertyField = new PropertyField(property);
            propertyField.SetEnabled(false);
            return propertyField;
        }
        #endregion
    }
}
