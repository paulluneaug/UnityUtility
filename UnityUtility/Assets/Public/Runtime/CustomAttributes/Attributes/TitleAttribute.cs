using System.Collections;
using System.Collections.Generic;
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
        public bool HorizontalLine { get; }
        public TitleAlignments TitleAlignment { get; }

        public TitleAttribute(
            string title,
            string subtitle,
            bool bold = true,
            bool horizontalLine = true,
            TitleAlignments titleAlignment = TitleAlignments.Left)
        {
            Title = title;
            Subtitle = subtitle;
            Bold = bold;
            HorizontalLine = horizontalLine;
            TitleAlignment = titleAlignment;
        }

        public TitleAttribute(
            string title, 
            bool bold = true, 
            bool horizontalLine = true, 
            TitleAlignments titleAlignment = TitleAlignments.Left) :

            this(title, string.Empty, bold, horizontalLine, titleAlignment)
        {
        }
    }
}
