using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;

using UnityUtility.MathU;


namespace UnityUtility.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(EditInlineAttribute))]
    public class EditInlineAttributeDrawer : PropertyDrawer
    {
        private const string WRONG_PROPERTY_TYPE_ERROR = nameof(EditInlineAttribute) + " can only be applied to objects derived from " + nameof(ScriptableObject);


        #region GUI

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 0.0f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _ = EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                WrongPropertyTypeBox();
                _ = EditorGUILayout.PropertyField(property, label);
                return;
            }

            if (property.objectReferenceValue == null)
            {
                _ = EditorGUILayout.PropertyField(property, label);
                return;
            }

            if (property.objectReferenceValue is not ScriptableObject target)
            {
                WrongPropertyTypeBox();
                _ = EditorGUILayout.PropertyField(property, label);
                return;
            }

            Rect foldoutRect = new Rect()
            {
                x = position.x,
                y = position.y + 2,
                width = EditorGUIUtility.labelWidth,
                height = EditorGUIUtility.singleLineHeight
            };

            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label, toggleOnLabelClick: true);

            _ = EditorGUILayout.PropertyField(property, label, false);

            if (!property.isExpanded)
            {
                return;
            }

            using SerializedObject serializedTarget = new SerializedObject(target);

            SerializedProperty childProperty = serializedTarget.GetIterator();

            _ = childProperty.NextVisible(true); // Base
            _ = childProperty.NextVisible(true); // Script

            EditorGUI.indentLevel++;

            do
            {
                _ = EditorGUILayout.PropertyField(childProperty);
            }
            while (childProperty.NextVisible(false));

            EditorGUI.indentLevel--;

            _ = serializedTarget.ApplyModifiedProperties();

            EditorGUI.EndProperty();
        }

        private void WrongPropertyTypeBox()
        {
            EditorGUILayout.HelpBox(WRONG_PROPERTY_TYPE_ERROR, MessageType.Error);
        }

        #endregion

        #region VisualElement

#if false

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new VisualElement();

            PropertyField objectField = new PropertyField(property);

            Foldout foldout = new Foldout();
            foldout.text = property.displayName;

            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                container.Add(AttributeUtils.GetWrongTypeHelpBox(property, typeof(EditInlineAttribute)));
                container.Add(objectField);
                return container;
            }

            container.Add(objectField);

            Object traget = property.objectReferenceValue;

            if (traget == null)
            {
                return container;
            }

            // Main thread hook
            //EditorApplication.update += OnEditorUpdate;
            FillFoldout(property, foldout);
            foldout.Add(objectField);

            container.Add(foldout);

            return container;
        }

        private void FillFoldout(SerializedProperty property, Foldout foldout)
        {
            using SerializedObject serializedTarget = new SerializedObject(property.objectReferenceValue);
            serializedTarget.Update();
            SerializedProperty targetProperty = serializedTarget.GetIterator();

            _ = targetProperty.NextVisible(true); // Base
            _ = targetProperty.NextVisible(true); //Script

            do
            {
                foldout.Add(new PropertyField(targetProperty));
            }
            while (targetProperty.NextVisible(false));

            foldout.MarkDirtyRepaint();

            _ = serializedTarget.ApplyModifiedProperties();
        }
#endif
        #endregion
    }
}
