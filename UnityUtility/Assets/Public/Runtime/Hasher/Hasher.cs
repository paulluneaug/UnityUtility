using System.Runtime.CompilerServices;

namespace UnityUtility.Hash
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
            m_hash = IntToUInt(GetHashCode());
        }

        public Hasher(int seed) : this(IntToUInt(seed))
        {
        }

        public Hasher(uint seed)
        {
            m_hash = Hash(seed);
        }

        /// <summary>
        /// Adds a new object to the hash
        /// </summary>
        /// <typeparam name="T">The type of the object to add to the hash</typeparam>
        /// <param name="addedObject">The object to add to the hash</param>
        /// <returns>The new hash value</returns>
        public uint Add<T>(T addedObject)
        {
            m_hash = Combine(m_hash, IntToUInt(addedObject.GetHashCode()));
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
            return Hash(ref m_hash);
        }

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
    }
}
