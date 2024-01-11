using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace UnityUtility.Editor
{
    public static class EditorUtils
    {
        public static string ReplaceLast(string str, string oldValue, string newValue)
        {
            int place = str.LastIndexOf(oldValue);

            if (place == -1)
            {
                return str;
            }

            return str.Remove(place, oldValue.Length).Insert(place, newValue);
        }

        public static bool IsPropertyPartOfArray(SerializedProperty property, out SerializedProperty arrayProperty, out int index)
        {
            // Get the index
            string indexText = Regex.Match(property.propertyPath, @"(\d+)(?!.*\d)").Value;
            if (string.IsNullOrEmpty(indexText))
            {
                arrayProperty = null;
                index = -1;
                return false;
            }

            // Get the parent's propertyPath
            string parentPath = ReplaceLast(property.propertyPath, $".Array.data[{indexText}]", string.Empty);

            arrayProperty = property.serializedObject.FindProperty(parentPath);
            index = int.Parse(indexText);
            return true;
        }
    }
}
