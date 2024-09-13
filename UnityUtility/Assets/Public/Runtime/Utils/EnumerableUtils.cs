using System;
using System.Collections;
using System.Collections.Generic;
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
            foreach (T element in enumerable)
            {
                action(element);
            }
        }

        /// <summary>
        /// Formats the <paramref name="enumerable"/> into a nicer looking <see cref="string"/>
        /// </summary>
        /// <typeparam name="T">Type of the elements of the <see cref="IEnumerable"/></typeparam>
        /// <returns>A <see cref="string"/> representing all the elements of <paramref name="enumerable"/> separated by a comma and surrounded by square brackets</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        public static int IndexOf<T>(this IEnumerable<T> enumerable, T searchedItem, Func<T, T, bool> equalityComparer)
        {
            int index = 0;
            foreach (T element in enumerable)
            {
                if (equalityComparer(element, searchedItem))
                {
                    return index;
                }
                ++index;
            }
            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOf<T>(this IEnumerable<T> enumerable, T searchedItem)
        {
            return IndexOf(enumerable, searchedItem, (item0, item1) => item0.Equals(item1));
        }
    }

    public static class SortUtils
    {
        #region Shuffle

        /// <summary>
        /// Shuffles the given <paramref name="list"/>
        /// </summary>
        /// <typeparam name="T">Type of the elements of the <see cref="IList"/></typeparam>
        /// <param name="list">The <see cref="IList"/> to shuffle</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Shuffle<T>(this IList<T> list)
        {
            list.ShuffleImpl(new Hasher());
        }

        /// <inheritdoc cref="Shuffle{T}(IList{T})"/>
        /// <param name="seed">The pseudo random seed used to shuffle the list</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Shuffle<T>(this IList<T> list, int seed)
        {
            list.ShuffleImpl(new Hasher(seed));
        }

        /// <inheritdoc cref="Shuffle{T}(IList{T})"/>
        /// <param name="hasher">The <see cref="Hasher"/> used to create pseudo random values to shuffle the list</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Shuffle<T>(this IList<T> list, Hasher hasher)
        {
            list.ShuffleImpl(hasher);
        }

        /// <summary>
        /// Copies the given <paramref name="list"/> into a new array and shuffles it
        /// </summary>
        /// <typeparam name="T">Type of the elements of the <see cref="IList"/></typeparam>
        /// <param name="list">The <see cref="IList"/> to copy and shuffle</param>
        /// <returns>A shuffled copy of the the given <paramref name="list"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ShuffleCopy<T>(this IList<T> list)
        {
            T[] copy = list.Copy();
            copy.Shuffle();
            return copy;
        }

        /// <inheritdoc cref="ShuffleCopy{T}(IList{T})"/>
        /// <param name="seed">The pseudo random seed used to shuffle the list</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ShuffleCopy<T>(this IList<T> list, int seed)
        {
            T[] copy = list.Copy();
            copy.Shuffle(seed);
            return copy;
        }

        /// <inheritdoc cref="ShuffleCopy{T}(IList{T})"/>
        /// <param name="hasher">The <see cref="Hasher"/> used to create pseudo random values to shuffle the list</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ShuffleCopy<T>(this IList<T> list, Hasher hasher)
        {
            T[] copy = list.Copy();
            copy.Shuffle(hasher);
            return copy;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ShuffleImpl<T>(this IList<T> list, Hasher hasher)
        {
            list.Sort((T a, T b) => Math.Sign(hasher.RandomInt()));
        }
        #endregion

        #region Sort
        /// <summary>
        /// Sorts the given <paramref name="list"/> using the quicksort algorithm
        /// </summary>
        /// <typeparam name="T">Type of the elements of the <see cref="IList"/></typeparam>
        /// <param name="list">The <see cref="IList"/> to sort</param>
        /// <param name="start">The index of the lower bound of the interval to sort</param>
        /// <param name="end">The index of the higher bound of the interval to sort</param>
        /// <param name="comparison"></param>
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

        /// <inheritdoc cref="Sort{T}(IList{T}, int, int, Comparison{T})"/>
        public static void Sort<TComp>(this IList<TComp> list, int start, int end)
            where TComp : IComparable<TComp>
        {
            list.Sort(start, end, ComparableComparison);
        }

        /// <inheritdoc cref="Sort{T}(IList{T}, int, int, Comparison{T})"/>
        public static void Sort<T>(this IList<T> list, Comparison<T> comparison)
        {
            list.Sort(0, list.Count - 1, comparison);
        }

        /// <inheritdoc cref="Sort{T}(IList{T}, int, int, Comparison{T})"/>
        public static void Sort<TComp>(this IList<TComp> list)
            where TComp : IComparable<TComp>
        {
            list.Sort(ComparableComparison);
        }

        /// <summary>
        /// Copies the given <paramref name="list"/> into a new array and sorts it
        /// </summary>
        /// <typeparam name="T">Type of the elements of the <see cref="IList"/></typeparam>
        /// <param name="list"></param>
        /// <param name="comparison"></param>
        /// <returns>A sorted copy of the the given <paramref name="list"/></returns>
        public static T[] SortCopy<T>(this IList<T> list, Comparison<T> comparison)
        {
            T[] result = list.Copy();
            result.Sort(comparison);
            return result;
        }

        /// <inheritdoc cref="SortCopy{T}(IList{T}, Comparison{T})"/>
        public static TComp[] SortCopy<TComp>(this IList<TComp> list)
            where TComp : IComparable<TComp>
        {
            return list.SortCopy(ComparableComparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ComparableComparison<TComp>(TComp a, TComp b)
            where TComp : IComparable<TComp>
        {
            return a.CompareTo(b);
        }
        #endregion

        #region Sort Checkers
        /// <summary>
        /// Checks wether the given <paramref name="list"/> is sorted
        /// </summary>
        /// <typeparam name="TComp"></typeparam>
        /// <param name="list"></param>
        /// <returns>Wether the given <see cref="IList"/> is sorted</returns>
        public static bool IsSorted<TComp>(this IList<TComp> list)
                where TComp : IComparable<TComp>
        {
            return IsSorted(list, ComparableComparison);
        }

        /// <inheritdoc cref="IsSorted{TComp}(IList{TComp})"/>
        public static bool IsSorted<T>(this IList<T> list, Comparison<T> comparison)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (comparison(list[i], list[i + 1]) == 1)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Quicksort methods
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
