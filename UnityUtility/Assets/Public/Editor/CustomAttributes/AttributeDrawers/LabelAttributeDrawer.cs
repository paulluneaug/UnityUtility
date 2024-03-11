using UnityEditor;
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
            m_drawnProperty = new PropertyField(property)
            {
                name = "DrawnProperty"
            };

            m_drawnProperty.RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            return m_drawnProperty;
        }

        private void OnAttachToPanel(AttachToPanelEvent evt)
        {
            m_drawnProperty.UnregisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            if (attribute is LabelAttribute labelAttribute)
            {
                Label propertyLabel = null;
                if (m_partOfArray)
                {
                    PropertyField drawnArray = FindInParent<PropertyField>(m_drawnProperty.parent.parent);
                    if (drawnArray == null)
                    {
                        return;
                    }
                    propertyLabel = drawnArray.Q<Label>();
                }
                else
                {
                    propertyLabel = m_drawnProperty.Q<Label>();
                }

                if (propertyLabel == null)
                {
                    return;
                }

                // Label name
                if (!string.IsNullOrEmpty(labelAttribute.OverrideName))
                {
                    propertyLabel.text = labelAttribute.OverrideName;
                }

                // Label font style
                propertyLabel.style.unityFontStyleAndWeight = AttributeUtils.GetFontStyle(labelAttribute.Bold, labelAttribute.Italic);

                // Label font size
                propertyLabel.style.fontSize = labelAttribute.FontSize;
            }
        }


        private T FindInParent<T>(VisualElement v, string name = null) where T : VisualElement
        {
            if (v == null)
            {
                return null;
            }

            if (v is T matchingParent)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    if (matchingParent.name == name)
                    {
                        return matchingParent;
                    }
                }
                else
                {
                    return matchingParent;
                }
            }

            return FindInParent<T>(v.parent, name);
        }
        #endregion
    }
}
