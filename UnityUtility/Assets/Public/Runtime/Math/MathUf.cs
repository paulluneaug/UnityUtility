using System;
using System.Runtime.CompilerServices;

namespace UnityUtility.MathU
{
    public static class MathUf
    {
        private const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

        #region MathF re-implementation
        public const float E = 2.71828175F;
        public const float PI = 3.14159274F;

        [MethodImpl(INLINE)]
        public static float Abs(float x)
        {
            return MathF.Abs(x);
        }

        [MethodImpl(INLINE)]
        public static float Acos(float x)
        {
            return MathF.Acos(x);
        }

        [MethodImpl(INLINE)]
        public static float Acosh(float x)
        {
            return MathF.Acosh(x);
        }

        [MethodImpl(INLINE)]
        public static float Asin(float x)
        {
            return MathF.Asin(x);
        }

        [MethodImpl(INLINE)]
        public static float Asinh(float x)
        {
            return MathF.Asinh(x);
        }

        [MethodImpl(INLINE)]
        public static float Atan(float x)
        {
            return MathF.Atan(x);
        }

        [MethodImpl(INLINE)]
        public static float Atan2(float y, float x)
        {
            return MathF.Atan2(y, x);
        }

        [MethodImpl(INLINE)]
        public static float Atanh(float x)
        {
            return MathF.Atanh(x);
        }

        [MethodImpl(INLINE)]
        public static float Cbrt(float x)
        {
            return MathF.Cbrt(x);
        }

        [MethodImpl(INLINE)]
        public static float Ceiling(float x)
        {
            return MathF.Ceiling(x);
        }

        [MethodImpl(INLINE)]
        public static float Cos(float x)
        {
            return MathF.Cos(x);
        }

        [MethodImpl(INLINE)]
        public static float Cosh(float x)
        {
            return MathF.Cosh(x);
        }

        [MethodImpl(INLINE)]
        public static float Exp(float x)
        {
            return MathF.Exp(x);
        }

        [MethodImpl(INLINE)]
        public static float Floor(float x)
        {
            return MathF.Floor(x);
        }

        [MethodImpl(INLINE)]
        public static float IEEERemainder(float x, float y)
        {
            return MathF.IEEERemainder(x, y);
        }

        [MethodImpl(INLINE)]
        public static float Log(float x)
        {
            return MathF.Log(x);
        }

        [MethodImpl(INLINE)]
        public static float Log(float x, float y)
        {
            return MathF.Log(x, y);
        }

        [MethodImpl(INLINE)]
        public static float Log10(float x)
        {
            return MathF.Log10(x);
        }

        [MethodImpl(INLINE)]
        public static float Max(float x, float y)
        {
            return MathF.Max(x, y);
        }

        [MethodImpl(INLINE)]
        public static float Min(float x, float y)
        {
            return MathF.Min(x, y);
        }

        [MethodImpl(INLINE)]
        public static float Pow(float x, float y)
        {
            return MathF.Pow(x, y);
        }

        [MethodImpl(INLINE)]
        public static float Round(float x, MidpointRounding mode)
        {
            return MathF.Round(x, mode);
        }

        [MethodImpl(INLINE)]
        public static float Round(float x, int digits, MidpointRounding mode)
        {
            return MathF.Round(x, digits, mode);
        }

        [MethodImpl(INLINE)]
        public static float Round(float x)
        {
            return MathF.Round(x);
        }

        [MethodImpl(INLINE)]
        public static float Round(float x, int digits)
        {
            return MathF.Round(x, digits);
        }

        [MethodImpl(INLINE)]
        public static int Sign(float x)
        {
            return MathF.Sign(x);
        }

        [MethodImpl(INLINE)]
        public static float Sin(float x)
        {
            return MathF.Sin(x);
        }

        [MethodImpl(INLINE)]
        public static float Sinh(float x)
        {
            return MathF.Sinh(x);
        }

        [MethodImpl(INLINE)]
        public static float Sqrt(float x)
        {
            return MathF.Sqrt(x);
        }

        [MethodImpl(INLINE)]
        public static float Tan(float x)
        {
            return MathF.Tan(x);
        }

        [MethodImpl(INLINE)]
        public static float Tanh(float x)
        {
            return MathF.Tanh(x);
        }

        [MethodImpl(INLINE)]
        public static float Truncate(float x)
        {
            return MathF.Truncate(x);
        }
        #endregion
    }
}