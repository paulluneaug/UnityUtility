using UnityEngine;

using UnityUtility.Attributes;
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
        public ComponentPool(int initialPoolSize, Transform componentParent) :
            base(initialPoolSize, GetComponentInstancier(componentParent, null))
        {
        }
        public ComponentPool(int initialPoolSize, Transform componentParent, TComponent prefab) :
            base(initialPoolSize, GetComponentInstancier(componentParent, prefab))
        {
        }

        public override void Release(TComponent releasedComponent)
        {
            releasedComponent.gameObject.SetActive(false);
            base.Release(releasedComponent);
        }

        private static PoolObjectConstructor<TComponent> GetComponentInstancier(Transform parent, TComponent prefab)
        {
            TComponent ComponentInstancier(int index)
            {
                if (prefab != null)
                {
                    TComponent newComponent = GameObject.Instantiate(prefab);
                    newComponent.name = newComponent.name.Replace("(Clone)", $"_{index}");
                    newComponent.gameObject.SetActive(false);
                    newComponent.transform.SetParent(parent, false);
                    return newComponent;
                }

                GameObject newGo = new GameObject($"{typeof(TComponent).Name}_{index}");
                newGo.transform.parent.SetParent(parent, false);
                newGo.SetActive(false);
                return newGo.GetOrAddComponent<TComponent>();
            }

            return ComponentInstancier;
        }
    }
}
