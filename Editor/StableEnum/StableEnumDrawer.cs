// Based on DotSquid's "StableEnum" package : https://github.com/dotsquid/StableEnum

using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;


namespace UnityUtility.Editor
{

    [CustomPropertyDrawer(typeof(StableEnum<>), true)]
    public class StableEnumDrawer : PropertyDrawer
    {
        private enum A
        {

        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProperty = property.FindPropertyRelative(StableEnum<A>.VALUE_FIELD_NAME);
            EditorGUILayout.PropertyField(valueProperty, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 0.0f;
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            SerializedProperty valueProperty = property.FindPropertyRelative(StableEnum<A>.VALUE_FIELD_NAME);
            return new PropertyField(valueProperty);
        }
    }
}
