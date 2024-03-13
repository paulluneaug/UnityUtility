namespace UnityUtility.Pools
{
    /// <summary>
    /// Base interface for an object pool
    /// 
    /// <para>
    /// Contains :
    /// <br><seealso cref="PoolSize"/></br>
    /// <br><seealso cref="Request"/></br>
    /// <br><seealso cref="Release"/></br>
    /// </para>
    /// </summary>
    /// <typeparam name="T">Pooled object type</typeparam>
    public interface IObjectPool<T> where T : class
    {
        /// <summary>
        /// The number of object created by the pool
        /// </summary>
        int PoolSize { get; }

        /// <summary>
        /// Requests an object from the pool
        /// </summary>
        /// <returns>A wrapper arround the requested object</returns>
        public PooledObject<T> Request();

        /// <summary>
        /// Releases the object so that it can be re-used by something else
        /// </summary>
        /// <param name="releasedObject">The object to release</param>
        public void Release(T releasedObject);
    }
}