#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomPropertyDrawer(typeof(SerializedDictionary<,>))]
public class SerializedDictionaryEditor : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        SerializedProperty keyValuePairsList = property.FindPropertyRelative("m_keyValuePairsList");
        VisualElement container = new VisualElement();
        container.Add(new PropertyField(keyValuePairsList));
        container.Add(new Label("Test"));
        return container;
    }
}
#endif