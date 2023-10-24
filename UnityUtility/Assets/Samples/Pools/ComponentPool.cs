using System.Collections.Generic;

using UnityEngine;

namespace UnityUtility.Pools
{
    public abstract class ComponentPool<TComponent> : MonoBehaviour, IObjectPool<TComponent> where TComponent : Component
    {
        public int PoolSize => m_poolSize;

        [SerializeField] private int m_initialPoolSize = 10;

        private readonly Stack<TComponent> m_availableComponents = null;
        private int m_poolSize = 0;

        protected virtual void Awake()
        {
            for (int i = 0; i < m_initialPoolSize; ++i)
            {
                AddItem();
            }
        }

        public virtual PooledObject<TComponent> Request()
        {
            if (m_availableComponents.Count == 0)
            {
                AddItem();
            }

            return new PooledObject<TComponent>(m_availableComponents.Pop(), this);
        }

        public virtual void Release(TComponent releasedComponent)
        {
            m_availableComponents.Push(releasedComponent);
        }

        protected virtual void AddItem()
        {
            m_availableComponents.Push(NewItem());
            ++m_poolSize;
        }

        protected virtual TComponent NewItem()
        {
            GameObject newGo = new GameObject($"{nameof(TComponent)}_{m_poolSize}");
            newGo.transform.parent = transform;
            return newGo.AddComponent<TComponent>();
        }
    }
}
