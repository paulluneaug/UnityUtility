using System;
using System.Runtime.CompilerServices;
using UnityUtility.Utils;

namespace UnityUtility.Hash
{
    public static class HashUtils
    {
        /// <summary>
        /// Transforms an <see cref="int"/> into a <see cref="uint"/> without changing its bits
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint IntToUInt(int val)
        {
            return unchecked((uint)val);
        }

        /// <summary>
        /// Transforms a <see cref="uint"/> into an <see cref="int"/> without changing its bits
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int UIntToInt(uint val)
        {
            return unchecked((int)val);
        }

        /// <summary>
        /// Hashes the given value into a new pseudo random value
        /// </summary>
        /// <remarks><i>
        /// Hash function from H. Schechter and R. Bridson 
        /// (<see href="https://www.cs.ubc.ca/~rbridson/docs/schechter-sca08-turbulence.pdf">Link to their paper</see>)
        /// </i></remarks>
        /// <param name="s"></param>
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

        /// <inheritdoc cref="Hash(uint)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Hash(ref uint s)
        {
            s = Hash(s);
            return s;
        }

        /// <summary>
        /// Creates a hash value from two <see langword="uint"/>
        /// </summary>
        /// <remarks><i>
        /// From the 
        /// <see href="https://www.boost.org/doc/libs/1_55_0/doc/html/hash/reference.html#boost.hash_combine">hash_combine</see>
        /// method of the C++ boost library
        /// </i></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Combine(uint a, uint b)
        {
            return a ^ (Hash(b) + 0x9e3779b9 + (a << 6) + (a >> 2));
        }

        /// <summary>
        /// Returns a pseudo random <see cref="float"/> between 0 and 1
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RandomFloat01(ref uint seed)
        {
            return Hash(ref seed) / 4294967295.0f; // 2^32-1
        }

        /// <summary>
        /// Returns a pseudo random <see cref="int"/>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandomInt(ref uint seed)
        {
            return UIntToInt(Hash(ref seed));
        }

        /// <summary>
        /// Returns a pseudo random <see cref="uint"/>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint RandomUInt(ref uint seed)
        {
            return Hash(ref seed);
        }

        /// <summary>
        /// Returns a pseudo random <see cref="bool"/>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RandomBool(ref uint seed)
        {
            return Math.Sign(RandomInt(ref seed)) == 1;
        }

        /// <summary>
        /// Returns <see langword="true"/> according to the given <paramref name="probability"/>
        /// </summary>
        /// <param name="probability">The probability of returning <see langword="true"/></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RandomBoolProb(ref uint seed, float probability)
        {
            return RandomFloat01(ref seed) < probability;
        }

        /// <summary>
        /// Returns a pseudo-random <see cref="int"/> within the interval [<paramref name="lowerBound"/>; <paramref name="higherBound"/>[
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="lowerBound"></param>
        /// <param name="higherBound"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandomIntInRange(ref uint seed, int lowerBound, int higherBound)
        {
            return (int)RandomFloat01(ref seed).RemapFrom01(lowerBound, higherBound);
        }
    }
}
