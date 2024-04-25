using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace UnityUtility.ObservableList
{
    [Flags]
    public enum ListChangeOperations : int
    {
        Add = 0x1,
        Remove = 0x2,
        Edit = 0x4,
        Sort = 0x8,


        AtIndex = 0x100,
        Range = 0x200,

        Clear = 0x400,

        Insert = Add | AtIndex,
        RemoveAt = Remove | AtIndex,
        AddRange = Add | Range,
    }

    [Serializable]
    public class ObservableList<T> : ICollection<T>, IEnumerable<T>, IEnumerable, IList<T>, IReadOnlyCollection<T>, IReadOnlyList<T>
    {
        [SerializeField] private List<T> m_underlyingList;

        public event Action<ListChangeOperations> OnListChanged;
        public event Action<int> OnItemAdded;
        public event Action<int> OnItemRemoved;
        public event Action<int> OnItemEdited;

        public event Action OnListSorted;
        public event Action OnListCleared;

        public event Action<T, int> OnItemInserted;
        public event Action<T, int> OnItemRemovedAt;
        public event Action<IEnumerable<T>, int> OnRangeAdded;

        #region Contructors
        public ObservableList()
        {
            m_underlyingList = new List<T>();
        }

        public ObservableList(IEnumerable<T> collection)
        {
            m_underlyingList = new List<T>(collection);
        }

        public ObservableList(int capacity)
        {
            m_underlyingList = new List<T>(capacity);
        }
        #endregion

        #region Interfaces Implemtation
        public T this[int index] { get => m_underlyingList[index]; set => m_underlyingList[index] = value; }

        public int Count => m_underlyingList.Count; 
        public int Capacity { get => m_underlyingList.Capacity; set => m_underlyingList.Capacity = value; }

        public bool IsReadOnly => ((ICollection<T>)m_underlyingList).IsReadOnly;

        public void Add(T item)
        {
            m_underlyingList.Add(item);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            m_underlyingList.AddRange(collection);
        }

        public ReadOnlyCollection<T> AsReadOnly()
        {
            return m_underlyingList.AsReadOnly();
        }

        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            return m_underlyingList.BinarySearch(index, count, item, comparer);
        }

        public int BinarySearch(T item)
        {
            return m_underlyingList.BinarySearch(item);
        }

        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return m_underlyingList.BinarySearch(item, comparer);
        }

        public void Clear()
        {
            m_underlyingList.Clear();
        }

        public bool Contains(T item)
        {
            return m_underlyingList.Contains(item);
        }

        public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            return m_underlyingList.ConvertAll(converter);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            m_underlyingList.CopyTo(array, arrayIndex);
        }

        public void CopyTo(T[] array)
        {
            m_underlyingList.CopyTo(array);
        }

        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            m_underlyingList.CopyTo(index, array, arrayIndex, count);
        }

        public bool Exists(Predicate<T> match)
        {
            return m_underlyingList.Exists(match);
        }

        public T Find(Predicate<T> match)
        {
            return m_underlyingList.Find(match);
        }

        public List<T> FindAll(Predicate<T> match)
        {
            return m_underlyingList.FindAll(match);
        }

        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return m_underlyingList.FindIndex(startIndex, count, match);
        }

        public int FindIndex(int startIndex, Predicate<T> match)
        {
            return m_underlyingList.FindIndex(startIndex, match);
        }

        public int FindIndex(Predicate<T> match)
        {
            return m_underlyingList.FindIndex(match);
        }

        public T FindLast(Predicate<T> match)
        {
            return m_underlyingList.FindLast(match);
        }

        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            return m_underlyingList.FindLastIndex(startIndex, count, match);
        }

        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            return m_underlyingList.FindLastIndex(startIndex, match);
        }

        public int FindLastIndex(Predicate<T> match)
        {
            return m_underlyingList.FindLastIndex(match);
        }

        public void ForEach(Action<T> action)
        {
            m_underlyingList.ForEach(action);
        }

        public List<T>.Enumerator GetEnumerator()
        {
            return m_underlyingList.GetEnumerator();
        }

        public List<T> GetRange(int index, int count)
        {
            return m_underlyingList.GetRange(index, count);
        }

        public int IndexOf(T item, int index, int count)
        {
            return m_underlyingList.IndexOf(item, index, count);
        }

        public int IndexOf(T item, int index)
        {
            return m_underlyingList.IndexOf(item, index);
        }

        public int IndexOf(T item)
        {
            return m_underlyingList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            m_underlyingList.Insert(index, item);
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            m_underlyingList.InsertRange(index, collection);
        }

        public int LastIndexOf(T item)
        {
            return m_underlyingList.LastIndexOf(item);
        }

        public int LastIndexOf(T item, int index)
        {
            return m_underlyingList.LastIndexOf(item, index);
        }

        public int LastIndexOf(T item, int index, int count)
        {
            return m_underlyingList.LastIndexOf(item, index, count);
        }

        public bool Remove(T item)
        {
            return m_underlyingList.Remove(item);
        }

        public int RemoveAll(Predicate<T> match)
        {
            return m_underlyingList.RemoveAll(match);
        }

        public void RemoveAt(int index)
        {
            m_underlyingList.RemoveAt(index);
        }

        public void RemoveRange(int index, int count)
        {
            m_underlyingList.RemoveRange(index, count);
        }

        public void Reverse(int index, int count)
        {
            m_underlyingList.Reverse(index, count);
        }

        public void Reverse()
        {
            m_underlyingList.Reverse();
        }

        public void Sort(Comparison<T> comparison)
        {
            m_underlyingList.Sort(comparison);
        }

        public void Sort(int index, int count, IComparer<T> comparer)
        {
            m_underlyingList.Sort(index, count, comparer);
        }

        public void Sort()
        {
            m_underlyingList.Sort();
        }

        public void Sort(IComparer<T> comparer)
        {
            m_underlyingList.Sort(comparer);
        }

        public T[] ToArray()
        {
            return m_underlyingList.ToArray();
        }

        public void TrimExcess()
        {
            m_underlyingList.TrimExcess();
        }

        public bool TrueForAll(Predicate<T> match)
        {
            return m_underlyingList.TrueForAll(match);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return m_underlyingList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_underlyingList.GetEnumerator();
        }
        #endregion
    }
}
