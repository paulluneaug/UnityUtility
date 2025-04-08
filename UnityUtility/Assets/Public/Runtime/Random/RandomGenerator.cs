using System;
using System.Runtime.CompilerServices;

using UnityEngine;

using UnityUtility.Hash;
using UnityUtility.MathU;

namespace UnityUtility.Random
{
    /// <summary>
    /// Object that holds a hash value and can provide pseudo-random values
    /// </summary>
    public class RandomGenerator
    {
        private uint m_seed;

        public RandomGenerator()
        {
            m_seed = Hasher.IntToUInt(GetHashCode());
        }

        public RandomGenerator(int seed) : this(Hasher.IntToUInt(seed))
        {
        }

        public RandomGenerator(uint seed)
        {
            m_seed = Hasher.Hash(seed);
        }

        /// <inheritdoc cref="RandomUtils.RandomFloat01(ref uint)"/>
        public float RandomFloat01()
        {
            return RandomFloat01(ref m_seed);
        }

        /// <inheritdoc cref="RandomUtils.RandomFloat(ref uint, float, float)"/>
        public float RandomFloat(float min, float max)
        {
            return RandomFloat(ref m_seed, min, max);
        }

        /// <inheritdoc cref="RandomUtils.RandomFloat(ref uint, Vector2)"/>
        public float RandomFloat(Vector2 range)
        {
            return RandomFloat(ref m_seed, range);
        }

        /// <inheritdoc cref="RandomUtils.RandomInt(ref uint)"/>
        public int RandomInt()
        {
            return RandomInt(ref m_seed);
        }

        /// <inheritdoc cref="RandomUtils.RandomInt(ref uint, int, int)"/>
        public int RandomInt(int min, int max)
        {
            return RandomInt(ref m_seed, min, max);
        }

        /// <inheritdoc cref="RandomUtils.RandomInt(ref uint, Vector2Int)"/>
        public int RandomInt(Vector2Int range)
        {
            return RandomInt(ref m_seed, range);
        }

        /// <inheritdoc cref="RandomUtils.RandomBool(ref uint)"/>
        public bool RandomBool()
        {
            return RandomBool(ref m_seed);
        }

        /// <inheritdoc cref="RandomUtils.RandomBoolProb(ref uint, float)"/>
        public bool RandomBoolProb(float probability)
        {
            return RandomBoolProb(ref m_seed, probability);
        }

        /// <inheritdoc cref="RandomUtils.RandomOnUnitCircle(ref uint)"/>
        public Vector2 RandomOnUnitCircle()
        {
            return RandomOnUnitCircle(ref m_seed);
        }

        /// <inheritdoc cref="RandomUtils.RandomInUnitCircle(ref uint)"/>
        public Vector2 RandomInUnitCircle()
        {
            return RandomInUnitCircle(ref m_seed);
        }

        /// <inheritdoc cref="RandomUtils.RandomOnUnitSphere(ref uint)"/>
        public Vector3 RandomOnUnitSphere()
        {
            return RandomOnUnitSphere(ref m_seed);
        }

        /// <inheritdoc cref="RandomUtils.RandomInUnitSphere(ref uint)"/>
        public Vector3 RandomInUnitSphere()
        {
            return RandomInUnitSphere(ref m_seed);
        }

        /// <inheritdoc cref="RandomUtils.RandomRotation(ref uint)"/>
        public Quaternion RandomRotation()
        {
            return RandomRotation(ref m_seed);
        }

        #region Static Methods

        /// <summary>
        /// Returns a pseudo random <see cref="float"/> between 0 and 1
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RandomFloat01(ref uint seed)
        {
            return Hasher.Hash(ref seed) / 4294967295.0f; // 2^32-1
        }

        /// <summary>
        /// Returns a pseudo random <see cref="float"/> between <paramref name="min"/> and <paramref name="max"/>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RandomFloat(ref uint seed, float min, float max)
        {
            return RandomFloat01(ref seed).RemapFrom01(min, max);
        }

        /// <summary>
        /// Returns a pseudo random <see cref="float"/> within the given <paramref name="range"/>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RandomFloat(ref uint seed, Vector2 range)
        {
            return RandomFloat01(ref seed).RemapFrom01(range);
        }

        /// <summary>
        /// Returns a pseudo random <see cref="int"/>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandomInt(ref uint seed)
        {
            return Hasher.UIntToInt(Hasher.Hash(ref seed));
        }

        /// <summary>
        /// Returns a pseudo random <see cref="int"/> within the interval [<paramref name="min"/>, <paramref name="max"/>[
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandomInt(ref uint seed, int min, int max)
        {
            return (int)RandomFloat01(ref seed).RemapFrom01(min, max);
        }

        /// <summary>
        /// Returns a pseudo random <see cref="int"/> within the given <paramref name="range"/>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandomInt(ref uint seed, Vector2Int range)
        {
            return RandomInt(ref seed, range.x, range.y);
        }

        /// <summary>
        /// Returns a pseudo random <see cref="uint"/>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint RandomUInt(ref uint seed)
        {
            return Hasher.Hash(ref seed);
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
        /// Returns a pseudo random <see cref="Vector2"/> whose magnitude is equal to 1
        /// </summary>
        public static Vector2 RandomOnUnitCircle(ref uint seed)
        {
            float angle = RandomFloat(ref seed, 0.0f, MathUf.TAU);
            return new Vector2(MathUf.Cos(angle), MathUf.Sin(angle));
        }

        /// <summary>
        /// Returns a pseudo random <see cref="Vector2"/> whose magnitude is less than 1
        /// </summary>
        public static Vector2 RandomInUnitCircle(ref uint seed)
        {
            return RandomOnUnitCircle(ref seed) * RandomFloat01(ref seed);
        }

        /// <summary>
        /// Returns a pseudo random <see cref="Vector3"/> whose magnitude is less than 1
        /// </summary>
        public static Vector3 RandomInUnitSphere(ref uint seed)
        {
            float x, y, z;
            do
            {
                x = RandomFloat01(ref seed) * 2.0f - 1.0f;
                y = RandomFloat01(ref seed) * 2.0f - 1.0f;
                z = RandomFloat01(ref seed) * 2.0f - 1.0f;
            } while (x * x + y * y + z * z > 1.0f);

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Returns a pseudo random <see cref="Vector3"/> whose magnitude is equal to 1
        /// </summary>
        public static Vector3 RandomOnUnitSphere(ref uint seed)
        {
            return RandomInUnitSphere(ref seed).normalized;
        }

        /// <summary>
        /// Returns a pseudo random uniform rotation
        /// </summary>
        public static Quaternion RandomRotation(ref uint seed)
        {
            Vector2 xy = RandomInUnitCircle(ref seed);
            Vector2 uv = RandomInUnitCircle(ref seed);
            float s = MathUf.Sqrt((1.0f - xy.SqrMagnitude()) / uv.SqrMagnitude());

            return new Quaternion(xy.x, xy.y, s * uv.x, s * uv.y);
        }
        #endregion
    }
}