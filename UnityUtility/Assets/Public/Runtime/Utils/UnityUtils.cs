using System;
using UnityEngine;

namespace UnityUtility.Utils
{
    public static class UnityUtils
    {
        [Flags]
        public enum Axis : int
        {
            X = 0x1, 
            Y = 0x2, 
            Z = 0x4,
        }

        #region Components
        /// <summary>
        /// Tries to get <see cref="Component"/> on the given <see cref="GameObject"/> and if none are found, one is added and returned
        /// </summary>
        /// <typeparam name="TComp">Type of the expected <see cref="Component"/> </typeparam>
        /// <param name="go"><see cref="GameObject"/> on which to search for the <see cref="Component"/></param>
        /// <returns></returns>
        public static TComp GetOrAddComponent<TComp>(this GameObject go) where TComp : Component
        {
            if (go.TryGetComponent(out TComp comp))
            {
                return comp;
            }
            return go.AddComponent<TComp>();
        }

        /// <summary>
        /// Destroys the first <see cref="Component"/> of type <typeparamref name="TComp"/> that can be found on the given <see cref="GameObject"/>
        /// </summary>
        /// <typeparam name="TComp">Type of the <see cref="Component"/> to destroy</typeparam>
        /// <param name="go"></param>
        public static void RemoveComponent<TComp>(this GameObject go) where TComp : Component
        {
            go.GetComponent<TComp>()?.Destroy();
        }

        /// <summary>
        /// Destroys all the <see cref="Component"/>s of type <typeparamref name="TComp"/> that can be found on the given <see cref="GameObject"/>
        /// </summary>
        /// <typeparam name="TComp">Type of the <see cref="Component"/>s to destroy</typeparam>
        /// <param name="go"></param>
        public static void RemoveComponents<TComp>(this GameObject go) where TComp : Component
        {
            TComp[] components = go.GetComponents<TComp>();
            foreach (TComp component in components)
            {
                component.Destroy();
            }
        }

        /// <summary>
        /// Destroys an <see cref="Object"/>
        /// </summary>
        /// <remarks>
        /// If the method is called at runtime 
        /// <seealso cref="Object.Destroy"/> will be called and 
        /// <see cref="Object.DestroyImmediate"/> otherwise</remarks>
        /// <param name="obj"></param>
        public static void Destroy(this Object obj)
        {
#if UNITY_EDITOR
            Object.DestroyImmediate(obj);
#else
            Object.Destroy(comp);
#endif
        }
        #endregion

        /// <summary>
        /// Checks if a <paramref name="layer"/> is in a <see cref="LayerMask"/>
        /// </summary>
        /// <param name="mask"></param>
        /// <param name="layer"></param>
        /// <returns>Wether the <c><paramref name="layer"/></c> is the given <see cref="LayerMask"/></returns>
        public static bool Contains(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }

        public static bool HasFlag(this int val, int flag)
        {
            return (val & flag) == flag;
        }
    }
}
