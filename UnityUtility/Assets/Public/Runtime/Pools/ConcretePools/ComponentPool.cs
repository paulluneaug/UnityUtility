using System;
using System.Collections.Generic;

using UnityEngine;

using UnityUtility.CustomAttributes;
using UnityUtility.Extensions;

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

        public int ElementsInPool => m_availableComponents.Count;

#if UNITY_EDITOR
        public GameObject Prefab => m_componentPrefab;
#endif

        public event Action OnObjectRequested;
        public event Action OnObjectReleased;

        [SerializeField] private int m_initialPoolSize = 10;
        [SerializeField] protected bool m_instantiateFromPrefab = false;
        [SerializeField, ShowIf(nameof(m_instantiateFromPrefab))] private GameObject m_componentPrefab = null;

        private Stack<TComponent> m_availableComponents = null;
        private int m_poolSize = 0;


        protected virtual void Awake()
        {
            if (m_instantiateFromPrefab && !m_componentPrefab.HasComponent<TComponent>())
            {
                Debug.LogError($"The given GameObject does not have a component of type {typeof(TComponent).Name} on its root. The pool will not work");
                return;
            }

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

            PooledObject<TComponent> requestedComponent = new PooledObject<TComponent>(m_availableComponents.Pop(), this);

            OnObjectRequested?.Invoke();

            return requestedComponent;
        }

        public virtual void Release(TComponent releasedComponent)
        {
            releasedComponent.gameObject.SetActive(false);
            m_availableComponents.Push(releasedComponent);

            OnObjectReleased?.Invoke();
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
                TComponent newComponent = Instantiate(m_componentPrefab).GetComponent<TComponent>();
                newComponent.name = newComponent.name.Replace("(Clone)", $"_{m_poolSize}");
                newComponent.gameObject.SetActive(false);
                newComponent.transform.parent = transform;
                return newComponent;
            }
            GameObject newGo = new GameObject($"{typeof(TComponent).Name}_{m_poolSize}");
            newGo.transform.parent = transform;
            newGo.SetActive(false);
            return newGo.GetOrAddComponent<TComponent>();
        }
    }
}
