using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UnityUtility.CustomAttributes.Editor
{
    public static class AttributeUtils
    {
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
    }
}
