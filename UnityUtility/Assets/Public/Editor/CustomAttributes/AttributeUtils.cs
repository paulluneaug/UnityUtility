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
        private const BindingFlags DEFAULT_BINDING_FLAGS = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public;

        public static Length LabelWidth => s_labelWidth;

        public static Color SeparatorColor => s_separatorColor;

        private static readonly Length s_labelWidth = Length.Percent(42);

        private static readonly Color s_separatorColor = new Color(0.3515625f, 0.3515625f, 0.3515625f);

        public static VisualElement CreateSeparator()
        {
            VisualElement separator = new VisualElement();
            separator.style.backgroundColor = s_separatorColor;
            separator.style.height = 1;
            separator.name = "Separator";
            return separator;
        }

        private static bool TryGetNestedChildMemberInfos(Type parentType, string memberName, IMemberConditionInfo parentMemberInfo, out IMemberConditionInfo childMemberInfos)
        {
            FieldInfo conditionFieldInfo = parentType.GetField(memberName, DEFAULT_BINDING_FLAGS);
            if (conditionFieldInfo != null)
            {
                childMemberInfos = new FieldConditionInfos(conditionFieldInfo, parentMemberInfo);
                return true;
            }

            PropertyInfo conditionPropertyInfo = parentType.GetProperty(memberName, DEFAULT_BINDING_FLAGS);
            if (conditionPropertyInfo != null)
            {
                childMemberInfos = new PropertyConditionInfos(conditionPropertyInfo, parentMemberInfo);
                return true;
            }
            Debug.LogError($"No field nor property named {memberName} in the type {parentType.Name}");
            childMemberInfos = null;
            return false;

        }

        public static bool TryGetNestedMemberInfosChain(SerializedProperty property, string memberName, out IMemberConditionInfo memberInfos)
        {
            Type parentObjectType = property.serializedObject.targetObject.GetType();

            string[] splittedPropertyPath = property.propertyPath.Split('.');

            Type parentType = parentObjectType;

            IMemberConditionInfo parentMemberInfo = null;

            for (int i = 0; i < splittedPropertyPath.Length - 1; i++)
            {
                string nestedMemberName = splittedPropertyPath[i];
                if (!TryGetNestedChildMemberInfos(parentType, nestedMemberName, parentMemberInfo, out IMemberConditionInfo childInfos))
                {
                    memberInfos = null;
                    return false;
                }
                parentMemberInfo = childInfos;
                parentType = childInfos.GetMemberType();
            }

            if (TryGetNestedChildMemberInfos(parentType, memberName, parentMemberInfo, out memberInfos))
            {
                return true;
            }
            return false;
        }

        public static bool ConditionSucessFromFieldOrProperty(SerializedProperty property, IMemberConditionInfo memberConditionInfos, object compareValue, bool inverse = false)
        {
            object conditionMemberValue = memberConditionInfos.GetValue(property.serializedObject.targetObject);

            if (memberConditionInfos.GetMemberType() == typeof(bool))
            {
                bool? condition = (bool?)conditionMemberValue;
                if (condition.HasValue)
                {
                    return condition.Value != inverse;
                }
            }
            else
            {
                if (conditionMemberValue != null && compareValue != null)
                {
                    return Convert.ChangeType(compareValue, conditionMemberValue.GetType()).Equals(conditionMemberValue) != inverse;
                }
            }

            Debug.LogError($"[{property.serializedObject.targetObject.GetType()}] Invalid Condition", property.serializedObject.targetObject);
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
