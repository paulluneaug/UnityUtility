namespace UnityUtility.Pools
{
    /// <summary>
    /// A wrapper arround an object from a pool allowing to release it easily
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public readonly struct PooledObject<T> where T : class
    {
        public readonly T Object => m_object;

        public readonly IObjectPool<T> Pool => m_pool;

        private readonly T m_object;
        private readonly IObjectPool<T> m_pool;

        public PooledObject(T obj, IObjectPool<T> pool)
        {
            m_object = obj;
            m_pool = pool;
        }

        /// <summary>
        /// Releases the underlying object into its original pool
        /// </summary>
        public readonly void Release()
        {
            m_pool.Release(m_object);
        }
    }
}