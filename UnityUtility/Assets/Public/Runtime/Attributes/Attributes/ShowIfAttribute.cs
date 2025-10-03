using UnityEngine;

namespace UnityUtility.Attributes
{
    public class ShowIfAttribute : PropertyAttribute
    {
        public string FieldName { get; }
        public object CompareValue { get; }
        public bool Inverse { get; }

        public ShowIfAttribute(string fieldName, object compareValue = null, bool inverse = false)
        {
            FieldName = fieldName;
            CompareValue = compareValue;
            Inverse = inverse;
        }
    }
}
