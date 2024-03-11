using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;

namespace UnityUtility.CustomAttributes.Editor
{
    [CustomPropertyDrawer(typeof(TitleAttribute))]
    public class TitleAttributeDrawer : DecoratorDrawer
    {
        private readonly float m_spaceAtTheBeginning = 15;
        private readonly float m_titleHeight = EditorGUIUtility.singleLineHeight;
        private readonly float m_subtitleHeight = EditorGUIUtility.singleLineHeight;
        private readonly float m_spaceAtTheEnd = 5;

        #region IMGUI
        public override float GetHeight()
        {
            TitleAttribute titleAttribute = attribute as TitleAttribute;

            float totalHeight = m_titleHeight;

            if (!string.IsNullOrEmpty(titleAttribute.Subtitle)) { totalHeight += m_subtitleHeight; }

            if (titleAttribute.Separator) { totalHeight += m_spaceAtTheEnd; }

            return totalHeight + m_spaceAtTheBeginning;
        }
        public override void OnGUI(Rect position)
        {
            if (attribute is TitleAttribute titleAttribute)
            {
                float offset = m_spaceAtTheBeginning;

                Rect titleRect = new Rect(position.x, position.y + offset, position.width, m_titleHeight);
                offset += m_titleHeight;

                GUIStyle titleStyle = new GUIStyle(titleAttribute.Bold ? EditorStyles.boldLabel : EditorStyles.label)
                {
                    alignment = GetTextAnchor(titleAttribute.TitleAlignment)
                };

                EditorGUI.LabelField(titleRect, titleAttribute.Title, titleStyle);

                if (!string.IsNullOrEmpty(titleAttribute.Subtitle))
                {
                    Rect subtitleRect = new Rect(position.x, position.y + offset, position.width, m_subtitleHeight);

                    GUIStyle subtitleStyle = new GUIStyle(EditorStyles.miniLabel)
                    {
                        alignment = GetTextAnchor(titleAttribute.TitleAlignment)
                    };

                    EditorGUI.LabelField(subtitleRect, titleAttribute.Subtitle, subtitleStyle);
                }

                if (titleAttribute.Separator)
                {
                    Rect horizontalLine = new Rect(position.x, position.y + GetHeight() - m_spaceAtTheEnd, position.width, 1);
                    EditorGUI.DrawRect(horizontalLine, AttributeUtils.SeparatorColor);
                }
            }
        }

        private TextAnchor GetTextAnchor(TitleAlignments alignment)
        {
            return alignment switch
            {
                TitleAlignments.Right => TextAnchor.MiddleRight,
                TitleAlignments.Centered => TextAnchor.MiddleCenter,
                TitleAlignments.Left => TextAnchor.MiddleLeft,
                _ => TextAnchor.MiddleLeft,
            };
        }
        #endregion

        #region VisualElement
        public override VisualElement CreatePropertyGUI()
        {
            VisualElement container = new VisualElement();

            container.style.marginTop = m_spaceAtTheBeginning;
            container.style.marginBottom = m_spaceAtTheEnd;

            if (attribute is TitleAttribute titleAttribute)
            {
                Label titleLabel = new Label(titleAttribute.Title);
                titleLabel.style.height = m_titleHeight;
                titleLabel.style.fontSize = titleAttribute.FontSize;
                titleLabel.style.unityFontStyleAndWeight = AttributeUtils.GetFontStyle(titleAttribute.Bold, titleAttribute.Italic);
                titleLabel.style.unityTextAlign = GetTextAnchor(titleAttribute.TitleAlignment);
                container.Add(titleLabel);


                if (!string.IsNullOrEmpty(titleAttribute.Subtitle))
                {
                    Label subtitleLabel = new Label(titleAttribute.Subtitle);
                    subtitleLabel.style.fontSize = 10;
                    subtitleLabel.style.height = m_subtitleHeight;
                    subtitleLabel.style.unityTextAlign = GetTextAnchor(titleAttribute.TitleAlignment);
                    container.Add(subtitleLabel);
                }

                if (titleAttribute.Separator)
                {
                    container.Add(AttributeUtils.CreateSeparator());
                }
            }

            return container;
        }
        #endregion
    }
}
