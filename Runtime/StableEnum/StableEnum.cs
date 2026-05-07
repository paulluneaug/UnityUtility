// Based on DotSquid's "StableEnum" package : https://github.com/dotsquid/StableEnum

using System;
using System.Collections.Generic;

using UnityEngine;

namespace UnityUtility
{
    [Serializable]
    public struct StableEnum<T> : IEquatable<StableEnum<T>>, ISerializationCallbackReceiver
        where T : struct
    {
#if UNITY_EDITOR
        public const string VALUE_FIELD_NAME = nameof(m_value);
#endif

        public T Value
        {
            readonly get => m_value;
            set => m_value = value;
        }

        [SerializeField]
        private T m_value;

        [SerializeField, HideInInspector]
        private string _proxy;
        [SerializeField, HideInInspector]
        private int _index;

        public StableEnum(T value)
        {
            m_value = value;
            _proxy = Enum.GetName(typeof(T), m_value);
            _index = (int)(ValueType)m_value;
        }

        public void OnBeforeSerialize()
        {
            _proxy = Enum.GetName(typeof(T), m_value);
            _index = (int)(ValueType)m_value;
        }

        public void OnAfterDeserialize()
        {
            if (!Enum.TryParse(_proxy, out m_value))
            {
                if (Enum.IsDefined(typeof(T), _index))
                {
                    m_value = (T)(ValueType)_index; // awful boxing, stupid c#  (╯°□°）╯︵ ┻━┻
                }
                else
                {
                    Debug.LogError($"Deserialization failed: \"{typeof(T)}\" enum has neither \"{_proxy}\" value, nor \"{_index}\" index");
                }
            }
        }

        public readonly bool Equals(StableEnum<T> other)
        {
            return EqualityComparer<T>.Default.Equals(m_value, other.Value);
        }

        public override readonly bool Equals(object obj)
        {
            if (obj is not StableEnum<T> stable)
            {
                return false;
            }
            return Equals(stable);
        }

        public override readonly int GetHashCode()
        {
            return m_value.GetHashCode();
        }

        public static implicit operator T(StableEnum<T> stableEnum)
        {
            return stableEnum.m_value;
        }

        public static T Convert(StableEnum<T> stableEnum)
        {
            return stableEnum.m_value;
        }
    }

}

