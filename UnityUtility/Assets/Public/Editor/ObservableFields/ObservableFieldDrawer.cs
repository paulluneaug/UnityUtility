using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine.UIElements;

namespace UnityUtility.ObservableFields.Editor
{
    [CustomPropertyDrawer(typeof(ObservableField<>))]
    public class ObservableFieldDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            SerializedProperty underlyingValueProperty = property.FindPropertyRelative(ObservableField<int>.UNDERLYING_VALUE_NAME);
            return new PropertyField(underlyingValueProperty, property.displayName);
        }
    }
}
