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

        private readonly Color m_lineColor = new Color(0.3515625f, 0.3515625f, 0.3515625f);

        #region IMGUI
        public override float GetHeight()
        {
            TitleAttribute titleAttribute = attribute as TitleAttribute;

            float totalHeight = m_titleHeight;

            if (titleAttribute.Subtitle != String.Empty) { totalHeight += m_subtitleHeight; }

            if (titleAttribute.HorizontalLine) { totalHeight += m_spaceAtTheEnd; }

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

                if (titleAttribute.Subtitle != string.Empty)
                {
                    Rect subtitleRect = new Rect(position.x, position.y + offset, position.width, m_subtitleHeight);

                    GUIStyle subtitleStyle = new GUIStyle(EditorStyles.miniLabel)
                    {
                        alignment = GetTextAnchor(titleAttribute.TitleAlignment)
                    };

                    EditorGUI.LabelField(subtitleRect, titleAttribute.Subtitle, subtitleStyle);
                }

                if (titleAttribute.HorizontalLine)
                {
                    Rect horizontalLine = new Rect(position.x, position.y + GetHeight() - m_spaceAtTheEnd, position.width, 1);
                    EditorGUI.DrawRect(horizontalLine, m_lineColor);
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
                titleLabel.style.unityFontStyleAndWeight = titleAttribute.Bold ? FontStyle.Bold : FontStyle.Normal;
                titleLabel.style.unityTextAlign = GetTextAnchor(titleAttribute.TitleAlignment);
                container.Add(titleLabel);


                if (titleAttribute.Subtitle != string.Empty)
                {
                    Label subtitleLabel = new Label(titleAttribute.Subtitle);
                    subtitleLabel.style.fontSize = 10;
                    subtitleLabel.style.height = m_subtitleHeight;
                    subtitleLabel.style.unityTextAlign = GetTextAnchor(titleAttribute.TitleAlignment);
                    container.Add(subtitleLabel);
                }

                if (titleAttribute.HorizontalLine)
                {
                    VisualElement horizontalLine = new VisualElement();
                    horizontalLine.style.backgroundColor = m_lineColor;
                    horizontalLine.style.height = 1;
                    container.Add(horizontalLine);
                }
            }

            return container;
        }

        public override bool CanCacheInspectorGUI()
        {
            return base.CanCacheInspectorGUI();
        }
        #endregion
    }
}
