using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

using UnityEditor;

using UnityEngine;
using UnityEngine.UIElements;

using UnityUtility.Utils;

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

        /// <summary>
        /// Creates and returns a <see cref="VisualElement"/> that can be used to separate fields in the inspector
        /// </summary>
        /// <returns></returns>
        public static VisualElement CreateSeparator()
        {
            VisualElement separator = new VisualElement();
            separator.style.backgroundColor = s_separatorColor;
            separator.style.height = 1;
            separator.name = "Separator";
            return separator;
        }

        /// <summary>
        /// Given a type and a member name, tries to find the <see cref="FieldInfo"/> or <see cref="PropertyInfo"/> of this member, wrapped in a <see cref="IMemberConditionInfo"/>
        /// </summary>
        private static bool TryGetNestedChildMemberInfos(Type parentType, string memberName, int arrayIndex, IMemberConditionInfo parentMemberInfo, out IMemberConditionInfo childMemberInfos)
        {
            FieldInfo conditionFieldInfo = parentType.GetField(memberName, DEFAULT_BINDING_FLAGS);
            if (conditionFieldInfo != null)
            {
                if (arrayIndex == -1)
                {
                    childMemberInfos = new FieldConditionInfos(conditionFieldInfo, parentMemberInfo);
                }
                else
                {
                    childMemberInfos = new ArrayFieldConditionInfos(conditionFieldInfo, arrayIndex, parentMemberInfo);
                }
                return true;
            }

            PropertyInfo conditionPropertyInfo = parentType.GetProperty(memberName, DEFAULT_BINDING_FLAGS);
            if (conditionPropertyInfo != null)
            {
                if (arrayIndex == -1)
                {
                    childMemberInfos = new PropertyConditionInfos(conditionPropertyInfo, parentMemberInfo);
                }
                else
                {
                    childMemberInfos = new ArrayPropertyConditionInfos(conditionPropertyInfo, arrayIndex, parentMemberInfo);
                }
                return true;
            }
            Debug.LogError($"No field nor property named {memberName} in the type {parentType.Name}");
            childMemberInfos = null;
            return false;

        }

        private static string FormatArrayData(string pathStep)
        {
            Match arrayIndexMatch = Regex.Match(pathStep, "(?<=data\\[)[0-9]*(?=\\])");
            if (arrayIndexMatch.Success)
            {
                return arrayIndexMatch.Value;
            }
            return pathStep;
        }

        private static IEnumerable<(string memberName, int arrayIndex)> CurateArrayMembers(string[] splittedPath)
        {
            splittedPath = splittedPath.Select(FormatArrayData).ToArray();
            for (int i = 0; i < splittedPath.Length; ++i)
            {
                string memberName = splittedPath[i];
                if (StringUtils.IsInteger(memberName))
                {
                    continue;
                }

                if (i < splittedPath.Length - 1)
                {
                    if (string.Equals(memberName, "Array") && StringUtils.IsInteger(splittedPath[i + 1]))
                    {
                        continue;
                    }
                }


                if (i < splittedPath.Length - 2)
                {
                    if (string.Equals(splittedPath[i + 1], "Array") && int.TryParse(splittedPath[i + 2], out int arrayIndex))
                    {
                        yield return (memberName, arrayIndex);
                        continue;
                    }
                }

                yield return (memberName, -1);

            }
        }

        /// <summary>
        /// Tries to find a member in the <see cref="SerializedObject"/> of a <see cref="SerializedProperty"/> 
        /// </summary>
        public static bool TryGetNestedMemberInfosChain(SerializedProperty property, string memberName, out IMemberConditionInfo memberInfos)
        {
            Type parentObjectType = property.serializedObject.targetObject.GetType();

            string[] splittedPropertyPath = property.propertyPath.Split('.');
            (string memberName, int arrayIndex)[] memberHierarchy = CurateArrayMembers(splittedPropertyPath).ToArray();

            Type parentType = parentObjectType;

            IMemberConditionInfo parentMemberInfo = null;

            for (int i = 0; i < memberHierarchy.Length - 1; i++)
            {
                (string nestedMemberName, int arrayIndex) = memberHierarchy[i];

                if (!TryGetNestedChildMemberInfos(parentType, nestedMemberName, arrayIndex, parentMemberInfo, out IMemberConditionInfo childInfos))
                {
                    memberInfos = null;
                    return false;
                }
                parentMemberInfo = childInfos;
                parentType = childInfos.GetMemberType();
            }

            if (TryGetNestedChildMemberInfos(parentType, memberName, -1, parentMemberInfo, out memberInfos))
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
            if (bold)
            {
                return FontStyle.Bold;
            }
            if (italic)
            {
                return FontStyle.Italic;
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
