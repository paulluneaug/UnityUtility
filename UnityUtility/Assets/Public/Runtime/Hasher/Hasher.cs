using System;
using UnityUtility.Utils;

namespace UnityUtility.Hash
{
    public class Hasher
    {
        private int m_hash;

        public Hasher()
        {
            m_hash = GetHashCode();
        }

        public Hasher(int seed)
        {
            m_hash = HasherUtils.Hash(seed);
        }

        public int Add<T>(T other)
        {
            m_hash = HasherUtils.Combine(m_hash, other.GetHashCode());
            return m_hash;
        }

        public int Hash()
        {
            return HasherUtils.Hash(ref m_hash);
        }

        public float RandomFloat01()
        {
            return HasherUtils.RandomFloat01(ref m_hash);
        }
        public float RandomFloat()
        {
            return HasherUtils.RandomFloat(ref m_hash);
        }

        public int RandomInt()
        {
            return HasherUtils.RandomInt(ref m_hash);
        }

        public bool RandomBool()
        {
            return HasherUtils.RandomBool(ref m_hash);
        }
    }

    public static class HasherUtils
    {
        public static int Hash(int s)
        {
            s = ((s >> 16) ^ s) * 0x45d9f3b;
            s = ((s >> 16) ^ s) * 0x45d9f3b;
            s = (s >> 16) ^ s;
            return s;
        }

        public static int Hash(ref int s)
        {
            s = Hash(s);
            return s;
        }

        public static int Combine(int a, int b)
        {
            // From the hash_combine method of the c++ boost library
            // https://www.boost.org/doc/libs/1_55_0/doc/html/hash/reference.html#boost.hash_combine
            unchecked
            {
                return a ^ (Hash(b) + (int)0x9e3779b9 + (a << 6) + (a >> 2));
            }
        }

        public static float RandomFloat01(ref int seed)
        {
            return (Hash(ref seed) - (float)int.MinValue) / 4294967295.0f; // 2^32-1
        }

        public static float RandomFloat(ref int seed)
        {
            return RandomFloat01(ref seed).RemapFrom01(float.MinValue, float.MaxValue);
        }

        public static int RandomInt(ref int seed)
        {
            return Hash(ref seed);
        }

        public static bool RandomBool(ref int seed)
        {
            return Math.Sign(Hash(ref seed)) == 1;
        }
    }
}