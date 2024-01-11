namespace UnityUtility.Pools
{
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

        public readonly void Release()
        {
            m_pool.Release(m_object);
        }
    }
}