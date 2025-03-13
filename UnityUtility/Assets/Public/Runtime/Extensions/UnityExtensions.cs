using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityUtility.Extensions
{
    #region Object 
    public static class ObjectExtensions
    {

        /// <summary>
        /// Destroys an <see cref="Object"/>
        /// </summary>
        /// <remarks>
        /// This method supports both editor mode and runtime
        /// </remarks>
        /// <param name="obj"></param>
        public static void Destroy(this Object obj)
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                Object.Destroy(obj);
            }
            else
            {
                Object.DestroyImmediate(obj);
            }
#else
            Object.Destroy(obj);
#endif
        }
    }
    #endregion

    #region GameObject
    public static class GameObjectExtensions
    {
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveComponent<TComp>(this GameObject go) where TComp : Component
        {
            go.GetComponent<TComp>()?.Destroy();
        }

        /// <summary>
        /// Destroys all the Components of type <typeparamref name="TComp"/> that can be found on the given <see cref="GameObject"/>
        /// </summary>
        /// <typeparam name="TComp">Type of the Component to destroy</typeparam>
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
        /// Checks wether the <see cref="GameObject"/> has a component of type <typeparamref name="TComp"/>
        /// </summary>
        /// <typeparam name="TComp">Type of the <see cref="Component"/> to search for</typeparam>
        /// <param name="go">Wether the <see cref="GameObject"/> has a component of type <typeparamref name="TComp"/></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponent<TComp>(this GameObject go) where TComp : Component
        {
            return go.TryGetComponent(out TComp _);
        }
    }
    #endregion

    #region Transform
    public static class TransformeExtensions
    {
        public static Vector3 Left(this Transform transform)
        {
            return -transform.right;
        }

        public static Vector3 Down(this Transform transform)
        {
            return -transform.up;
        }

        public static Vector3 Backward(this Transform transform)
        {
            return -transform.forward;
        }
    }
    #endregion

    #region LayerMask
    public static class LayerMaskExtensions
    {
        /// <summary>
        /// Checks if a <paramref name="layer"/> is in a <see cref="LayerMask"/>
        /// </summary>
        /// <param name="mask"></param>
        /// <param name="layer"></param>
        /// <returns>Wether the <c><paramref name="layer"/></c> is the given <see cref="LayerMask"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
    }
    #endregion

    #region Color
    public static class ColorExtensions
    {
        /// <summary> Copies the given <see cref="Color"/>, sets its red value and returns it </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color WhereR(this Color color, float rValue)
        {
            color.r = rValue;
            return color;
        }

        /// <summary> Copies the given <see cref="Color"/>, sets its green value and returns it </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color WhereG(this Color color, float gValue)
        {
            color.g = gValue;
            return color;
        }

        /// <summary> Copies the given <see cref="Color"/>, sets its blue value and returns it </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color WhereB(this Color color, float bValue)
        {
            color.b = bValue;
            return color;
        }

        /// <summary> Copies the given <see cref="Color"/>, sets its alpha value and returns it </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color WhereA(this Color color, float aValue)
        {
            color.a = aValue;
            return color;
        }
    }
    #endregion
}
