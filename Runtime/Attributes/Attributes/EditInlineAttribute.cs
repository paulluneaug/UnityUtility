using System;

using UnityEngine;

namespace UnityUtility.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class EditInlineAttribute : PropertyAttribute
    {
        public EditInlineAttribute()
        {
        }
    }
}
