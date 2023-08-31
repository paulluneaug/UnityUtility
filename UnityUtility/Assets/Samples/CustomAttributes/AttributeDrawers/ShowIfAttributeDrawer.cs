using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityUtility.CustomAttributes.Utils;

namespace UnityUtility.CustomAttributes.Drawers
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfAttributeDrawer : PropertyDrawer
    {
        private bool m_conditionSucess;
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

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new VisualElement();
            if (attribute is ShowIfAttribute showIfAttribute)
            {
                m_conditionSucess = AttributeUtils.ConditionSucessFromFieldOrProperty(
                    property,
                    showIfAttribute.FieldName,
                    showIfAttribute.CompareValue,
                    showIfAttribute.Inverse);

                if (m_conditionSucess)
                {
                    container.Add(new PropertyField(property));
                }
            }
            return container;
        }
    }
}
