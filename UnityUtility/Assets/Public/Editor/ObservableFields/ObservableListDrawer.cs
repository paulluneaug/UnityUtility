using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace UnityUtility.ObservableFields.Editor
{
    [CustomPropertyDrawer(typeof(ObservableList<>))]
    public class ObservableListDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            SerializedProperty underlyingListProperty = property.FindPropertyRelative(ObservableList<int>.UNDERLYING_LIST_NAME);
            return new PropertyField(underlyingListProperty, property.displayName);
        }
    }
}
