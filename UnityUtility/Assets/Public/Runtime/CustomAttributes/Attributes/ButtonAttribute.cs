using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtility.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ButtonAttribute : PropertyAttribute
    {
        public string MethodName { get; }
        public string DisplayName => string.IsNullOrEmpty(m_displayName) ? MethodName : m_displayName;

        private string m_displayName = string.Empty;

        public ButtonAttribute(string methodName, string displayName = "")
        {
            MethodName = methodName;
            m_displayName = displayName;
        }
    }
}
