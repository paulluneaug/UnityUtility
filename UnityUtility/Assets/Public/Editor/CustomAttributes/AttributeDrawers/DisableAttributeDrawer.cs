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
        private PropertyField m_propertyField;
        private object m_savedValue;

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
            m_savedValue = property.boxedValue;
            m_propertyField = new PropertyField(property);
            m_propertyField.RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            m_propertyField.RegisterValueChangeCallback(OnValueChanged);
            return m_propertyField;
        }

        private void OnValueChanged(SerializedPropertyChangeEvent evt)
        {
            evt.changedProperty.boxedValue = m_savedValue;
        }

        private void OnAttachToPanel(AttachToPanelEvent evt)
        {
            m_propertyField.UnregisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            m_propertyField.EnableInClassList(AttributeUtils.DISABLED_SELECTOR_NAME, true);
            m_propertyField.EnableInClassList("--disabled", true);

            //m_propertyField.re = false;
        }
        #endregion
    }
}
