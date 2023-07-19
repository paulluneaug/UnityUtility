using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SerializedDictionary<TKey, TValue> : ISerializedDictionary, IDictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [Serializable]
    private struct KeyValuePair
    {
        [SerializeField] public TKey Key;
        [SerializeField] public TValue Value;

        public KeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is KeyValuePair pair &&
                   EqualityComparer<TKey>.Default.Equals(Key, pair.Key) &&
                   EqualityComparer<TValue>.Default.Equals(Value, pair.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Key, Value);
        }
    }

    [SerializeField] private List<KeyValuePair> m_keyValuePairsList = new List<KeyValuePair>();

    private Dictionary<TKey, TValue> m_dictionary = new Dictionary<TKey, TValue>();
    private bool m_duplicateKeys = false;

    #region IDictionary Implementation
    public TValue this[TKey key] { get => ((IDictionary<TKey, TValue>)m_dictionary)[key]; set => ((IDictionary<TKey, TValue>)m_dictionary)[key] = value; }

    public ICollection<TKey> Keys => ((IDictionary<TKey, TValue>)m_dictionary).Keys;

    public ICollection<TValue> Values => ((IDictionary<TKey, TValue>)m_dictionary).Values;

    public int Count => ((ICollection<KeyValuePair<TKey, TValue>>)m_dictionary).Count;

    public bool IsReadOnly => ((ICollection<KeyValuePair<TKey, TValue>>)m_dictionary).IsReadOnly;

    public void Add(TKey key, TValue value)
    {
        ((IDictionary<TKey, TValue>)m_dictionary).Add(key, value);
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        ((ICollection<KeyValuePair<TKey, TValue>>)m_dictionary).Add(item);
    }

    public void Clear()
    {
        ((ICollection<KeyValuePair<TKey, TValue>>)m_dictionary).Clear();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return ((ICollection<KeyValuePair<TKey, TValue>>)m_dictionary).Contains(item);
    }

    public bool ContainsKey(TKey key)
    {
        return ((IDictionary<TKey, TValue>)m_dictionary).ContainsKey(key);
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        ((ICollection<KeyValuePair<TKey, TValue>>)m_dictionary).CopyTo(array, arrayIndex);
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<TKey, TValue>>)m_dictionary).GetEnumerator();
    }

    public bool Remove(TKey key)
    {
        return ((IDictionary<TKey, TValue>)m_dictionary).Remove(key);
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        return ((ICollection<KeyValuePair<TKey, TValue>>)m_dictionary).Remove(item);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        return ((IDictionary<TKey, TValue>)m_dictionary).TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)m_dictionary).GetEnumerator();
    }
    #endregion

    #region ISerializationCallbackReceiver Implementation
    public void OnBeforeSerialize()
    {
        if (m_duplicateKeys)
        {
            foreach (KeyValuePair<TKey, TValue> pair in m_dictionary)
            {
                KeyValuePair newPair = new KeyValuePair(pair.Key, pair.Value);
                if (m_keyValuePairsList.Contains(newPair))
                {
                    continue;
                }
                m_keyValuePairsList.Add(newPair);
            }
        }
        else
        {
            m_keyValuePairsList = new List<KeyValuePair>();
            foreach (KeyValuePair<TKey, TValue> pair in m_dictionary)
            {
                m_keyValuePairsList.Add(new KeyValuePair(pair.Key, pair.Value));
            }
        }
    }

    public void OnAfterDeserialize()
    {
        m_dictionary = new Dictionary<TKey, TValue>();
        m_duplicateKeys = false;
        foreach (KeyValuePair pair in m_keyValuePairsList)
        {
            if (m_dictionary.ContainsKey(pair.Key))
            {
                m_duplicateKeys = true;
                continue;
            }
            m_dictionary.Add(pair.Key, pair.Value);
        }
    }
    #endregion
}

public interface ISerializedDictionary
{

}
