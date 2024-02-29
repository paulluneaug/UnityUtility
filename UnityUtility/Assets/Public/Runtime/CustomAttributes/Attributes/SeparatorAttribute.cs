using UnityEngine;
using System;

namespace UnityUtility.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public class SeparatorAttribute : PropertyAttribute
    {
        public SeparatorAttribute()
        {
        }
    }
}
