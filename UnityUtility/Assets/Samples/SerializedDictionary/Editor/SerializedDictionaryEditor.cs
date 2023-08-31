#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using Unity.VisualScripting;
using System.Reflection;
using System.Text.RegularExpressions;

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
            m_listField.RegisterCallback<SerializedPropertyChangeEvent>(OnValueChanged);
            m_container.Add(m_listField);

            Debug.LogError($"Filled Schedule {iterations++} times");
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
        private static void GetParentAndElementIndex(SerializedProperty property, out SerializedProperty parent, out int index)
        {
            // Get the index
            string indexText = Regex.Match(property.propertyPath, @"(\d+)(?!.*\d)").Value;

            // Get the parent's propertyPath
            string parentPath = ReplaceLast(property.propertyPath, $".Array.data[{indexText}]", string.Empty);

            parent = property.serializedObject.FindProperty(parentPath);
            index = int.Parse(indexText);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            GetParentAndElementIndex(property, out SerializedProperty parent, out int index);
            return new SerializedDictionaryPairVisualElement(property, parent, index);
        }

        public static string ReplaceLast(string str, string oldValue, string newValue)
        {
            int place = str.LastIndexOf(oldValue);

            if (place == -1)
            {
                return str;
            }

            return str.Remove(place, oldValue.Length).Insert(place, newValue);
        }
    }
}
#endif