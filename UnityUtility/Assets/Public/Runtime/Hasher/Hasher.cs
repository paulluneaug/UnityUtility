using UnityEngine;

namespace UnityUtility.Hash
{
    /// <summary>
    /// Hasher that holds a hash value and can provide pseudo-random values
    /// </summary>
    public class Hasher
    {
        /// <summary>
        /// The current value of the hash
        /// </summary>
        public uint CurrentHash => m_hash;

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

        /// <summary>
        /// Adds a new object to the hash
        /// </summary>
        /// <typeparam name="T">The type of the object to add to the hash</typeparam>
        /// <param name="addedObject">The object to add to the hash</param>
        /// <returns>The new hash value</returns>
        public uint Add<T>(T addedObject)
        {
            m_hash = HashUtils.Combine(m_hash, HashUtils.IntToUInt(addedObject.GetHashCode()));
            return m_hash;
        }

        /// <summary>
        /// Hashes the current hash value
        /// </summary>
        /// <remarks>
        /// For more details on the hash function used : 
        /// <see cref="HashUtils.Hash(uint)"/><br/>
        /// </remarks>
        /// <returns>The new current hash value</returns>
        public uint Hash()
        {
            return HashUtils.Hash(ref m_hash);
        }

        /// <inheritdoc cref="HashUtils.RandomFloat01(ref uint)"/>
        public float RandomFloat01()
        {
            return HashUtils.RandomFloat01(ref m_hash);
        }

        /// <inheritdoc cref="HashUtils.RandomFloat(ref uint, float, float)"/>
        public float RandomFloat(float min, float max)
        {
            return HashUtils.RandomFloat(ref m_hash, min, max);
        }

        /// <inheritdoc cref="HashUtils.RandomFloat(ref uint, Vector2)"/>
        public float RandomFloat(Vector2 range)
        {
            return HashUtils.RandomFloat(ref m_hash, range);
        }

        /// <inheritdoc cref="HashUtils.RandomInt(ref uint)"/>
        public int RandomInt()
        {
            return HashUtils.RandomInt(ref m_hash);
        }

        /// <inheritdoc cref="HashUtils.RandomInt(ref uint, int, int)"/>
        public int RandomInt(int min, int max)
        {
            return HashUtils.RandomInt(ref m_hash, min, max);
        }

        /// <inheritdoc cref="HashUtils.RandomInt(ref uint, Vector2Int)"/>
        public int RandomInt(Vector2Int range)
        {
            return HashUtils.RandomInt(ref m_hash, range);
        }

        /// <inheritdoc cref="HashUtils.RandomBool(ref uint)"/>
        public bool RandomBool()
        {
            return HashUtils.RandomBool(ref m_hash);
        }

        /// <inheritdoc cref="HashUtils.RandomBoolProb(ref uint, float)"/>
        public bool RandomBoolProb(float probability)
        {
            return HashUtils.RandomBoolProb(ref m_hash, probability);
        }
    }
}