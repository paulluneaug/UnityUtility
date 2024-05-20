using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityUtility.Utils
{
    public static class EnumerableUtils
    {
        /// <summary>
        /// Execute the specified <paramref name="action"/> on each element of <paramref name="enumerable"/>
        /// </summary>
        /// <typeparam name="T">Type of the elements in the <see cref="IEnumerable"/></typeparam>
        /// <param name="action">The <see cref="Action"/> to execute on each element of <paramref name="enumerable"/></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        public static IEnumerable<T> Copy<T>(this IEnumerable<T> enumerable)
        {
            foreach (T item in enumerable)
            {
                yield return item;
            }
        }
    }

    public static class SortUtils
    {
        public static void Swap<T>(this T[] array, int indexA, int indexB)
        {
            if (indexA == indexB)
            {
                return;
            }

            int arrayLength = array.Length;
            if (indexA < 0 || arrayLength <= indexA)
            {
                throw new ArgumentOutOfRangeException(nameof(indexA), $"{nameof(indexA)} must belong in the interval [0; array.Length - 1] ([{0}; {arrayLength - 1}]), currently {indexA}");
            }
            if (indexB < 0 || arrayLength <= indexB)
            {
                throw new ArgumentOutOfRangeException(nameof(indexB), $"{nameof(indexB)} must belong in the interval [0; array.Length - 1] ([{0}; {arrayLength - 1}]), currently {indexB}");
            }
            (array[indexB], array[indexA]) = (array[indexA], array[indexB]);
        }

        public static IEnumerable<T> ShuffleCopy<T>(this IEnumerable<T> enumerable)
        {
            IEnumerable<T> copy = enumerable.Copy();
            copy.Shuffle();
            return copy;
        }
        public static void Shuffle<T>(this IEnumerable<T> enumerable)
        {
        }

        public static IEnumerable<T> SortCopy<T>(this IEnumerable<T> enumerable, Comparison<T> comparison)
        {
            T[] copy = enumerable.ToArray();
            copy.Sort(comparison);
            return copy;
        }

        public static void Sort<TComparable>(this TComparable[] array)
            where TComparable : IComparable<TComparable>
        {
            array.Sort((TComparable a, TComparable b) => a.CompareTo(b));
        }

        public static void Sort<TComparable>(this TComparable[] array, int start, int end)
            where TComparable : IComparable<TComparable>
        {
            array.Sort(start, end, (TComparable a, TComparable b) => a.CompareTo(b));
        }

        public static void Sort<T>(this T[] array, Comparison<T> comparison)
        {
            array.Sort(0, array.Length - 1, comparison);
        }

        public static void Sort<T>(this T[] array, int start, int end, Comparison<T> comparison)
        {
            // Quicksort
            if (start < end)
            {
                int pivot = GetPivot(array, start, end);
                pivot = Partition(array, start, end, pivot, comparison);
                array.Sort(start, pivot - 1, comparison);
                array.Sort(pivot + 1, end, comparison);
            }
        }

        #region Quicksort methods
        private static int Partition<T>(T[] array, int start, int end, int pivot, Comparison<T> comparison)
        {
            array.Swap(pivot, end);
            int j = start;
            for (int i = start; i < end; ++i)
            {
                if (comparison(array[i], array[end]) <= 0)
                {
                    array.Swap(i, j);
                    ++j;
                }
            }
            array.Swap(end, j);
            return j;
        }

        private static int GetPivot<T>(this T[] array, int start, int end)
        {
            return end;
        }
        #endregion
    }
}
