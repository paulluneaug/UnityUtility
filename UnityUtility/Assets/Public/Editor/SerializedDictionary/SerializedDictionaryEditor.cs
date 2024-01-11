#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Text.RegularExpressions;
using UnityUtility.Editor;

namespace UnityUtility.SerializedDictionary.Editor
{
    [CustomPropertyDrawer(typeof(SerializedDictionary<,>))]
    public class SerializedDictionaryEditor : PropertyDrawer
    {
        private VisualElement m_container;
        private SerializedProperty m_property;
        SerializedProperty m_pairListProperty;
        private PropertyField m_listField;

        private int iterations = 0;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            m_container = new VisualElement();
            m_property = property;
            m_pairListProperty = m_property.FindPropertyRelative("m_keyValuePairsList");
            FillContainer();

            return m_container;
        }

        private void FillContainer()
        {
            m_listField?.UnregisterCallback<SerializedPropertyChangeEvent>(OnValueChanged);

            m_listField = new PropertyField(m_pairListProperty);
            m_listField.label = m_property.displayName;
            m_listField.RegisterCallback<SerializedPropertyChangeEvent>(OnValueChanged);
            m_container.Add(m_listField);
        }

        private void OnValueChanged(SerializedPropertyChangeEvent evt)
        {
            FillContainer();
        }
    }

    [CustomPropertyDrawer(typeof(SerializedDictionary<,>.KeyValuePair))]
    public class SerializedDictionaryKeyValuePairEditor : PropertyDrawer
    {
        [SerializeField] private VisualTreeAsset m_keyValuePairTemplate;


        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            _ = EditorUtils.IsPropertyPartOfArray(property, out SerializedProperty arrayProperty, out int index);
            return new SerializedDictionaryPairVisualElement(property, arrayProperty, index);
        }
    }
}
#endif