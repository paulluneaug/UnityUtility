namespace UnityUtility.Pools
{
    public interface IObjectPool<T> where T : class
    {
        int PoolSize { get; }
        public PooledObject<T> Request();
        public void Release(T releasedObject);
    }
}