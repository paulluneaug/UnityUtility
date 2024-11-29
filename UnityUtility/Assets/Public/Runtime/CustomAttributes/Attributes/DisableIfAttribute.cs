using UnityEngine;

namespace UnityUtility.CustomAttributes
{
    public class DisableIfAttribute : PropertyAttribute
    {
        public string FieldName { get; }
        public object CompareValue { get; }
        public bool Inverse { get; }

        public DisableIfAttribute(string fieldName, object compareValue = null, bool inverse = false)
        {
            FieldName = fieldName;
            CompareValue = compareValue;
            Inverse = inverse;
        }
    }
}
