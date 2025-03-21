using UnityEngine;

using UnityUtility.Hasher;

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
            m_seed = HashUtils.IntToUInt(GetHashCode());
        }

        public RandomGenerator(int seed) : this(HashUtils.IntToUInt(seed))
        {
        }

        public RandomGenerator(uint seed)
        {
            m_seed = HashUtils.Hash(seed);
        }

        /// <inheritdoc cref="RandomUtils.RandomFloat01(ref uint)"/>
        public float RandomFloat01()
        {
            return RandomUtils.RandomFloat01(ref m_seed);
        }

        /// <inheritdoc cref="RandomUtils.RandomFloat(ref uint, float, float)"/>
        public float RandomFloat(float min, float max)
        {
            return RandomUtils.RandomFloat(ref m_seed, min, max);
        }

        /// <inheritdoc cref="RandomUtils.RandomFloat(ref uint, Vector2)"/>
        public float RandomFloat(Vector2 range)
        {
            return RandomUtils.RandomFloat(ref m_seed, range);
        }

        /// <inheritdoc cref="RandomUtils.RandomInt(ref uint)"/>
        public int RandomInt()
        {
            return RandomUtils.RandomInt(ref m_seed);
        }

        /// <inheritdoc cref="RandomUtils.RandomInt(ref uint, int, int)"/>
        public int RandomInt(int min, int max)
        {
            return RandomUtils.RandomInt(ref m_seed, min, max);
        }

        /// <inheritdoc cref="RandomUtils.RandomInt(ref uint, Vector2Int)"/>
        public int RandomInt(Vector2Int range)
        {
            return RandomUtils.RandomInt(ref m_seed, range);
        }

        /// <inheritdoc cref="RandomUtils.RandomBool(ref uint)"/>
        public bool RandomBool()
        {
            return RandomUtils.RandomBool(ref m_seed);
        }

        /// <inheritdoc cref="RandomUtils.RandomBoolProb(ref uint, float)"/>
        public bool RandomBoolProb(float probability)
        {
            return RandomUtils.RandomBoolProb(ref m_seed, probability);
        }

        /// <inheritdoc cref="RandomUtils.RandomOnUnitCircle(ref uint)"/>
        public Vector2 RandomOnUnitCircle()
        {
            return RandomUtils.RandomOnUnitCircle(ref m_seed);
        }

        /// <inheritdoc cref="RandomUtils.RandomInUnitCircle(ref uint)"/>
        public Vector2 RandomInUnitCircle()
        {
            return RandomUtils.RandomInUnitCircle(ref m_seed);
        }

        /// <inheritdoc cref="RandomUtils.RandomOnUnitSphere(ref uint)"/>
        public Vector3 RandomOnUnitSphere()
        {
            return RandomUtils.RandomOnUnitSphere(ref m_seed);
        }

        /// <inheritdoc cref="RandomUtils.RandomInUnitSphere(ref uint)"/>
        public Vector3 RandomInUnitSphere()
        {
            return RandomUtils.RandomInUnitSphere(ref m_seed);
        }

        /// <inheritdoc cref="RandomUtils.RandomRotation(ref uint)"/>
        public Quaternion RandomRotation()
        {
            return RandomUtils.RandomRotation(ref m_seed);
        }
    }
}