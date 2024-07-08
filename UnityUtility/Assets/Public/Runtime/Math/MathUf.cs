using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityUtility.MathU
{
    public static class MathUf
    {
        private const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

        public const float INFINITY = float.PositiveInfinity;
        public const float NEGATIVE_INFINITY = float.NegativeInfinity;
        public const float DEG_2_RAD = MathF.PI / 180f;
        public const float RAD_2_DEG = 57.29578f;
        public static readonly float Epsilon = Mathf.Epsilon;


        #region System.MathF re-implementation
        public const float E = 2.718_281_828_459_045f;
        public const float PI = 3.141_592_653_589_793f;
        public const float TAU = 2 * PI;

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

        #region UnityEngine.Mathf re-implementation
        public static int ClosestPowerOfTwo(int value)
        {
            return Mathf.ClosestPowerOfTwo(value);
        }

        public static bool IsPowerOfTwo(int value)
        {
            return Mathf.IsPowerOfTwo(value);
        }

        public static int NextPowerOfTwo(int value)
        {
            return Mathf.NextPowerOfTwo(value);
        }

        public static float GammaToLinearSpace(float value)
        {
            return Mathf.GammaToLinearSpace(value);
        }

        public static float LinearToGammaSpace(float value)
        {
            return Mathf.LinearToGammaSpace(value);
        }

        public static Color CorrelatedColorTemperatureToRGB(float kelvin)
        {
            return Mathf.CorrelatedColorTemperatureToRGB(kelvin);
        }

        public static ushort FloatToHalf(float val)
        {
            return Mathf.FloatToHalf(val);
        }

        public static float HalfToFloat(ushort val)
        {
            return Mathf.HalfToFloat(val);
        }

        public static int Abs(int value)
        {
            return MathU.Abs(value);
        }

        public static float Min(params float[] values)
        {
            float min = int.MaxValue;
            foreach (float val in values)
            {
                min = Min(val, min);
            }
            return min;
        }


        public static int Min(int a, int b)
        {
            return MathU.Min(a, b);
        }

        public static int Min(params int[] values)
        {
            int min = int.MaxValue;
            foreach (int val in values)
            {
                min = Min(val, min);
            }
            return min;
        }

        public static float Max(params float[] values)
        {
            float max = float.MinValue;
            foreach (float val in values)
            {
                max = Max(val, max);
            }
            return max;
        }

        public static int Max(int a, int b)
        {
            return MathU.Max(a, b);
        }

        public static int Max(params int[] values)
        {
            int max = int.MinValue;
            foreach (int val in values)
            {
                max = Max(val, max);
            }
            return max;
        }

        public static float Ceil(float f)
        {
            return MathF.Ceiling(f);
        }

        public static int CeilToInt(float f)
        {
            return (int)MathF.Ceiling(f);
        }

        public static int FloorToInt(float f)
        {
            return (int)MathF.Floor(f);
        }

        public static int RoundToInt(float f)
        {
            return (int)MathF.Round(f);
        }

        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }

            return value;
        }

        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }

            return value;
        }
        public static float Clamp01(float value)
        {
            return Clamp(value, 0.0f, 1.0f);
        }

        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static float LerpClamped(float a, float b, float t)
        {
            return Lerp(a, b, Clamp01(t));
        }
        public static float LerpAngle(float a, float b, float t)
        {
            float num = Repeat(b - a, 360f);
            if (num > 180f)
            {
                num -= 360f;
            }

            return a + num * Clamp01(t);
        }
        public static float MoveTowards(float current, float target, float maxDelta)
        {
            if (Abs(target - current) <= maxDelta)
            {
                return target;
            }

            return current + Sign(target - current) * maxDelta;
        }

        public static float MoveTowardsAngle(float current, float target, float maxDelta)
        {
            float num = DeltaAngle(current, target);
            if (-maxDelta < num && num < maxDelta)
            {
                return target;
            }

            target = current + num;
            return MoveTowards(current, target, maxDelta);
        }

        public static float Gamma(float value, float absmax, float gamma)
        {
            Mathf.Gamma
        }
        public static bool Approximately(float a, float b)
        {
            return Abs(b - a) < Max(1E-06f * Max(Abs(a), Abs(b)), Epsilon * 8f);
        }

        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed);

        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime);
        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime);
        public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed);
        public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime);
        public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime);

        public static float Repeat(float t, float length)
        {
            return Clamp(t - Floor(t / length) * length, 0f, length);
        }

        public static float PingPong(float t, float length)
        {
            t = Repeat(t, length * 2f);
            return length - Abs(t - length);
        }

        public static float InverseLerp(float a, float b, float value);

        public static float DeltaAngle(float current, float target)
        {
            float num = Repeat(target - current, 360f);
            if (num > 180f)
            {
                num -= 360f;
            }

            return num;
        }
        #endregion



        public static float Smoothstep(float x)
        {
            return 3 * x * x - 2 * x * x * x;
        }

        /// <summary>
        /// Compares 2 floats and returns wether their difference is less than a tolerance
        /// </summary>
        /// <param name="val"></param>
        /// <param name="other"></param>
        /// <param name="tolerance"></param>
        /// <returns>Wether the difference between the two floats is less than the given tolerance</returns>
        [MethodImpl(INLINE)]
        public static bool Approximately(this float val, float other, float tolerance = 0.0001f)
        {
            return Math.Abs(val - other) < tolerance;
        }

        /// <summary>
        /// Remaps the value of <paramref name="input"/> 
        /// from the range <paramref name="initialRange"/> to the range <paramref name="targetRange"/>
        /// </summary>
        /// <returns>The remapped value of <paramref name="input"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Remap(this float input, Vector2 initialRange, Vector2 targetRange)
        {
            return input.Remap(initialRange.x, initialRange.y, targetRange.x, targetRange.y);
        }

        /// <summary>
        /// Remaps the value of <paramref name="input"/> 
        /// from the range [<paramref name="initialMin"/>; <paramref name="initialMax"/>] 
        /// to the range [<paramref name="targetMin"/>; <paramref name="targetMax"/>]
        /// </summary>
        /// <returns>The remapped value of <paramref name="input"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Remap(this float input, float initialMin, float initialMax, float targetMin, float targetMax)
        {
            return input.RemapTo01(initialMin, initialMax).RemapFrom01(targetMin, targetMax);
        }

        /// <summary>
        /// Remaps the value of <paramref name="input"/> 
        /// from the range [0; 1] 
        /// to the range [<paramref name="targetMin"/>; <paramref name="targetMax"/>]
        /// </summary>
        /// <returns>The remapped value of <paramref name="input"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RemapFrom01(this float input, float targetMin, float targetMax)
        {
            return targetMin + input * (targetMax - targetMin);
        }

        /// <summary>
        /// Remaps the value of <paramref name="input"/> 
        /// from the range [0; 1] to the range <paramref name="targetRange"/>
        /// </summary>
        /// <returns>The remapped value of <paramref name="input"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RemapFrom01(this float input, Vector2 targetRange)
        {
            return input.RemapFrom01(targetRange.x, targetRange.y);
        }

        /// <summary>
        /// Remaps the value of <paramref name="input"/> 
        /// from the range [<paramref name="initialMin"/>; <paramref name="initialMax"/>]
        /// to the range [0; 1] 
        /// </summary>
        /// <returns>The remapped value of <paramref name="input"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RemapTo01(this float input, float initialMin, float initialMax)
        {
            return (input - initialMin) / (initialMax - initialMin);
        }

        /// <summary>
        /// Remaps the value of <paramref name="input"/> 
        /// from the range <paramref name="initialRange"/> to the range [0; 1]
        /// </summary>
        /// <returns>The remapped value of <paramref name="input"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RemapTo01(this float input, Vector2 initialRange)
        {
            return input.RemapTo01(initialRange.x, initialRange.y);
        }
    }
}