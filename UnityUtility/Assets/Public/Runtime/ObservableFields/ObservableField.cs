using System;

using UnityEngine;

namespace UnityUtility.ObservableFields
{
    [Serializable]
    public class ObservableField<T>
    {
#if UNITY_EDITOR
        public const string UNDERLYING_VALUE_NAME = nameof(m_underlyingValue);
#endif

        public event Action<T> OnValueChanged;

        public T Value
        {
            get => m_underlyingValue;
            set
            {
                m_underlyingValue = value;
                OnValueChanged?.Invoke(value);
            }
        }

        [SerializeField] private T m_underlyingValue;

        public ObservableField(T value)
        {
            m_underlyingValue = value;
        }
    }
}
