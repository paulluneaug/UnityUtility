using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System;
using UnityUtility.Utils.Editor;

namespace UnityUtility.CustomAttributes.Editor
{
    [CustomPropertyDrawer(typeof(DisableIfAttribute))]
    public class DisableIfAttributeDrawer : PropertyDrawer
    {
        private DisableIfAttribute m_disableIfAttribute = null;

        private bool m_conditionSucess;
        private bool m_wasDisabled = false;

        private SerializedProperty m_property = null;

        private bool m_isPartOfArray = false;
        private ListView m_listProperty = null;

        private PropertyField m_propertyField = null;

        #region IMGUI
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (m_conditionSucess)
            {
                return base.GetPropertyHeight(property, label);
            }
            return 0.0f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (attribute is DisableIfAttribute disableIfAttribute)
            {
                m_conditionSucess = AttributeUtils.ConditionSucessFromFieldOrProperty(
                    property,
                    disableIfAttribute.FieldName,
                    disableIfAttribute.CompareValue,
                    disableIfAttribute.Inverse);

                EditorGUI.BeginDisabledGroup(m_conditionSucess);
                _ = EditorGUILayout.PropertyField(property, label);
                EditorGUI.EndDisabledGroup();
            }
        }
        #endregion

        #region UIElements
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new VisualElement();

            if (attribute is DisableIfAttribute disableIfAttribute)
            {
                m_disableIfAttribute = disableIfAttribute;
                m_property = property;
                m_propertyField = new PropertyField(property);

                m_isPartOfArray = property.IsPropertyPartOfArray(out SerializedProperty _, out int _);

                container.Add(m_propertyField);

                m_wasDisabled = false;

                EditorApplication.update += OnEditorUpdate;
            }
            return container;
        }

        private void OnEditorUpdate()
        {
            try
            {
                m_conditionSucess = AttributeUtils.ConditionSucessFromFieldOrProperty(
                    m_property,
                    m_disableIfAttribute.FieldName,
                    m_disableIfAttribute.CompareValue,
                    m_disableIfAttribute.Inverse);

                if (m_isPartOfArray)
                {
                    m_listProperty ??= m_propertyField.GetFirstAncestorOfType<ListView>();
                    DisableField(m_listProperty, m_conditionSucess);
                }
                else
                {
                    DisableField(m_propertyField, m_conditionSucess);
                }

            }
            catch (ArgumentNullException)
            {
                EditorApplication.update -= OnEditorUpdate;
                return;
            }
        }

        private void DisableField(VisualElement propertyField, bool disable)
        {
            if (m_wasDisabled ^ disable)
            {
                propertyField.SetEnabled(!disable);
                m_wasDisabled = disable;
            }
        }
        #endregion
    }
}
