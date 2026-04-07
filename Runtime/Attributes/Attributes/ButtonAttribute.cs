using System;

using UnityEngine;

namespace UnityUtility.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ButtonAttribute : PropertyAttribute
    {
        public string MethodName { get; }
        public string DisplayName => string.IsNullOrEmpty(m_displayName) ? MethodName : m_displayName;

        private readonly string m_displayName = string.Empty;

        public ButtonAttribute(string methodName, string displayName = "")
        {
            MethodName = methodName;
            m_displayName = displayName;
        }
    }
}
