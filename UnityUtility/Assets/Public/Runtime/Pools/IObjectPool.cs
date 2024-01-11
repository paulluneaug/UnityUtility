namespace UnityUtility.Pools
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Pooled object type</typeparam>
    public interface IObjectPool<T> where T : class
    {
        int PoolSize { get; }
        public PooledObject<T> Request();
        public void Release(T releasedObject);
    }
}