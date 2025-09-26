using System;

using UnityEngine;

namespace UnityUtility.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public class SeparatorAttribute : PropertyAttribute
    {
        public SeparatorAttribute()
        {
        }
    }
}
