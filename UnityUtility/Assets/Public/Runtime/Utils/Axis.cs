
using System.Runtime.CompilerServices;

using UnityEngine;

namespace UnityUtility.Utils
{
    [System.Flags]
    public enum Axis : int
    {
        X = 0x1,
        Y = 0x2,
        Z = 0x4,
    }

    public static class AxisExtensions
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
}