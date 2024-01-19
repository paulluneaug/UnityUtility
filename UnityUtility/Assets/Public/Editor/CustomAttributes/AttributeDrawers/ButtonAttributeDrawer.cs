using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityUtility.CustomAttributes.Editor
{
    [CustomPropertyDrawer(typeof(ButtonAttribute))]
    public class ButtonAttributeDrawer : PropertyDrawer
    {
        private bool m_isInit = false;

        private MethodInfo m_targetMethod = null;
        private object m_targetObject = null;
        private ButtonAttribute m_target = null;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new VisualElement();

            InitIfNeeded(property);
            
            Button b = new Button(() => InvokeMethod());
            b.text = m_target.DisplayName;
            container.Add(b);
            container.Add(base.CreatePropertyGUI(property));

            return container;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            InitIfNeeded(property);

            Rect buttonRect = position;
            buttonRect.height = EditorGUIUtility.singleLineHeight;
            if (GUI.Button(buttonRect, m_target.DisplayName))
            {
                InvokeMethod();
            }
            base.OnGUI(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight + base.GetPropertyHeight(property, label);
        }

        private void InitIfNeeded(SerializedProperty property)
        {
            if (m_isInit)
            {
                return;
            }
            m_target = attribute as ButtonAttribute;
            GetMethodInfos(property, m_target.MethodName); 
            m_isInit = true;
        }

        private void GetMethodInfos(SerializedProperty property, string methodName)
        {
            Type parentType = property.serializedObject.targetObject.GetType();

            m_targetMethod = parentType.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            m_targetObject = property.serializedObject.targetObject;
        }

        private void InvokeMethod()
        {
            m_targetMethod.Invoke(m_targetObject, null);
        }
    }
}