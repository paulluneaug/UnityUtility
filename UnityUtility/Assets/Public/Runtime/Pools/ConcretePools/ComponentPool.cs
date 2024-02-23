using System.Collections.Generic;
using UnityEngine;
using UnityUtility.CustomAttributes;

namespace UnityUtility.Pools
{
    /// <summary>
    /// A object pool for <see cref="Component"/>
    /// 
    /// <para>
    /// See also :
    /// <br><seealso cref="ObjectPool{T}"/></br>
    /// <br><seealso cref="CallbackRecieverObjectPool{T}"/></br>
    /// <br><seealso cref="CallbackRecieverComponentPool{TComponent}"/></br>
    /// </para>
    /// </summary>
    /// <typeparam name="TComponent">Pooled component type</typeparam>
    public abstract class ComponentPool<TComponent> : MonoBehaviour, IObjectPool<TComponent> 
        where TComponent : Component
    {
        public int PoolSize => m_poolSize;

        [SerializeField] private int m_initialPoolSize = 10;
        [SerializeField] protected bool m_instantiateFromPrefab = false;
        [SerializeField, ShowIf(nameof(m_instantiateFromPrefab))] private TComponent m_componentPrefab = null;

        private Stack<TComponent> m_availableComponents = null;
        private int m_poolSize = 0;

        protected virtual void Awake()
        {
            m_availableComponents = new Stack<TComponent>(m_initialPoolSize);
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
            if (m_instantiateFromPrefab)
            {
                TComponent newComponent = Instantiate(m_componentPrefab);
                newComponent.name = newComponent.name.Replace("(Clone)", $"_{m_poolSize}");
                newComponent.gameObject.SetActive(false);
                newComponent.transform.parent = transform;
                return newComponent;
            }
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
