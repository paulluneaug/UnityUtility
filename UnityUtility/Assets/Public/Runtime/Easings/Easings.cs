using System;
using System.Runtime.CompilerServices;

using UnityUtility.MathU;

namespace UnityUtility.Easings
{
    public static class Easings
    {
        public enum EasingFunction
        {
            Linear,
            Smoothstep,
            Smootherstep,
            EaseInSine,
            EaseOutSine,
            EaseInOutSine,
            EaseInQuad,
            EaseOutQuad,
            EaseInOutQuad,
            EaseInCubic,
            EaseOutCubic,
            EaseInOutCubic,
            EaseInQuart,
            EaseOutQuart,
            EaseInOutQuart,
            EaseInQuint,
            EaseOutQuint,
            EaseInOutQuint,
            EaseInExpo,
            EaseOutExpo,
            EaseInOutExpo,
            EaseInCirc,
            EaseOutCirc,
            EaseInOutCirc,
        }

        public static float Ease(float x, EasingFunction function)
        {
            return function switch
            {
                EasingFunction.Linear => MathUf.Clamp01(x),
                EasingFunction.Smoothstep => Smoothstep(x),
                EasingFunction.Smootherstep => Smootherstep(x),
                EasingFunction.EaseInSine => EaseInSine(x),
                EasingFunction.EaseOutSine => EaseOutSine(x),
                EasingFunction.EaseInOutSine => EaseInOutSine(x),
                EasingFunction.EaseInQuad => EaseInQuad(x),
                EasingFunction.EaseOutQuad => EaseOutQuad(x),
                EasingFunction.EaseInOutQuad => EaseInOutQuad(x),
                EasingFunction.EaseInCubic => EaseInCubic(x),
                EasingFunction.EaseOutCubic => EaseOutCubic(x),
                EasingFunction.EaseInOutCubic => EaseInOutCubic(x),
                EasingFunction.EaseInQuart => EaseInQuart(x),
                EasingFunction.EaseOutQuart => EaseOutQuart(x),
                EasingFunction.EaseInOutQuart => EaseInOutQuart(x),
                EasingFunction.EaseInQuint => EaseInQuint(x),
                EasingFunction.EaseOutQuint => EaseOutQuint(x),
                EasingFunction.EaseInOutQuint => EaseInOutQuint(x),
                EasingFunction.EaseInExpo => EaseInExpo(x),
                EasingFunction.EaseOutExpo => EaseOutExpo(x),
                EasingFunction.EaseInOutExpo => EaseInOutExpo(x),
                EasingFunction.EaseInCirc => EaseInCirc(x),
                EasingFunction.EaseOutCirc => EaseOutCirc(x),
                EasingFunction.EaseInOutCirc => EaseInOutCirc(x),
                _ => throw new NotImplementedException(),
            };
        }

        #region Smoothstep

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Smoothstep(float x)
        {
            x = MathUf.Clamp01(x);
            return 3 * x * x - 2 * x * x * x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Smootherstep(float x)
        {
            x = MathUf.Clamp01(x);
            return x * x * x * (x * (6.0f * x - 15.0f) + 10.0f);
        }

        /// <summary>
        /// A generalized version of the smoothstep function for higher orders
        /// </summary>
        /// <remarks>
        /// Remark : 
        /// For <paramref name="N"/> = 1 and <paramref name="N"/> = 2, 
        /// prefer using <see cref="Smoothstep(float)"/> and <see cref="Smootherstep(float)"/> respectivly
        /// as they are way faster
        /// </remarks>
        public static float GeneralSmoothstep(int N, float x)
        {
            x = MathUf.Clamp01(x);
            float result = 0.0f;
            for (int n = 0; n <= N; ++n)
            {
                result += MathUf.BinomialCoefficient(-N - 1, n) *
                          MathUf.BinomialCoefficient(2 * N + 1, N - n) *
                          MathUf.Pow(x, N + n + 1);
            }

            return result;
        }
        #endregion

        #region Sine
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInSine(float x)
        {
            x = MathUf.Clamp01(x);
            return 1 - MathUf.Cos((x * MathUf.PI) / 2.0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseOutSine(float x)
        {
            x = MathUf.Clamp01(x);
            return MathUf.Sin((x * MathUf.PI) / 2.0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInOutSine(float x)
        {
            x = MathUf.Clamp01(x);
            return -(MathUf.Cos(MathUf.PI * x) - 1.0f) / 2.0f;
        }
        #endregion

        #region Quad
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInQuad(float x)
        {
            x = MathUf.Clamp01(x);
            return x * x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseOutQuad(float x)
        {
            x = MathUf.Clamp01(x);
            float val0 = 1.0f - x;
            return 1.0f - val0 * val0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInOutQuad(float x)
        {
            x = MathUf.Clamp01(x);
            float val0 = -2.0f * x + 2.0f;
            return x < 0.5f ?
                2.0f * x * x :
                1.0f - val0 * val0 / 2.0f;
        }
        #endregion

        #region Cubic
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInCubic(float x)
        {
            x = MathUf.Clamp01(x);
            return x * x * x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseOutCubic(float x)
        {
            x = MathUf.Clamp01(x);
            float val0 = 1.0f - x;
            return 1.0f - val0 * val0 * val0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInOutCubic(float x)
        {
            x = MathUf.Clamp01(x);
            float val0 = -2.0f * x + 2.0f;
            return x < 0.5f ?
                4.0f * x * x * x :
                1.0f - val0 * val0 * val0 / 2.0f;
        }
        #endregion

        #region Quart
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInQuart(float x)
        {
            x = MathUf.Clamp01(x);
            return x * x * x * x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseOutQuart(float x)
        {
            x = MathUf.Clamp01(x);
            float val0 = 1.0f - x;
            return 1.0f - val0 * val0 * val0 * val0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInOutQuart(float x)
        {
            x = MathUf.Clamp01(x);
            float val0 = -2.0f * x + 2.0f;
            return x < 0.5f ?
                8.0f * x * x * x * x :
                1.0f - val0 * val0 * val0 * val0 / 2.0f;
        }
        #endregion

        #region Quint
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInQuint(float x)
        {
            x = MathUf.Clamp01(x);
            return x * x * x * x * x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseOutQuint(float x)
        {
            x = MathUf.Clamp01(x);
            float val0 = 1.0f - x;
            return 1.0f - val0 * val0 * val0 * val0 * val0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInOutQuint(float x)
        {
            x = MathUf.Clamp01(x);
            float val0 = -2.0f * x + 2.0f;
            return x < 0.5 ?
                16 * x * x * x * x * x :
                1.0f - val0 * val0 * val0 * val0 * val0 / 2.0f;
        }
        #endregion

        #region Expo
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInExpo(float x)
        {
            x = MathUf.Clamp01(x);
            return x == 0.0f ? 0.0f : MathUf.Pow(2.0f, 10.0f * x - 10.0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseOutExpo(float x)
        {
            x = MathUf.Clamp01(x);
            return x == 1.0f ? 1.0f : 1.0f - MathUf.Pow(2, -10.0f * x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInOutExpo(float x)
        {
            x = MathUf.Clamp01(x);
            return
                x == 0.0f ? 0.0f :
                x == 1.0f ? 1.0f :
                x < 0.5f ? MathUf.Pow(2.0f, 20.0f * x - 10.0f) / 2.0f :
                (2.0f - MathUf.Pow(2.0f, -20.0f * x + 10)) / 2.0f;
        }
        #endregion

        #region Circ
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInCirc(float x)
        {
            x = MathUf.Clamp01(x);
            return 1 - MathUf.Sqrt(1 - x * x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseOutCirc(float x)
        {
            x = MathUf.Clamp01(x);
            float val0 = x - 1;
            return MathUf.Sqrt(1 - val0 * val0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInOutCirc(float x)
        {
            x = MathUf.Clamp01(x);
            float val0 = 2.0f * x;
            float val1 = -2.0f * x + 2.0f;
            return x < 0.5 ?
                (1 - MathUf.Sqrt(1 - val0 * val0)) / 2.0f :
                (MathUf.Sqrt(1 - val1 * val1) + 1) / 2.0f;
        }

        #endregion
    }
}
