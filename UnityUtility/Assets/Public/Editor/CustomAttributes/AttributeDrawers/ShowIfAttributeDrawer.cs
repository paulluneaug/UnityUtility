using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;

using UnityUtility.Extensions.Editor;

namespace UnityUtility.CustomAttributes.Editor
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfAttributeDrawer : PropertyDrawer
    {
        private ShowIfAttribute m_showIfAttribute = null;

        private bool m_conditionSucess;

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
            if (attribute is ShowIfAttribute showIfAttribute)
            {
                if (AttributeUtils.TryGetNestedMemberInfosChain(property, showIfAttribute.FieldName, out IMemberConditionInfo memberInfos))
                {
                    m_conditionSucess = AttributeUtils.ConditionSucessFromFieldOrProperty(
                        property,
                        memberInfos,
                        showIfAttribute.CompareValue,
                        showIfAttribute.Inverse);
                }
                else
                {
                    m_conditionSucess = true;
                }

                if (m_conditionSucess)
                {
                    _ = EditorGUILayout.PropertyField(property, label);
                }
            }
        }
        #endregion

        #region UIElements
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new VisualElement();

            if (attribute is ShowIfAttribute showIfAttribute)
            {
                m_showIfAttribute = showIfAttribute;
                m_property = property;
                m_propertyField = new PropertyField(property);

                m_isPartOfArray = property.IsPropertyPartOfArray(out SerializedProperty _, out int _);

                container.Add(m_propertyField);

                EditorApplication.update += OnEditorUpdate;
            }
            return container;
        }

        private void OnEditorUpdate()
        {
            try
            {
                if (AttributeUtils.TryGetNestedMemberInfosChain(m_property, m_showIfAttribute.FieldName, out IMemberConditionInfo memberInfos))
                {
                    m_conditionSucess = AttributeUtils.ConditionSucessFromFieldOrProperty(
                        m_property,
                        memberInfos,
                        m_showIfAttribute.CompareValue,
                        m_showIfAttribute.Inverse);
                }
                else
                {
                    m_conditionSucess = true;
                }

                if (m_isPartOfArray)
                {
                    m_listProperty ??= m_propertyField.GetFirstAncestorOfType<ListView>();
                    m_listProperty.style.display = m_conditionSucess ? DisplayStyle.Flex : DisplayStyle.None;
                }
                else
                {
                    m_propertyField.style.display = m_conditionSucess ? DisplayStyle.Flex : DisplayStyle.None;
                }

            }
            catch
            {
                EditorApplication.update -= OnEditorUpdate;
                return;
            }
        }
        #endregion
    }
}
