using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;

using UnityUtility.MathU;


namespace UnityUtility.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(NoFoldoutAttribute))]
    public class NoFoldoutAttributeDrawer : PropertyDrawer
    {


        #region GUI

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 0.0f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _ = EditorGUI.BeginProperty(position, label, property);

            SerializedProperty childProperty = property.Copy();
            SerializedProperty nextProperty = property.Copy();

            bool hasNextChild = childProperty.NextVisible(true);
            bool hasNextProperty = nextProperty.NextVisible(false);

            bool hasChildren = hasNextChild && (!hasNextProperty || childProperty != nextProperty);

            if (!hasChildren)
            {
                _ = EditorGUILayout.PropertyField(property);
                EditorGUI.EndProperty();
                return;
            }

            EditorGUILayout.LabelField(property.displayName);

            EditorGUI.indentLevel++;

            do
            {
                _ = EditorGUILayout.PropertyField(childProperty);
            }
            while (childProperty.NextVisible(false) && (!hasNextProperty || childProperty != nextProperty));

            EditorGUI.indentLevel--;

            EditorGUI.EndProperty();
        }

        #endregion

#region VisualElement
#if false
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new VisualElement();

            SerializedProperty childProperty = property.Copy();
            SerializedProperty nextProperty = property.Copy();

            bool hasNextChild = childProperty.Next(true);
            bool hasNextProperty = nextProperty.Next(false);

            bool hasChildren = hasNextChild && (!hasNextProperty || childProperty != nextProperty);

            if (!hasChildren)
            {
                _ = EditorGUILayout.PropertyField(property);
                return;
            }

            EditorGUILayout.LabelField(property.displayName);

            EditorGUI.indentLevel++;

            do
            {
                _ = EditorGUILayout.PropertyField(childProperty);
            }
            while (childProperty.Next(false) && (!hasNextProperty || childProperty != nextProperty));

            EditorGUI.indentLevel--;

            return container;
        }
#endif
#endregion
    }
}
