using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;

namespace UnityUtility.CustomAttributes.Editor
{
    [CustomPropertyDrawer(typeof(TagAttribute))]
    public class TagAttributeDrawer : PropertyDrawer
    {
        #region IMGUI
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
        }
        #endregion

        #region UIElements
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                return AttributeUtils.GetWrongTypeHelpBox(property, typeof(TagAttribute));
            }

            TagField field = new TagField(property.displayName);
            field.labelElement.style.width = AttributeUtils.LabelWidth;
            field.BindProperty(property);
            return field;
        }
        #endregion
    }
}

