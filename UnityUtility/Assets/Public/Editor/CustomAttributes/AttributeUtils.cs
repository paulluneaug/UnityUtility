using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityUtility.CustomAttributes.Editor
{
    public static class AttributeUtils
    {
        public const string WRONG_TYPE_ERROR_FMT = "{0} cannot be applied to variables of type {1}";

        public static Length LabelWidth => s_labelWidth;

        public static Color SeparatorColor => s_separatorColor;

        private static readonly Length s_labelWidth = Length.Percent(42);

        private static readonly Color s_separatorColor = new Color(0.3515625f, 0.3515625f, 0.3515625f);

        public static VisualElement CreateSeparator()
        {
            VisualElement line = new VisualElement();
            line.style.backgroundColor = s_separatorColor;
            line.style.height = 1;
            line.name = "Line";
            return line;
        }

        public static bool ConditionSucessFromFieldOrProperty(SerializedProperty property, string fieldName, object compareValue, bool inverse = false)
        {
            Type parentType = property.serializedObject.targetObject.GetType();

            FieldInfo conditionField = parentType.GetField(
                fieldName,
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);

            PropertyInfo conditionProperty = parentType.GetProperty(
                fieldName,
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);

            Type conditionType;
            object conditionValue;

            if (conditionField != null)
            {
                conditionType = conditionField.FieldType;
                conditionValue = conditionField.GetValue(property.serializedObject.targetObject);
            }
            else if (conditionProperty != null)
            {
                conditionType = conditionProperty.PropertyType;
                conditionValue = conditionProperty.GetValue(property.serializedObject.targetObject);
            }
            else
            {
                Debug.LogError($"[{property.serializedObject.targetObject.GetType()}] Could not find a field or a property named {fieldName}");
                return false;
            }

            if (conditionType == typeof(bool))
            {
                bool? condition = (bool?)conditionValue;
                if (condition.HasValue)
                {
                    return condition.Value != inverse;
                }
            }
            else
            {
                if (conditionValue != null && compareValue != null)
                {
                    return Convert.ChangeType(compareValue, conditionValue.GetType()).Equals(conditionValue) != inverse;
                }
            }

            Debug.LogError($"[{property.serializedObject.targetObject.GetType()}] Invalid Condition");
            return false;
        }

        public static FontStyle GetFontStyle(bool bold, bool italic)
        {
            if (bold && italic)
            {
                return FontStyle.BoldAndItalic;
            }
            else
            {
                if (bold)
                {
                    return FontStyle.Bold;
                }
                else if (italic)
                {
                    return FontStyle.Italic;
                }
            }

            return FontStyle.Normal;
        }

        public static VisualElement GetWrongTypeHelpBox(SerializedProperty property, Type attributeType)
        {
            VisualElement container = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;
            Label propertyLabel = new Label(property.displayName);
            propertyLabel.style.width = LabelWidth;
            container.Add(propertyLabel);

            container.Add(new HelpBox(GetWrongTypeMessage(property, attributeType), HelpBoxMessageType.Error));
            return container;
        }

        public static string GetWrongTypeMessage(SerializedProperty property, Type attributeType)
        {
            return string.Format(WRONG_TYPE_ERROR_FMT, attributeType.Name, property.type);
        }
    }
}
