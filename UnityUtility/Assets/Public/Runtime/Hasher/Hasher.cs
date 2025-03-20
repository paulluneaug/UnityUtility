namespace UnityUtility.Hasher
{
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
        /// <see cref="RandomUtils.Hash(uint)"/><br/>
        /// </remarks>
        /// <returns>The new current hash value</returns>
        public uint Hash()
        {
            return HashUtils.Hash(ref m_hash);
        }
    }
}
