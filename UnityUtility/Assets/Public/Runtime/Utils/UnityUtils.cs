using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

using Object = UnityEngine.Object;

namespace UnityUtility.Utils
{
    #region Axis
    [System.Flags]
    public enum Axis : int
    {
        X = 0x1, 
        Y = 0x2, 
        Z = 0x4,
    }

    public static class AxisUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ToVector(this Axis axis)
        {
            return new Vector3(
                (int)(axis & Axis.X) / (int)Axis.X,
                (int)(axis & Axis.Y) / (int)Axis.Y,
                (int)(axis & Axis.Z) / (int)Axis.Z);
        }
    }
    #endregion

    #region Object 
    public static class ObjectUtils
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
    public static class GameObjectUtils
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
    }
    #endregion

    #region LayerMask
    public static class LayerMaskUtils
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
    public static class ColorUtils
    {
        /// <summary>
        /// Parse a string to a color 
        /// </summary>
        /// <remarks>
        /// Supported formats : <br/>
        /// - "(r, g, b)" where r, g and b belongs to [0; 255]<br/>
        /// - "(r, g, b, a)" where r, g, b and a belongs to [0; 255]<br/>
        /// - "#rgb" where r, g and b are hexadecimal numbers of 2 characters each<br/>
        /// <br/>
        /// Note : The whitespaces are ignored
        /// </remarks>
        /// <param name="colorString"></param>
        /// <param name="color"></param>
        /// <returns>Wether the conversion was a success</returns>
        public static bool TryParseColor(string colorString, out Color color)
        {
            color = Color.black;
            if (string.IsNullOrEmpty(colorString))
            {
                return false;
            }

            colorString = colorString.Replace(" ", "");
            if (colorString.StartsWith('(') && colorString.EndsWith(')')) // For the formats (r, g, b) and (r, g, b, a)
            {
                string colorValuesString = colorString.Substring(1, colorString.Length - 2);
                List<(bool parseSuceeded, int parsedValue)> parsedValues = colorValuesString.Split(',').Select((string s) => (int.TryParse(s, out int parsedValue), parsedValue)).ToList();
                if (parsedValues.All((result) => result.parseSuceeded))
                {
                    int parsedValueCount = parsedValues.Count;
                    if (parsedValueCount == 3)
                    {
                        color = new Color(
                            parsedValues[0].parsedValue / 255.0f,
                            parsedValues[1].parsedValue / 255.0f,
                            parsedValues[2].parsedValue / 255.0f);
                        return true;
                    }
                    if (parsedValueCount == 4)
                    {
                        color = new Color(
                            parsedValues[0].parsedValue / 255.0f,
                            parsedValues[1].parsedValue / 255.0f,
                            parsedValues[2].parsedValue / 255.0f,
                            parsedValues[3].parsedValue / 255.0f);
                        return true;
                    }
                }
            }
            else if (colorString.StartsWith('#')) // For the format #rgb
            {
                colorString = colorString[1..];
                if (colorString.Length == 6)
                {
                    (bool isHex, int intValue)[] hexParseResults =
                        colorString.SplitBySize(2)
                        .Select((string hexSt) => (MathUtils.TryHexToInt(hexSt, out int intValue), intValue))
                        .ToArray();

                    if (hexParseResults.All(result => result.isHex))
                    {
                        color = new Color(
                            hexParseResults[0].intValue / 255.0f,
                            hexParseResults[1].intValue / 255.0f,
                            hexParseResults[2].intValue / 255.0f);
                        return true;
                    }
                }
            }
            return false;
        }
    }
    #endregion
}
