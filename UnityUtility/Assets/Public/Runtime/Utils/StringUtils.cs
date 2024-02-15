using System;
using System.Linq;

namespace UnityUtility.Utils
{
    public static class StringUtils
    {
        /// <summary>
        /// Replaces the last occurence of <paramref name="oldValue"/> with <paramref name="newValue"/> in <paramref name="str"/>
        /// </summary>
        /// <param name="oldValue">The value that will be replaced by <paramref name="newValue"/></param>
        /// <param name="newValue">The value used to replace <paramref name="oldValue"/></param>
        /// <returns><paramref name="str"/> with the last occurence of <paramref name="oldValue"/> replaced by <paramref name="newValue"/></returns>
        public static string ReplaceLast(this string str, string oldValue, string newValue)
        {
            int oldValIndex = str.LastIndexOf(oldValue);

            if (oldValIndex == -1)
            {
                return str;
            }

            return str.Remove(oldValIndex, oldValue.Length).Insert(oldValIndex, newValue);
        }

        /// <summary>
        /// Sets the first character of a given <see cref="string"/> to upper
        /// </summary>
        /// <returns>The given <see cref="string"/> with the first character to upper</returns>
        public static string FirstCharToUpper(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            char[] strArray = str.ToCharArray();
            strArray[0] = char.ToUpper(strArray[0]);
            return new string(strArray);
        }    
        
        /// <summary>
        /// Splits a given string (<paramref name="st"/>) into an array of string of a given size (<paramref name="wantedSize"/>)
        /// 
        /// <para> Note : The last element of the array may not have the desired size if 
        /// <c><paramref name="st"/>.Length % <paramref name="wantedSize"/> != 0</c></para>
        /// </summary>
        /// 
        /// <param name="st">String to split</param>
        /// <param name="wantedSize">The size of the strings returned</param>
        /// 
        /// <returns>An array of <see cref="string"/> of size <paramref name="wantedSize"/></returns>
        public static string[] SplitBySize(this string st, int wantedSize)
        {
            int whereIndex = 0;
            int selectIndex = 0;
            int stringLen = st.Length;
            return st.Where((char c) => whereIndex++ % wantedSize == 0)
                     .Select((char c) => st.Substring(
                         selectIndex * wantedSize,
                         Math.Clamp(stringLen - (selectIndex++ * wantedSize), 0, wantedSize)))
                     .ToArray();
        }
    }
}
