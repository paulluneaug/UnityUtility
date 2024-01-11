using UnityEngine;
using System;
using UnityEngine.UIElements;

namespace UnityUtility.CustomAttributes
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
	public class HelpBoxAttribute : PropertyAttribute
    {
        public string Message { get; }

        public HelpBoxMessageType MessageType { get; }

        public HelpBoxAttribute(string text, HelpBoxMessageType messageType = HelpBoxMessageType.Info)
        {
            Message = text;
            MessageType = messageType;
        }
    }
}
 




