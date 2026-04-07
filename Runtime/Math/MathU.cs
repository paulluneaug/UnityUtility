using System;
using System.Runtime.CompilerServices;

namespace UnityUtility.MathU
{
    public static class MathU
    {
        private const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

        #region System.Math re-implementation
        public const double E = 2.7182818284590451;
        public const double PI = 3.1415926535897931;
        public const double TAU = 2 * PI;

        [MethodImpl(INLINE)]
        public static decimal Abs(decimal value)
        {
            return Math.Abs(value);
        }

        [MethodImpl(INLINE)]
        public static double Abs(double value)
        {
            return Math.Abs(value);
        }

        [MethodImpl(INLINE)]
        public static short Abs(short value)
        {
            return Math.Abs(value);
        }

        [MethodImpl(INLINE)]
        public static int Abs(int value)
        {
            return Math.Abs(value);
        }

        [MethodImpl(INLINE)]
        public static long Abs(long value)
        {
            return Math.Abs(value);
        }

        [MethodImpl(INLINE)]
        public static sbyte Abs(sbyte value)
        {
            return Math.Abs(value);
        }

        [MethodImpl(INLINE)]
        public static float Abs(float value)
        {
            return Math.Abs(value);
        }

        [MethodImpl(INLINE)]
        public static double Acos(double d)
        {
            return Math.Acos(d);
        }

        [MethodImpl(INLINE)]
        public static double Acosh(double d)
        {
            return Math.Acosh(d);
        }

        [MethodImpl(INLINE)]
        public static double Asin(double d)
        {
            return Math.Asin(d);
        }

        [MethodImpl(INLINE)]
        public static double Asinh(double d)
        {
            return Math.Asinh(d);
        }

        [MethodImpl(INLINE)]
        public static double Atan(double d)
        {
            return Math.Atan(d);
        }

        [MethodImpl(INLINE)]
        public static double Atan2(double y, double x)
        {
            return Math.Atan2(y, x);
        }

        [MethodImpl(INLINE)]
        public static double Atanh(double d)
        {
            return Math.Atanh(d);
        }

        [MethodImpl(INLINE)]
        public static long BigMul(int a, int b)
        {
            return Math.BigMul(a, b);
        }

        [MethodImpl(INLINE)]
        public static double Cbrt(double d)
        {
            return Math.Cbrt(d);
        }

        [MethodImpl(INLINE)]
        public static decimal Ceiling(decimal d)
        {
            return Math.Ceiling(d);
        }

        [MethodImpl(INLINE)]
        public static double Ceiling(double a)
        {
            return Math.Ceiling(a);
        }

        [MethodImpl(INLINE)]
        public static ulong Clamp(ulong value, ulong min, ulong max)
        {
            return Math.Clamp(value, min, max);
        }

        [MethodImpl(INLINE)]
        public static uint Clamp(uint value, uint min, uint max)
        {
            return Math.Clamp(value, min, max);
        }

        [MethodImpl(INLINE)]
        public static ushort Clamp(ushort value, ushort min, ushort max)
        {
            return Math.Clamp(value, min, max);
        }

        [MethodImpl(INLINE)]
        public static float Clamp(float value, float min, float max)
        {
            return Math.Clamp(value, min, max);
        }

        [MethodImpl(INLINE)]
        public static sbyte Clamp(sbyte value, sbyte min, sbyte max)
        {
            return Math.Clamp(value, min, max);
        }

        [MethodImpl(INLINE)]
        public static short Clamp(short value, short min, short max)
        {
            return Math.Clamp(value, min, max);
        }

        [MethodImpl(INLINE)]
        public static int Clamp(int value, int min, int max)
        {
            return Math.Clamp(value, min, max);
        }

        [MethodImpl(INLINE)]
        public static double Clamp(double value, double min, double max)
        {
            return Math.Clamp(value, min, max);
        }

        [MethodImpl(INLINE)]
        public static decimal Clamp(decimal value, decimal min, decimal max)
        {
            return Math.Clamp(value, min, max);
        }

        [MethodImpl(INLINE)]
        public static byte Clamp(byte value, byte min, byte max)
        {
            return Math.Clamp(value, min, max);
        }

        [MethodImpl(INLINE)]
        public static long Clamp(long value, long min, long max)
        {
            return Math.Clamp(value, min, max);
        }

        [MethodImpl(INLINE)]
        public static double Cos(double d)
        {
            return Math.Cos(d);
        }

        [MethodImpl(INLINE)]
        public static double Cosh(double value)
        {
            return Math.Cosh(value);
        }

        [MethodImpl(INLINE)]
        public static int DivRem(int a, int b, out int result)
        {
            return Math.DivRem(a, b, out result);
        }

        [MethodImpl(INLINE)]
        public static long DivRem(long a, long b, out long result)
        {
            return Math.DivRem(a, b, out result);
        }

        [MethodImpl(INLINE)]
        public static double Exp(double d)
        {
            return Math.Exp(d);
        }

        [MethodImpl(INLINE)]
        public static double Floor(double d)
        {
            return Math.Floor(d);
        }

        [MethodImpl(INLINE)]
        public static decimal Floor(decimal d)
        {
            return Math.Floor(d);
        }

        [MethodImpl(INLINE)]
        public static double IEEERemainder(double x, double y)
        {
            return Math.IEEERemainder(x, y);
        }

        [MethodImpl(INLINE)]
        public static double Log(double d)
        {
            return Math.Log(d);
        }

        [MethodImpl(INLINE)]
        public static double Log(double a, double newBase)
        {
            return Math.Log(a, newBase);
        }

        [MethodImpl(INLINE)]
        public static double Log10(double d)
        {
            return Math.Log10(d);
        }

        [MethodImpl(INLINE)]
        public static ulong Max(ulong val1, ulong val2)
        {
            return Math.Max(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static uint Max(uint val1, uint val2)
        {
            return Math.Max(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static ushort Max(ushort val1, ushort val2)
        {
            return Math.Max(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static float Max(float val1, float val2)
        {
            return Math.Max(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static sbyte Max(sbyte val1, sbyte val2)
        {
            return Math.Max(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static decimal Max(decimal val1, decimal val2)
        {
            return Math.Max(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static int Max(int val1, int val2)
        {
            return Math.Max(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static short Max(short val1, short val2)
        {
            return Math.Max(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static double Max(double val1, double val2)
        {
            return Math.Max(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static byte Max(byte val1, byte val2)
        {
            return Math.Max(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static long Max(long val1, long val2)
        {
            return Math.Max(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static ulong Min(ulong val1, ulong val2)
        {
            return Math.Min(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static uint Min(uint val1, uint val2)
        {
            return Math.Min(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static ushort Min(ushort val1, ushort val2)
        {
            return Math.Min(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static float Min(float val1, float val2)
        {
            return Math.Min(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static sbyte Min(sbyte val1, sbyte val2)
        {
            return Math.Min(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static long Min(long val1, long val2)
        {
            return Math.Min(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static short Min(short val1, short val2)
        {
            return Math.Min(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static double Min(double val1, double val2)
        {
            return Math.Min(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static decimal Min(decimal val1, decimal val2)
        {
            return Math.Min(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static byte Min(byte val1, byte val2)
        {
            return Math.Min(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static int Min(int val1, int val2)
        {
            return Math.Min(val1, val2);
        }

        [MethodImpl(INLINE)]
        public static double Pow(double x, double y)
        {
            return Math.Pow(x, y);
        }

        [MethodImpl(INLINE)]
        public static double Round(double value, MidpointRounding mode)
        {
            return Math.Round(value, mode);
        }

        [MethodImpl(INLINE)]
        public static double Round(double value, int digits, MidpointRounding mode)
        {
            return Math.Round(value, digits, mode);
        }

        [MethodImpl(INLINE)]
        public static double Round(double value, int digits)
        {
            return Math.Round(value, digits);
        }

        [MethodImpl(INLINE)]
        public static double Round(double a)
        {
            return Math.Round(a);
        }

        [MethodImpl(INLINE)]
        public static decimal Round(decimal d, int decimals, MidpointRounding mode)
        {
            return Math.Round(d, decimals, mode);
        }

        [MethodImpl(INLINE)]
        public static decimal Round(decimal d, int decimals)
        {
            return Math.Round(d, decimals);
        }

        [MethodImpl(INLINE)]
        public static decimal Round(decimal d)
        {
            return Math.Round(d);
        }

        [MethodImpl(INLINE)]
        public static decimal Round(decimal d, MidpointRounding mode)
        {
            return Math.Round(d, mode);
        }

        [MethodImpl(INLINE)]
        public static int Sign(float value)
        {
            return Math.Sign(value);
        }

        [MethodImpl(INLINE)]
        public static int Sign(long value)
        {
            return Math.Sign(value);
        }

        [MethodImpl(INLINE)]
        public static int Sign(int value)
        {
            return Math.Sign(value);
        }

        [MethodImpl(INLINE)]
        public static int Sign(sbyte value)
        {
            return Math.Sign(value);
        }

        [MethodImpl(INLINE)]
        public static int Sign(double value)
        {
            return Math.Sign(value);
        }

        [MethodImpl(INLINE)]
        public static int Sign(decimal value)
        {
            return Math.Sign(value);
        }

        [MethodImpl(INLINE)]
        public static int Sign(short value)
        {
            return Math.Sign(value);
        }

        [MethodImpl(INLINE)]
        public static double Sin(double a)
        {
            return Math.Sin(a);
        }

        [MethodImpl(INLINE)]
        public static double Sinh(double value)
        {
            return Math.Sinh(value);
        }

        [MethodImpl(INLINE)]
        public static double Sqrt(double d)
        {
            return Math.Sqrt(d);
        }

        [MethodImpl(INLINE)]
        public static double Tan(double a)
        {
            return Math.Tan(a);
        }

        [MethodImpl(INLINE)]
        public static double Tanh(double value)
        {
            return Math.Tanh(value);
        }

        [MethodImpl(INLINE)]
        public static decimal Truncate(decimal d)
        {
            return Math.Truncate(d);
        }

        [MethodImpl(INLINE)]
        public static double Truncate(double d)
        {
            return Math.Truncate(d);
        }
        #endregion
    }
}
