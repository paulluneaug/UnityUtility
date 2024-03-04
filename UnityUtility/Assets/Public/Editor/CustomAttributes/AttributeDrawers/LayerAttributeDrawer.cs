using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityUtility.CustomAttributes.Editor
{
    [CustomPropertyDrawer(typeof(LayerAttribute))]
    public class LayerAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.intValue = EditorGUI.LayerField(position, label, property.intValue);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            LayerField field = new LayerField(property.displayName);
            field.labelElement.style.width = AttributeUtils.LabelWidth;
            field.BindProperty(property);
            return field;
        }
    }
}

