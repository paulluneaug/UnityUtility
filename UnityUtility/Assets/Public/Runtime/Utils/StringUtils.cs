namespace UnityUtility.Utils
{
    public static class StringUtils
    {
        /// <summary>
        /// Replaces the last occurence of <paramref name="oldValue"/> with <paramref name="newValue"/> in <paramref name="str"/>
        /// </summary>
        /// <param name="str"></param>
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
        /// <param name="str"></param>
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
    }
}
