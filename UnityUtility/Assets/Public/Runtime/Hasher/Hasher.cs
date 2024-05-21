using System;
using System.Runtime.CompilerServices;

namespace UnityUtility.Hash
{
    public class Hasher
    {
        private uint m_hash;

        public Hasher()
        {
            m_hash = HashUtils.IntToUInt(GetHashCode());
        }

        public Hasher(int seed) : this(HashUtils.IntToUInt(seed))
        {
        }

        public Hasher(uint seed)
        {
            m_hash = HashUtils.Hash(seed);
        }

        public uint Add<T>(T other)
        {
            m_hash = HashUtils.Combine(m_hash, HashUtils.IntToUInt(other.GetHashCode()));
            return m_hash;
        }

        public uint Hash()
        {
            return HashUtils.Hash(ref m_hash);
        }

        public float RandomFloat01()
        {
            return HashUtils.RandomFloat01(ref m_hash);
        }

        public int RandomInt()
        {
            return HashUtils.RandomInt(ref m_hash);
        }

        public bool RandomBool()
        {
            return HashUtils.RandomBool(ref m_hash);
        }

        public bool RandomBoolProb(float probability)
        {
            return HashUtils.RandomFloat01(ref m_hash) < probability;
        }
    }

    public static class HashUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint IntToUInt(int val)
        {
            return unchecked((uint)val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int UIntToInt(uint val)
        {
            return unchecked((int)val);
        }

        // Hash function from H. Schechter & R. Bridson : goo.gl/RXiKaH
        public static uint Hash(uint s)
        {
            s ^= 2747636419u;
            s *= 2654435769u;
            s ^= s >> 16;
            s *= 2654435769u;
            s ^= s >> 16;
            s *= 2654435769u;
            return s;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Hash(ref uint s)
        {
            s = Hash(s);
            return s;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Combine(uint a, uint b)
        {
            // From the hash_combine method of the c++ boost library
            // https://www.boost.org/doc/libs/1_55_0/doc/html/hash/reference.html#boost.hash_combine
            return a ^ (Hash(b) + 0x9e3779b9 + (a << 6) + (a >> 2));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RandomFloat01(ref uint seed)
        {
            return Hash(ref seed) / 4294967295.0f; // 2^32-1
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandomInt(ref uint seed)
        {
            return UIntToInt(Hash(ref seed));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint RandomUInt(ref uint seed)
        {
            return Hash(ref seed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RandomBool(ref uint seed)
        {
            return Math.Sign(RandomInt(ref seed)) == 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RandomBoolProb(ref uint seed, float probability)
        {
            return RandomFloat01(ref seed) < probability;
        }
    }
}