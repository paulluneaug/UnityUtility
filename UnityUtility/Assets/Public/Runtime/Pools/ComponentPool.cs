using System.Collections.Generic;

using UnityEngine;

namespace UnityUtility.Pools
{
    public abstract class ComponentPool<TComponent> : MonoBehaviour, IObjectPool<TComponent> where TComponent : Component
    {
        public int PoolSize => m_poolSize;

        [SerializeField] private int m_initialPoolSize = 10;
        [SerializeField] private bool m_instantiateFromPrefab = false;

        private Stack<TComponent> m_availableComponents = null;
        private int m_poolSize = 0;

        protected virtual void Awake()
        {
            m_availableComponents = new Stack<TComponent>();
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
            releasedComponent.gameObject.SetActive(false);
            m_availableComponents.Push(releasedComponent);
        }

        protected virtual void AddItem()
        {
            m_availableComponents.Push(NewItem());
            ++m_poolSize;
        }

        protected virtual TComponent NewItem()
        {
            GameObject newGo = new GameObject($"{typeof(TComponent).Name}_{m_poolSize}");
            newGo.transform.parent = transform;
            newGo.SetActive(false);
            if (newGo.TryGetComponent(out TComponent foundComponent))
            {
                return foundComponent;
            }
            return newGo.AddComponent<TComponent>();
        }
    }
}
