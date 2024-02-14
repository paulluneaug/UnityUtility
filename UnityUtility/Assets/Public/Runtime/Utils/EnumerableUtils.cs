using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityUtility.Utils
{
    public static class EnumerableUtils
    {
        /// <summary>
        /// Execute the specified <paramref name="action"/> on each element of <paramref name="enumerable"/>
        /// </summary>
        /// <typeparam name="T">Type of the elements of the <see cref="IEnumerable"/></typeparam>
        /// <param name="action">The <see cref="Action"/> to execute on each element of <paramref name="enumerable"/> </param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }

        /// <summary>
        /// Formats the <paramref name="enumerable"/> into a nicer looking <see cref="string"/>
        /// </summary>
        /// <typeparam name="T">Type of the elements of the <see cref="IEnumerable"/></typeparam>
        /// <returns>A <see cref="string"/> representing all the elements of <paramref name="enumerable"/> separated by a comma</returns>
        public static string EnumerableToString<T>(this IEnumerable<T> enumerable)
        {
            return $"[{string.Join(", ", enumerable)}]";
        }
    }
}
