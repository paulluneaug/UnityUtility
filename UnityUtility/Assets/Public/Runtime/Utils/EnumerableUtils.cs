using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityUtility.Hash;

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
        /// <returns>A <see cref="string"/> representing all the elements of <paramref name="enumerable"/> separated by a comma and surrounded by square brackets</returns>
        public static string EnumerableToString<T>(this IEnumerable<T> enumerable)
        {
            return $"[{string.Join(", ", enumerable)}]";
        }

        /// <summary>
        /// Shallow copies the content on the <paramref name="collection"/> into an array
        /// </summary>
        /// <typeparam name="T">Type of the elements of the <see cref="ICollection"/></typeparam>
        /// <param name="collection">The <see cref="ICollection"/> to copy</param>
        /// <returns>An array containing all the elements of the given <paramref name="collection"/></returns>
        public static T[] Copy<T>(this ICollection<T> collection)
        {
            T[] result = new T[collection.Count];
            collection.CopyTo(result, 0);
            return result;
        }
    }

    public static class SortUtils
    {
        /// <summary>
        /// Swaps the elements at index <paramref name="indexA"/> and <paramref name="indexB"/>
        /// </summary>
        /// <typeparam name="T">The type of the elments of the <paramref name="list"/></typeparam>
        /// <param name="list">The <see cref="IList"/> in which to swap the elements</param>
        /// <param name="indexA">The index of the first element to swap</param>
        /// <param name="indexB">The index of the second element to swap</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            if (indexA == indexB)
            {
                return;
            }

            int listLength = list.Count;
            if (indexA < 0 || listLength <= indexA)
            {
                throw new ArgumentOutOfRangeException(nameof(indexA), $"{nameof(indexA)} must belong in the interval [0; list.Count - 1] ([{0}; {listLength - 1}]), currently {indexA}");
            }
            if (indexB < 0 || listLength <= indexB)
            {
                throw new ArgumentOutOfRangeException(nameof(indexB), $"{nameof(indexB)} must belong in the interval [0; list.Count - 1] ([{0}; {listLength - 1}]), currently {indexB}");
            }
            (list[indexB], list[indexA]) = (list[indexA], list[indexB]);
        }

        public static T[] ShuffleCopy<T>(this IList<T> list)
        {
            T[] copy = list.Copy();
            copy.Shuffle();
            return copy;
        }
        public static T[] ShuffleCopy<T>(this IList<T> list, int seed)
        {
            T[] copy = list.Copy();
            copy.Shuffle(seed);
            return copy;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            list.ShuffleImpl(new Hasher());
        }
        public static void Shuffle<T>(this IList<T> list, int seed)
        {
            list.ShuffleImpl(new Hasher(seed));
        }

        private static void ShuffleImpl<T>(this IList<T> list, Hasher hasher)
        {
            list.Sort((T a, T b) => Math.Sign(hasher.RandomInt()));
        }

        public static T[] SortCopy<T>(this IList<T> list, Comparison<T> comparison)
        {
            T[] result = list.Copy();
            result.Sort(comparison);
            return result;
        }

        public static void Sort<TComparable>(this IList<TComparable> list)
            where TComparable : IComparable<TComparable>
        {
            list.Sort(ComparableComparison);
        }

        public static void Sort<TComparable>(this IList<TComparable> list, int start, int end)
            where TComparable : IComparable<TComparable>
        {
            list.Sort(start, end, ComparableComparison);
        }

        private static  int ComparableComparison<TComparable>(TComparable a, TComparable b)
            where TComparable : IComparable<TComparable>
        {
            return a.CompareTo(b);
        }

        public static void Sort<T>(this IList<T> list, Comparison<T> comparison)
        {
            list.Sort(0, list.Count - 1, comparison);
        }

        public static void Sort<T>(this IList<T> list, int start, int end, Comparison<T> comparison)
        {
            // Quicksort
            if (start < end)
            {
                int pivot = GetPivot(list, start, end);
                pivot = Partition(list, start, end, pivot, comparison);
                list.Sort(start, pivot - 1, comparison);
                list.Sort(pivot + 1, end, comparison);
            }
        }

        #region Quicksort methods
        private static int Partition<T>(IList<T> list, int start, int end, int pivot, Comparison<T> comparison)
        {
            list.Swap(pivot, end);
            int j = start;
            for (int i = start; i < end; ++i)
            {
                if (comparison(list[i], list[end]) <= 0)
                {
                    list.Swap(i, j);
                    ++j;
                }
            }
            list.Swap(end, j);
            return j;
        }

        private static int GetPivot<T>(this IList<T> list, int start, int end)
        {
            return end;
        }
        #endregion
    }
}
