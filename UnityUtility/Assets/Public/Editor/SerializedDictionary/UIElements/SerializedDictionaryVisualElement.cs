using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityUtility.SerializedDictionary.Editor
{
    public class SerializedDictionaryVisualElement : VisualElement
    {
        public Action OnKeyChanged;

        private const string VISUAL_ASSET_TREE_TEMPLATE_PATH = @"Assets/Samples/SerializedDictionary/Editor/UIElements/DictionaryTemplate.uxml";

        private const string PAIRS_CONTAINER_NAME = "PairsContainer";
        private const string KEY_FIELD_NAME = "KeyField";
        private const string VALUE_CONTAINER_NAME = "ValueContainer";
        private const string VALUE_FIELD_NAME = "ValueField";

        private const string EVEN_INDEX_COLOR_SELECTOR_NAME = "evenIndexColor";
        private const string ODD_INDEX_COLOR_SELECTOR_NAME = "oddIndexColor";
        private const string INVALID_KEY_SELECTOR_NAME = "invalidKey";
        private const string HAS_PROPERTY_CHILDREN_SELECTOR_NAME = "hasPropertyChildren";

        private readonly Toggle m_foldoutToggle;
        private readonly VisualElement m_valueContainer;
        private readonly ListView m_pairsContainer;

        private readonly PropertyField m_keyPropertyField;
        private readonly PropertyField m_valuePropertyField;

        private readonly SerializedProperty m_keyProperty;
        private readonly SerializedProperty m_valueProperty;
        private readonly SerializedProperty m_parentProperty;

        private readonly int m_indexInParent;

        public SerializedDictionaryVisualElement(SerializedProperty property)
        {
            SerializedProperty pairListProperty = property.FindPropertyRelative("m_keyValuePairsList");
            PropertyField listField = new PropertyField(pairListProperty);
            listField.RegisterCallback<SerializedPropertyChangeEvent>(OnValueChanged);
            Add(listField);
            //m_pairsContainer.
            //for (int i = 0; i < pairListProperty.arraySize; i++)
            //{
            //    m_pairsContainer.Add(new SerializedDictionaryPairVisualElement(pairListProperty.GetArrayElementAtIndex(i), pairListProperty, i));
            //}
        }

        private void OnValueChanged(SerializedPropertyChangeEvent evt)
        {
            Debug.LogWarning("Value Changed");
        }

        ~SerializedDictionaryVisualElement()
        {
            Dispose();
        }

        private void Dispose()
        {
        }

        private void OnKeyValueChanged(SerializedPropertyChangeEvent evt)
        {
            OnKeyChanged?.Invoke();
        }

        private void OnFoldoutToggleClicked(ChangeEvent<bool> changeEvent)
        {
            m_valueContainer.style.display = changeEvent.newValue ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}