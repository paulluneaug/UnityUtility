using UnityEngine;

namespace UnityUtility.CustomAttributes
{
    public class LabelAttribute : PropertyAttribute
    {
        public string OverrideName { get; }
        public bool Bold { get; }
        public bool Italic { get; }

        public LabelAttribute(string overrideName = null, bool bold = false, bool italic = false)
        {
            Bold = bold;
            Italic = italic;
            OverrideName = overrideName;
        }
    }
}
