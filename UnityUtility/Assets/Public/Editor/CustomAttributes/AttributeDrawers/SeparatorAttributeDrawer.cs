using UnityEditor;

using UnityEngine;
using UnityEngine.UIElements;

namespace UnityUtility.CustomAttributes.Editor
{
    [CustomPropertyDrawer(typeof(SeparatorAttribute))]
    public class SeparatorAttributeDrawer : DecoratorDrawer
    {
        public override VisualElement CreatePropertyGUI()
        {
            VisualElement separator = new VisualElement
            {
                name = "Separator"
            };
            separator.style.height = EditorGUIUtility.singleLineHeight;
            separator.style.justifyContent = Justify.Center;

            separator.Add(base.CreatePropertyGUI());
            separator.Add(AttributeUtils.CreateSeparator());
            return separator;
        }

        public override void OnGUI(Rect position)
        {
            base.OnGUI(position);
        }
    }
}

