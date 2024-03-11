using UnityEngine;
using System;

namespace UnityUtility.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public class TitleAttribute : PropertyAttribute
    {
        public string Title { get; }
        public string Subtitle { get; }
        public bool Bold { get; }
        public bool Italic { get; }
        public int FontSize { get; }
        public bool Separator { get; }
        public TitleAlignments TitleAlignment { get; }

        public TitleAttribute(
            string title,
            string subtitle,
            bool bold = true,
            bool italic = false,
            int fontSize = 12,
            bool separator = true,
            TitleAlignments titleAlignment = TitleAlignments.Left)
        {
            Title = title;
            Subtitle = subtitle;
            Bold = bold;
            Italic = italic;
            FontSize = fontSize;
            Separator = separator;
            TitleAlignment = titleAlignment;
        }

        public TitleAttribute(
            string title,
            bool italic = false,
            bool bold = true,
            int fontSize = 12,
            bool separator = true, 
            TitleAlignments titleAlignment = TitleAlignments.Left) :

            this(title, string.Empty, bold, italic, fontSize, separator, titleAlignment)
        {
        }
    }
}
