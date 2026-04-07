using System;
using System.Collections.Generic;

namespace UnityUtility.Pools
{
    /// <summary>
    /// Stack-based object pool
    /// 
    /// <para>
    /// See also :
    /// <br><seealso cref="CallbackRecieverObjectPool{T}"/></br>
    /// <br><seealso cref="ComponentPool{TComponent}"/></br>
    /// <br><seealso cref="CallbackRecieverComponentPool{TComponent}"/></br>
    /// </para>
    /// </summary>
    /// <typeparam name="T">Pooled object type</typeparam>
    public class ObjectPool<T> : IObjectPool<T> where T : class
    {
        public int PoolSize => m_poolSize;
        public int ElementsInPool => m_availableObjects.Count;

        public event Action OnObjectRequested;
        public event Action OnObjectReleased;

        protected readonly Stack<T> m_availableObjects = null;
        private int m_poolSize = 0;

        private readonly PoolObjectConstructor<T> m_constructor;

        public ObjectPool(int initialPoolSize, PoolObjectConstructor<T> constructor)
        {
            m_constructor = constructor;

            m_availableObjects = new Stack<T>(initialPoolSize);
            m_poolSize = 0;
            Populate(initialPoolSize);
        }

        public virtual PooledObject<T> Request()
        {
            if (m_availableObjects.Count == 0)
            {
                AddItem();
            }

            PooledObject<T> requestedObject = new PooledObject<T>(m_availableObjects.Pop(), this);

            OnObjectRequested?.Invoke();

            return requestedObject;
        }

        public virtual void Release(T releasedObject)
        {
            m_availableObjects.Push(releasedObject);
            OnObjectReleased?.Invoke();
        }

        protected virtual void Populate(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                AddItem();
            }
        }

        protected virtual void AddItem()
        {
            m_availableObjects.Push(NewItem(m_poolSize++));
        }

        protected virtual T NewItem(int index)
        {
            return m_constructor(index);
        }

        public virtual void Dispose()
        {
            m_availableObjects.Clear();
            m_poolSize = 0;
        }
    }
}
