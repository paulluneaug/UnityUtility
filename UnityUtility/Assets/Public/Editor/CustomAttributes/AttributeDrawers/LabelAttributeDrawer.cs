using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace UnityUtility.CustomAttributes.Editor
{
    [CustomPropertyDrawer(typeof(LabelAttribute))]
    public class LabelAttributeDrawer : PropertyDrawer
    {
        private VisualElement m_drawnProperty;

        #region VisualElement
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            m_drawnProperty = new PropertyField(property);
            m_drawnProperty.RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            return m_drawnProperty;
        }

        private void OnAttachToPanel(AttachToPanelEvent evt)
        {
            if (attribute is LabelAttribute labelAttribute)
            {
                Label propertyLabel = m_drawnProperty.Q<Label>();
                if (!string.IsNullOrEmpty(labelAttribute.OverrideName))
                {
                    propertyLabel.text = labelAttribute.OverrideName;
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
