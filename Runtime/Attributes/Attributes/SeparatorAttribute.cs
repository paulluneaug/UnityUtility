using System;

using UnityEngine;

namespace UnityUtility.Inspector
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public class SeparatorAttribute : PropertyAttribute
    {
        public SeparatorAttribute()
        {
        }
    }
}
