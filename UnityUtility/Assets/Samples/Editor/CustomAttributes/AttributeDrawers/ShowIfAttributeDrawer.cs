using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityUtility.CustomAttributes.Utils;
using System;

namespace UnityUtility.CustomAttributes.Drawers
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfAttributeDrawer : PropertyDrawer
    {
        private bool m_conditionSucess;

        private SerializedProperty[] m_properties = null;
        private PropertyField m_propertyField = null;
        private ShowIfAttribute m_showIfAttribute = null;

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
            if (attribute is ShowIfAttribute showIfAttribute)
            {
                m_conditionSucess = AttributeUtils.ConditionSucessFromFieldOrProperty(
                    property,
                    showIfAttribute.FieldName,
                    showIfAttribute.CompareValue,
                    showIfAttribute.Inverse);

                if (m_conditionSucess)
                {
                    _ = EditorGUILayout.PropertyField(property, label);
                }
            }
        }
        /*
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new VisualElement();

            m_properties ??= new SerializedProperty[property.isArray ? property.arraySize : 1];
            m_properties = property;

            if (attribute is ShowIfAttribute showIfAttribute)
            {
                m_showIfAttribute = showIfAttribute;
                m_propertyField = new PropertyField(property);

                SerializedProperty conditionProperty = property.serializedObject.FindProperty(showIfAttribute.FieldName);
                PropertyField conditionField = new PropertyField(conditionProperty);
                conditionField.RegisterValueChangeCallback(OnConditionValueChanged);
                conditionField.style.display = DisplayStyle.None;
                container.Add(conditionField);

                m_conditionSucess = AttributeUtils.ConditionSucessFromFieldOrProperty(
                    property,
                    showIfAttribute.FieldName,
                    showIfAttribute.CompareValue,
                    showIfAttribute.Inverse);

                m_propertyField.style.display = m_conditionSucess ? DisplayStyle.Flex : DisplayStyle.None;

                container.Add(m_propertyField);
            }
            return container;
        }
        
        private void OnConditionValueChanged(SerializedPropertyChangeEvent evt)
        {
            m_conditionSucess = AttributeUtils.ConditionSucessFromFieldOrProperty(
                m_properties,
                m_showIfAttribute.FieldName,
                m_showIfAttribute.CompareValue,
                m_showIfAttribute.Inverse);

            m_propertyField.style.display = m_conditionSucess ? DisplayStyle.Flex : DisplayStyle.None;
        }
        */
    }
}
