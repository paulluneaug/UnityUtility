using System;

using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;

namespace UnityUtility.SerializedDictionary.Editor
{
    public class SerializedDictionaryPairVisualElement : VisualElement
    {
        public Action OnKeyChanged;

        private const string VISUAL_ASSET_TREE_TEMPLATE_NAME = "DictionaryPairTemplate";

        private const string FOLDOUT_TOGGLE_NAME = "FoldoutToggle";
        private const string KEY_FIELD_NAME = "KeyField";
        private const string VALUE_CONTAINER_NAME = "ValueContainer";
        private const string VALUE_FIELD_NAME = "ValueField";

        private const string EVEN_INDEX_COLOR_SELECTOR_NAME = "evenIndexColor";
        private const string ODD_INDEX_COLOR_SELECTOR_NAME = "oddIndexColor";
        private const string INVALID_KEY_SELECTOR_NAME = "invalidKey";
        private const string HAS_PROPERTY_CHILDREN_SELECTOR_NAME = "hasPropertyChildren";

        private readonly Toggle m_foldoutToggle;
        private readonly VisualElement m_valueContainer;

        private readonly PropertyField m_keyPropertyField;
        private readonly PropertyField m_valuePropertyField;

        private readonly SerializedProperty m_keyProperty;
        private readonly SerializedProperty m_valueProperty;
        private readonly SerializedProperty m_parentProperty;

        private readonly int m_indexInParent;

        public SerializedDictionaryPairVisualElement(SerializedProperty pairProperty, SerializedProperty parentProperty, int pairIndex)
        {
            VisualTreeAsset keyValuePairDrawer = Resources.Load<VisualTreeAsset>(VISUAL_ASSET_TREE_TEMPLATE_NAME);
            keyValuePairDrawer.CloneTree(this);

            bool isIndexEven = pairIndex % 2 == 0;
            this.EnableInClassList(EVEN_INDEX_COLOR_SELECTOR_NAME, isIndexEven);
            this.EnableInClassList(ODD_INDEX_COLOR_SELECTOR_NAME, !isIndexEven);

            m_foldoutToggle = this.Q<Toggle>(FOLDOUT_TOGGLE_NAME);
            m_foldoutToggle.RegisterCallback<ChangeEvent<bool>>(OnFoldoutToggleClicked);
            m_foldoutToggle.value = false;

            m_valueContainer = this.Q<VisualElement>(VALUE_CONTAINER_NAME);
            m_valueContainer.style.display = m_foldoutToggle.value ? DisplayStyle.Flex : DisplayStyle.None;

            m_keyProperty = pairProperty.FindPropertyRelative("Key");
            m_valueProperty = pairProperty.FindPropertyRelative("Value");
            m_parentProperty = parentProperty;
            m_indexInParent = pairIndex;

            m_keyPropertyField = this.Q<PropertyField>(KEY_FIELD_NAME);
            m_keyPropertyField.BindProperty(m_keyProperty);
            m_keyPropertyField.RegisterValueChangeCallback(OnKeyValueChanged);
            m_keyPropertyField.EnableInClassList(HAS_PROPERTY_CHILDREN_SELECTOR_NAME, m_keyProperty.hasVisibleChildren);

            m_valuePropertyField = this.Q<PropertyField>(VALUE_FIELD_NAME);
            m_valuePropertyField.BindProperty(m_valueProperty);
            m_valuePropertyField.EnableInClassList(HAS_PROPERTY_CHILDREN_SELECTOR_NAME, m_valueProperty.hasVisibleChildren);

            CheckKeyDuplication();
        }

        ~SerializedDictionaryPairVisualElement()
        {
            Dispose();
        }

        private void Dispose()
        {
            m_keyPropertyField.Unbind();
            m_valuePropertyField.Unbind();
            m_foldoutToggle.UnregisterCallback<ChangeEvent<bool>>(OnFoldoutToggleClicked);
            m_keyPropertyField.UnregisterCallback<SerializedPropertyChangeEvent>(OnKeyValueChanged);
        }

        private void OnKeyValueChanged(SerializedPropertyChangeEvent evt)
        {
            CheckKeyDuplication();
            OnKeyChanged?.Invoke();
        }

        private void OnFoldoutToggleClicked(ChangeEvent<bool> changeEvent)
        {
            m_valueContainer.style.display = changeEvent.newValue ? DisplayStyle.Flex : DisplayStyle.None;
        }

        public void CheckKeyDuplication()
        {
            bool keyIsString = m_keyProperty.type.Equals("string");
            bool foundInvalidKey = false;
            for (int i = 0; i < m_parentProperty.arraySize; i++)
            {
                SerializedProperty otherKey = m_parentProperty.GetArrayElementAtIndex(i).FindPropertyRelative("Key");

                if (i != m_indexInParent && (
                    !keyIsString && SerializedProperty.DataEquals(m_keyProperty, otherKey) ||
                    keyIsString && m_keyProperty.stringValue.Equals(otherKey.stringValue)))
                {
                    foundInvalidKey = true;
                    break;
                }

            }
            m_keyPropertyField.EnableInClassList(INVALID_KEY_SELECTOR_NAME, foundInvalidKey);
        }
    }
}