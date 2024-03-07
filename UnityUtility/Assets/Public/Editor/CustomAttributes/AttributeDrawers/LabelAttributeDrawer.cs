using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityUtility.Utils.Editor;

namespace UnityUtility.CustomAttributes.Editor
{
    [CustomPropertyDrawer(typeof(LabelAttribute))]
    public class LabelAttributeDrawer : PropertyDrawer
    {
        private PropertyField m_drawnProperty;
        private bool m_partOfArray;

        #region UIElements
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            m_partOfArray = property.IsPropertyPartOfArray(out SerializedProperty arrayProperty, out int index);
            m_drawnProperty = new PropertyField(property);
            m_drawnProperty.name = "DrawnProperty";
            if (!m_partOfArray || true)
            {
                m_drawnProperty.RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            }
            return m_drawnProperty;
        }

        private void OnAttachToPanel(AttachToPanelEvent evt)
        {
            //m_drawnProperty.UnregisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            if (attribute is LabelAttribute labelAttribute)
            {
                Label propertyLabel = m_drawnProperty.Q<Label>();
                Foldout foldout = m_drawnProperty.Q<Foldout>();
                Debug.LogError(m_drawnProperty.name);
                foreach (var element in m_drawnProperty.Children())
                {
                    Debug.Log(element.name);
                }
                if (foldout != null)
                {
                    Debug.LogWarning(foldout.bindingPath);
                }
                if (propertyLabel == null)
                {
                    Debug.LogError($"No Label found for property {m_drawnProperty.bindingPath}");
                    return;
                }
                if (!string.IsNullOrEmpty(labelAttribute.OverrideName))
                {
                    //propertyLabel.text = labelAttribute.OverrideName;
                }

                FontStyle labelStyle = FontStyle.Normal;
                if (labelAttribute.Bold && labelAttribute.Italic)
                {
                    labelStyle = FontStyle.BoldAndItalic;
                }
                else
                {
                    if (labelAttribute.Bold)
                    {
                        labelStyle = FontStyle.Bold;
                    }
                    else if (labelAttribute.Italic)
                    {
                        labelStyle = FontStyle.Italic;
                    }
                }
                propertyLabel.style.unityFontStyleAndWeight = labelStyle;

            }
        }
        #endregion
    }
}
