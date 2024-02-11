using System.Text.RegularExpressions;
using UnityEditor;

namespace UnityUtility.Utils.Editor
{
    public static class EditorUtils
    {
        /// <summary>
        /// Checks wether a <see cref="SerializedProperty"/> is the child of an array property and if so,
        /// outputs the <paramref name="arrayProperty"/> and the <paramref name="index"/> of the given <paramref name="property"/> in the array 
        /// </summary>
        /// 
        /// <param name="property"></param>
        /// <param name="arrayProperty">If <paramref name="property"/> is part of an array, the <see cref="SerializedProperty"/> og the array</param>
        /// <param name="index">If <paramref name="property"/> is part of an array, its index in the array</param>
        /// <returns>Wether <paramref name="property"/> is part of an array property</returns>
        public static bool IsPropertyPartOfArray(this SerializedProperty property, out SerializedProperty arrayProperty, out int index)
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
            string parentPath = property.propertyPath.ReplaceLast($".Array.data[{indexText}]", string.Empty);

            arrayProperty = property.serializedObject.FindProperty(parentPath);
            index = int.Parse(indexText);
            return true;
        }

        /// <summary>
        /// Tries to find the <paramref name="parentProperty"/> of the given <paramref name="property"/>.
        /// <para>
        /// If none can be found, returns <see langword="false"/> and <paramref name="parentProperty"/> is <see langword="null"/>
        /// </para>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="parentProperty">If the <paramref name="property"/> is a child property, the <paramref name="parentProperty"/></param>
        /// <returns>Wether a <paramref name="parentProperty"/> has been found</returns>
        public static bool TryGetParentProperty(this SerializedProperty property, out SerializedProperty parentProperty)
        {
            if (property.IsPropertyPartOfArray(out parentProperty, out _))
            {
                return true;
            }

            string[] splittedPath = property.propertyPath.Split('.');
            if (splittedPath.Length > 1)
            {
                string parentPath = string.Join('.', splittedPath[..1]);

                parentProperty = property.serializedObject.FindProperty(parentPath);
                return true;
            }

            return false;
        }
    }
}
