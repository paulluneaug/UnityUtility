using UnityEngine;

namespace UnityUtility.CustomAttributes
{
    public class LabelAttribute : PropertyAttribute
    {
        public string OverrideName { get; }
        public bool Bold { get; }
        public bool Italic { get; }
        public int FontSize { get; }

        public LabelAttribute(string overrideName = null, bool bold = false, bool italic = false, int fontSize = 12)
        {
            Bold = bold;
            Italic = italic;
            FontSize = fontSize;
            OverrideName = overrideName;
        }
    }
}
