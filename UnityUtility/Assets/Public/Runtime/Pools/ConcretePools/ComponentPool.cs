using System;

using UnityEngine;

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
    public class ComponentPool<TComponent> : ObjectPool<TComponent>
        where TComponent : Component
    {
        [NonSerialized] private readonly Transform m_parent;

        public ComponentPool(int initialPoolSize, Transform componentParent, TComponent prefab) :
            base(initialPoolSize, GetComponentInstancier(componentParent, prefab))
        {
            m_parent = componentParent;
        }

        public ComponentPool(int initialPoolSize, Transform componentParent) :
            this(initialPoolSize, componentParent, null)
        {
        }

        public ComponentPool(ComponentPoolParameters<TComponent> poolParameters) :
            this(poolParameters.InitialPoolSize, poolParameters.PoolParent, poolParameters.Prefab)
        {

        }

        public override void Release(TComponent releasedComponent)
        {
            releasedComponent.gameObject.SetActive(false);
            releasedComponent.transform.SetParent(m_parent);
            base.Release(releasedComponent);
        }

        public override void Dispose()
        {
            m_availableObjects.ForEach(obj => obj.gameObject.Destroy());
            base.Dispose();
        }

        private static PoolObjectConstructor<TComponent> GetComponentInstancier(Transform parent, TComponent prefab)
        {
            TComponent ComponentInstancier(int index)
            {
                if (prefab != null)
                {
                    TComponent newComponent = GameObject.Instantiate(prefab, parent);
                    newComponent.name = newComponent.name.Replace("(Clone)", $"_{index}");
                    newComponent.gameObject.SetActive(false);
                    return newComponent;
                }

                GameObject newGo = new GameObject($"{typeof(TComponent).Name}_{index}");
                newGo.transform.SetParent(parent, false);
                newGo.SetActive(false);
                return newGo.GetOrAddComponent<TComponent>();
            }

            return ComponentInstancier;
        }
    }
}
