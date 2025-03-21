using UnityEditor;

using UnityEngine;
using UnityEngine.UIElements;

namespace UnityUtility.CustomAttributes.Editor
{
    [CustomPropertyDrawer(typeof(HelpBoxAttribute))]
    public class HelpBoxAttributeDrawer : DecoratorDrawer
    {
        private const int PADDING = 5;

        #region IMGUI
        public override float GetHeight()
        {
            if (attribute is not HelpBoxAttribute helpBoxAttribute)
            {
                return base.GetHeight();
            }

            GUIStyle helpBoxStyle = (GUI.skin != null) ? GUI.skin.GetStyle("helpbox") : null;
            if (helpBoxStyle == null)
            {
                return base.GetHeight();
            }

            float minHeight = EditorGUIUtility.singleLineHeight * (helpBoxAttribute.MessageType == HelpBoxMessageType.None ? 1 : 2);

            return Mathf.Max(minHeight, helpBoxStyle.CalcHeight(new GUIContent(helpBoxAttribute.Message), EditorGUIUtility.currentViewWidth) + 4);
        }

        public override void OnGUI(Rect position)
        {
            if (attribute is not HelpBoxAttribute helpBoxAttribute)
            {
                return;
            }

            EditorGUILayout.HelpBox(helpBoxAttribute.Message, GetMessageType(helpBoxAttribute.MessageType));
        }
        #endregion

        #region UIElements
        public override VisualElement CreatePropertyGUI()
        {
            VisualElement container = new VisualElement();

            if (attribute is not HelpBoxAttribute helpBoxAttribute)
            {
                return container;
            }

            container.style.paddingBottom = PADDING;
            container.style.paddingTop = PADDING;

            container.Add(new HelpBox(helpBoxAttribute.Message, helpBoxAttribute.MessageType));
            return container;
        }
        #endregion

        private MessageType GetMessageType(HelpBoxMessageType helpBoxType)
        {
            return helpBoxType switch
            {
                HelpBoxMessageType.None => MessageType.None,
                HelpBoxMessageType.Info => MessageType.Info,
                HelpBoxMessageType.Warning => MessageType.Warning,
                HelpBoxMessageType.Error => MessageType.Error,
                _ => MessageType.Info,
            };
        }
    }
}
