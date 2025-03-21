using System;

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
        /// The number of objects currently in the pool
        /// </summary>
        /// <remarks>
        /// Remark : <i> It is always possible to request items from the pool event if there are none available : the pool will simply create new ones</i>
        /// </remarks>
        int ElementsInPool { get; }

        /// <summary>
        /// Event called when an object is requested from the pool
        /// </summary>
        event Action OnObjectRequested;

        /// <summary>
        /// Event called when an object is released and gets back in the pool
        /// </summary>
        event Action OnObjectReleased;

        /// <summary>
        /// Requests an object from the pool
        /// </summary>
        /// <returns>A wrapper arround the requested object</returns>
        PooledObject<T> Request();

        /// <summary>
        /// Releases the object so that it can be re-used by something else
        /// </summary>
        /// <param name="releasedObject">The object to release</param>
        void Release(T releasedObject);
    }
}