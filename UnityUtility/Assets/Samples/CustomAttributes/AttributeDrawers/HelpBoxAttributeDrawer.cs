using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace UnityUtility.CustomAttributes.Drawers
{
    [CustomPropertyDrawer(typeof(HelpBoxAttribute))]
    public class HelpBoxAttributeDrawer : DecoratorDrawer
    {
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

        public override bool CanCacheInspectorGUI()
        {
            return true;
        }

        public override VisualElement CreatePropertyGUI()
        {
            VisualElement container = new VisualElement();

            if (attribute is not HelpBoxAttribute helpBoxAttribute)
            {
                return container;
            }

            container.Add(new HelpBox(helpBoxAttribute.Message, helpBoxAttribute.MessageType));
            return container;
        }

        private MessageType GetMessageType(HelpBoxMessageType helpBoxType)
        {
            switch (helpBoxType)
            {
                case HelpBoxMessageType.None:
                    return MessageType.None;

                default:
                case HelpBoxMessageType.Info:
                    return MessageType.Info;

                case HelpBoxMessageType.Warning:
                    return MessageType.Warning;

                case HelpBoxMessageType.Error:
                    return MessageType.Error;
            }
        }
    }
}
