using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngineInternal;

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
        public const float E =  2.718_281_828_459_045f;
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

        public static float PerlinNoise(float x, float y)
        {
            return Mathf.PerlinNoise(x, y);
        }

        public static float PerlinNoise1D(float x)
        {
            return Mathf.PerlinNoise1D(x);
        }

        public static int Abs(int value)
        {
            return Mathf.Abs(value);
        }

        public static float Min(params float[] values);


        public static int Min(int a, int b)
        {
            return MathU.Min(a, b);
        }

        public static int Min(params int[] values);

        public static float Max(params float[] values);
        public static int Max(int a, int b)
        {
            return MathU.Max(a, b);
        }

        public static int Max(params int[] values);

        public static float Ceil(float f)
        {
            return Mathf.Ceil(f);
        }

        public static int CeilToInt(float f)
        {
            return Mathf.CeilToInt(f);
        }

        public static int FloorToInt(float f)
        {
            return Mathf.FloorToInt(f);
        }

        public static int RoundToInt(float f)
        {
            return Mathf.RoundToInt(f);
        }

        public static float Clamp(float value, float min, float max);
        public static int Clamp(int value, int min, int max);
        public static float Clamp01(float value)
        {
            return Mathf.Clamp01(value);
        }

        public static float Lerp(float a, float b, float t);
        public static float LerpClamped(float a, float b, float t);
        public static float LerpAngle(float a, float b, float t);
        public static float MoveTowards(float current, float target, float maxDelta);
        public static float MoveTowardsAngle(float current, float target, float maxDelta);
        public static float SmoothStep(float from, float to, float t);
        public static float Gamma(float value, float absmax, float gamma);
        public static bool Approximately(float a, float b)
        {
            return Mathf.Approximately(a, b);
        }

        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed);

        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime);
        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime);
        public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed);
        public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime);
        public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime);

        public static float Repeat(float t, float length)
        {
            return Mathf.Repeat(t, length);
        }

        public static float PingPong(float t, float length)
        {
            return Mathf.PingPong(t, length);
        }

        public static float InverseLerp(float a, float b, float value);

        public static float DeltaAngle(float current, float target)
        {
            return Mathf.DeltaAngle(current, target);
        }
        #endregion
    }
}