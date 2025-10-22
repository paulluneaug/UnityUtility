using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;


namespace UnityUtility.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(EditInlineAttribute))]
    public class EditInlineAttributeDrawer : PropertyDrawer
    {


        #region VisualElement

        private Foldout m_foldout;
        private SerializedProperty m_property;


        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new VisualElement();

            m_property = property;
            PropertyField objectField = new PropertyField(property);

            m_foldout = new Foldout();
            m_foldout.text = property.displayName;

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
            FillFoldout(m_property, m_foldout);
            m_foldout.Add(objectField);

            container.Add(m_foldout);

            return container;
        }

        private void OnEditorUpdate()
        {
            EditorApplication.update -= OnEditorUpdate;
            FillFoldout(m_property, m_foldout);
        }

        private void FillFoldout(SerializedProperty property, Foldout foldout)
        {
            SerializedObject serializedTarget = new SerializedObject(m_property.objectReferenceValue);
            serializedTarget.Update();
            SerializedProperty targetProperty = serializedTarget.GetIterator();

            _ = targetProperty.NextVisible(true); // Base
            _ = targetProperty.NextVisible(true); //Script

            do
            {
                foldout.Add(new PropertyField(targetProperty));
            }
            while (targetProperty.NextVisible(true));

            m_foldout.MarkDirtyRepaint();
        }
        #endregion
    }
}
