using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityUtility.Utils
{
    #region Comparisons
    public static class ComparisonUtils
    {
        /// <summary>
        /// The different comparison operators
        /// </summary>
        /// 
        /// <remarks>
        /// Values : <br/>
        /// <see cref="Inferior"/> = 0 <br/>
        /// <see cref="InferiorEqual"/> = 1 <br/>
        /// <see cref="Equal"/> = 2 <br/>
        /// <see cref="Superior"/> = 3 <br/>
        /// <see cref="SuperiorEqual"/> = 4 <br/>
        /// <see cref="NotEquals"/> = 5 <br/>
        /// </remarks>
        public enum ComparisonOperator
        {
            Inferior = 0,
            InferiorEqual = 1,
            Equal = 2,
            Superior = 3,
            SuperiorEqual = 4,
            NotEquals = 5,
        };

        /// <summary>
        /// Returns wether <paramref name="value1"/> and <paramref name="value2"/> 
        /// resolves the <see cref="ComparisonOperator"/> <paramref name="op"/>
        /// </summary>
        /// 
        /// <typeparam name="T">Type of the operandes</typeparam>
        /// <param name="value1">The first value to compare</param>
        /// <param name="op">Operator</param>
        /// <param name="value2">The second value to compare</param>
        /// <returns>Wether <paramref name="value1"/> and <paramref name="value2"/> resolves the operator <paramref name="op"/></returns>
        public static bool ResolveComparison<T>(T value1, ComparisonOperator op, T value2)
            where T : IComparable<T>
        {
            return op switch
            {
                ComparisonOperator.Inferior => value1.SmallerThan(value2),
                ComparisonOperator.InferiorEqual => value1.SmallerOrEqualsTo(value2),
                ComparisonOperator.Equal => value1.EqualsTo(value2),
                ComparisonOperator.Superior => value1.GreaterThan(value2),
                ComparisonOperator.SuperiorEqual => value1.GreaterOrEqualsTo(value2),
                ComparisonOperator.NotEquals => value1.NotEqualsTo(value2),
                _ => false,
            }; ;
        }

        /// <summary>
        /// Compares <paramref name="value"/>, <paramref name="min"/> and <paramref name="max"/>
        /// and returns wether <paramref name="value"/> belongs in the interval [<paramref name="min"/> ; <paramref name="max"/>]
        /// </summary>
        /// 
        /// <typeparam name="T">Type of the operandes</typeparam>
        /// <param name="value">Value to compare</param>
        /// <param name="min">Lower bound</param>
        /// <param name="max">Higher bound</param>
        /// <param name="inclusive">Wether the bounds belongs to the interval</param>
        /// <returns>Wether <paramref name="value"/> belongs in the interval [<paramref name="min"/> ; <paramref name="max"/>]</returns>
        public static bool Between<T>(this T value, T min, T max, bool inclusive = true)
            where T : IComparable<T>
        {
            return inclusive ?
                value.GreaterOrEqualsTo(min) && value.SmallerOrEqualsTo(max) :
                value.GreaterThan(min) && value.SmallerThan(max);
        }

        /// <summary>
        /// Compares <paramref name="value"/> and <paramref name="threshold"/> 
        /// and returns wether <c><paramref name="value"/> <![CDATA[>]]> <paramref name="threshold"/></c>
        /// </summary>
        /// 
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TComp"></typeparam>
        /// <param name="value">Value to compare</param>
        /// <param name="threshold">Value to compare with</param>
        /// <returns>Wether <c><paramref name="value"/> <![CDATA[>]]> <paramref name="threshold"/></c></returns>
        public static bool GreaterThan<TVal, TComp>(this TVal value, TComp threshold)
            where TComp : IComparable<TVal>
        {
            return threshold.CompareTo(value) < 0;
        }

        /// <summary>
        /// Compares <paramref name="value"/> and <paramref name="threshold"/> 
        /// and returns wether <c><paramref name="value"/> <![CDATA[>=]]> <paramref name="threshold"/></c>
        /// </summary>
        /// 
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TComp"></typeparam>
        /// <param name="value">Value to compare</param>
        /// <param name="threshold">Value to compare with</param>
        /// <returns>Wether <c><paramref name="value"/> <![CDATA[>=]]> <paramref name="threshold"/></c></returns>
        public static bool GreaterOrEqualsTo<TVal, TComp>(this TVal value, TComp threshold)
            where TComp : IComparable<TVal>
        {
            return threshold.CompareTo(value) <= 0;
        }

        /// <summary>
        /// Compares <paramref name="value"/> and <paramref name="threshold"/> 
        /// and returns wether <c><paramref name="value"/> <![CDATA[<]]> <paramref name="threshold"/></c>
        /// </summary>
        /// 
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TComp"></typeparam>
        /// <param name="value">Value to compare</param>
        /// <param name="threshold">Value to compare with</param>
        /// <returns>Wether <c><paramref name="value"/> <![CDATA[<]]> <paramref name="threshold"/></c></returns>
        public static bool SmallerThan<TVal, TComp>(this TVal value, TComp threshold)
            where TComp : IComparable<TVal>
        {
            return threshold.CompareTo(value) > 0;
        }

        /// <summary>
        /// Compares <paramref name="value"/> and <paramref name="threshold"/> 
        /// and returns wether <c><paramref name="value"/> <![CDATA[<=]]> <paramref name="threshold"/></c>
        /// </summary>
        /// 
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TComp"></typeparam>
        /// <param name="value">Value to compare</param>
        /// <param name="threshold">Value to compare with</param>
        /// <returns>Wether <c><paramref name="value"/> <![CDATA[<=]]> <paramref name="threshold"/></c></returns>
        public static bool SmallerOrEqualsTo<TVal, TComp>(this TVal value, TComp threshold)
            where TComp : IComparable<TVal>
        {
            return threshold.CompareTo(value) >= 0;
        }

        /// <summary>
        /// Compares <paramref name="value"/> and <paramref name="other"/> 
        /// and returns wether <c><paramref name="value"/> <![CDATA[==]]> <paramref name="other"/></c>
        /// </summary>
        /// 
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TComp"></typeparam>
        /// <param name="value">Value to compare</param>
        /// <param name="other">Value to compare with</param>
        /// <returns>Wether <c><paramref name="value"/> <![CDATA[==]]> <paramref name="other"/></c></returns>
        public static bool EqualsTo<TVal, TComp>(this TVal value, TComp other)
            where TComp : IComparable<TVal>
        {
            return other.CompareTo(value) == 0;
        }

        /// <summary>
        /// Compares <paramref name="value"/> and <paramref name="other"/> 
        /// and returns wether <c><paramref name="value"/> <![CDATA[!=]]> <paramref name="other"/></c>
        /// </summary>
        /// 
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TComp"></typeparam>
        /// <param name="value">Value to compare</param>
        /// <param name="other">Value to compare with</param>
        /// <returns>Wether <c><paramref name="value"/> <![CDATA[!=]]> <paramref name="other"/></c></returns>
        public static bool NotEqualsTo<TVal, TComp>(this TVal value, TComp other)
            where TComp : IComparable<TVal>
        {
            return other.CompareTo(value) != 0;
        }
    }
    #endregion

    #region Vector2
    public static class Vector2Utils
    {
        /// <summary>
        /// Projects a vector onto another vector 
        /// </summary>
        /// <param name="vector">The projected vector</param>
        /// <param name="target">The vector on which <paramref name="vector"/> is projected</param>
        /// <returns><paramref name="vector"/> projected onto <paramref name="target"/></returns>
        public static Vector2 ProjectOn(this Vector2 vector, Vector2 target)
        {
            float targetDot = Vector2.Dot(target, target);
            if (targetDot < Mathf.Epsilon)
            {
                return Vector2.zero;
            }
            return target * (Vector2.Dot(vector, target) / targetDot);
        }

        /// <summary>
        /// Compares the position of two points and returns wether their difference is less than a tolerance
        /// </summary>
        /// <returns>Wether the difference in position between the two points is less than the given tolerance</returns>
        public static bool ApproximatelyEqualsPoint(this Vector2 point, Vector2 other, Vector2 tolerance)
        {
            return
                Math.Abs(point.x - other.x) < tolerance.x &&
                Math.Abs(point.y - other.y) < tolerance.y;
        }

        /// <inheritdoc cref="ApproximatelyEqualsPoint"/>
        public static bool ApproximatelyEqualsPoint(this Vector2 point, Vector2 other, float tolerance = 0.001f)
        {
            return ApproximatelyEqualsPoint(point, other, Vector2.one * tolerance);
        }

        /// <summary>
        /// Compares the magnitude and the angle of two vectors and returns wether their difference is less than the given tolerances
        /// </summary>
        /// <remarks>Warning : This method can use up to 3 times <see cref="Mathf.Sqrt(float)"/></remarks>
        /// <param name="angleTolerance">Angle tolerance (in degrees)</param>
        /// <returns>Wether the difference in magnitude and angle between the two vectors is less than the tolerances given</returns>
        public static bool ApproximatelyEqualsDir(this Vector2 dir, Vector2 other, float magnitudeTolerance = 0.001f, float angleTolerance = 0.01f)
        {
            float angle = Vector2.Angle(dir, other);
            if (angle > angleTolerance)
            {
                return false;
            }
            return Mathf.Abs(dir.magnitude - other.magnitude) < magnitudeTolerance;
        }

        /// <summary>
        /// Snaps the given vector to a grid of size (<paramref name="snap"/>; <paramref name="snap"/>)
        /// </summary>
        /// <param name="snap">The size of the grid cells</param>
        /// <returns>The given vector snapped to the grid</returns>
        public static Vector2 Snap(this Vector2 v, float snap)
        {
            return v.Snap(snap, snap);
        }

        /// <summary>
        /// Snaps the given vector to a grid of size (<paramref name="snapX"/>; <paramref name="snapY"/>)
        /// </summary>
        /// <returns>The given vector snapped to the grid</returns>
        public static Vector2 Snap(this Vector2 v, float snapX, float snapY)
        {
            return new Vector2(v.x - v.x % snapX, v.y - v.y % snapY);
        }

        /// <summary>
        /// Gets the signed angle in degrees between <paramref name="from"/> and <paramref name="to"/>
        /// </summary>
        /// <param name="from">The vector from which the angular difference is measured</param>
        /// <param name="to">The vector to which the angular difference is measured</param>
        /// <returns>The signed angle in degrees between the two vectors</returns>
        public static float AngleSigned(this Vector2 from, Vector2 to)
        {
            float sign = Mathf.Sign(from.x * to.y - from.y * to.x);
            return Vector2.Angle(from, to) * sign;
        }

        /// <summary>
        /// Returns the normal vector to the left of a given vector
        /// </summary>
        /// <param name="v"></param>
        /// <returns>The normal vector to the left of <paramref name="v"/></returns>
        public static Vector2 LeftNormal(this Vector2 v)
        {
            return new Vector2(-v.y, v.x);
        }

        /// <summary>
        /// Returns the normal vector to the right of a given vector
        /// </summary>
        /// <param name="v"></param>
        /// <returns>The normal vector to the right of <paramref name="v"/></returns>
        public static Vector2 RightNormal(this Vector2 v)
        {
            return new Vector2(v.y, -v.x);
        }

        /// <summary>
        /// Rotates the vector <paramref name="v"/> of <paramref name="angle"/> degrees
        /// </summary>
        /// <param name="v">The vector to rotate arround <paramref name="axis"/></param>
        /// <param name="angle">The angle (in degrees)</param>
        /// <returns></returns>
        public static Vector2 Rotate(this Vector2 v, float angle)
        {
            float rad = angle * Mathf.Deg2Rad;
            float c = Mathf.Cos(rad);
            float s = Mathf.Sin(rad);
            return new Vector2(c * v.x - s * v.y, s * v.x + c * v.y);
        }

        /// <inheritdoc cref="Vector2.Scale(Vector2, Vector2)"/>
        public static Vector2 CopyScale(this Vector2 v1, Vector2 v2)
        {
            return Vector2.Scale(v1, v2);
        }
    }
    #endregion

    #region Vector3
    public static class Vector3Utils
    {
        /// <inheritdoc cref="Vector2Utils.ProjectOn"/>
        public static Vector3 ProjectOn(this Vector3 vector, Vector3 target)
        {
            return Vector3.Project(vector, target);
        }

        /// <inheritdoc cref="Vector2Utils.ApproximatelyEqualsPoint"/>
        public static bool ApproximatelyEqualsPoint(this Vector3 point, Vector3 other, Vector3 tolerance)
        {
            return
                Math.Abs(point.x - other.x) < tolerance.x &&
                Math.Abs(point.y - other.y) < tolerance.y &&
                Math.Abs(point.z - other.z) < tolerance.z;
        }

        /// <inheritdoc cref="Vector2Utils.ApproximatelyEqualsPoint"/>
        public static bool ApproximatelyEqualsPoint(this Vector3 point, Vector3 other, float tolerance = 0.001f)
        {
            return ApproximatelyEqualsPoint(point, other, Vector3.one * tolerance);
        }

        /// <inheritdoc cref="Vector2Utils.ApproximatelyEqualsDir"/>
        public static bool ApproximatelyEqualsDir(this Vector3 dir, Vector3 other, float magnitudeTolerance = 0.001f, float angleTolerance = 0.01f)
        {
            float angle = Vector3.Angle(dir, other);
            if (angle > angleTolerance)
            {
                return false;
            }
            return Mathf.Abs(dir.magnitude - other.magnitude) < magnitudeTolerance;
        }

        /// <summary>
        /// Snaps the given vector to a grid of size (<paramref name="snap"/>; <paramref name="snap"/>; <paramref name="snap"/>)
        /// </summary>
        /// <returns>The given vector snapped to the grid</returns>
        public static Vector3 Snap(this Vector3 v, float snap)
        {
            return v.Snap(snap, snap, snap);
        }


        /// <summary>
        /// Snaps the given vector to a grid of size (<paramref name="snapX"/>; <paramref name="snapY"/>; <paramref name="snapZ"/>)
        /// </summary>
        /// <returns>The given vector snapped to the grid</returns>
        public static Vector3 Snap(this Vector3 v, float snapX, float snapY, float snapZ)
        {
            return new Vector3(v.x - v.x % snapX, v.y - v.y % snapY, v.z - v.z % snapZ);
        }

        /// <summary>
        /// Rotates the vector <paramref name="v"/> of <paramref name="angle"/> degrees around <paramref name="axis"/>
        /// </summary>
        /// <param name="v">The vector to rotate arround <paramref name="axis"/></param>
        /// <param name="axis">The axis arround which the vector <paramref name="v"/> is rotated</param>
        /// <param name="angle">The angle (in degrees)</param>
        /// <returns></returns>
        public static Vector3 Rotate(this Vector3 v, Vector3 axis, float angle)
        {
            Quaternion rotation = Quaternion.AngleAxis(angle, axis);
            return rotation * v;
        }

        /// <inheritdoc cref="Vector3.Scale(Vector3, Vector3)"/>
        public static Vector3 CopyScale(this Vector3 v1, Vector3 v2)
        {
            return Vector3.Scale(v1, v2);
        }
    }
    #endregion

    #region Vector4
    public static class Vector4Utils
    {
        /// <summary>
        /// Computes the angle in degrees between two <see cref="Vector4"/>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>The angle in degrees between <paramref name="from"/> and <paramref name="to"/></returns>
        public static float Vector4Angle(Vector4 from, Vector4 to)
        {
            float num = Mathf.Sqrt(from.sqrMagnitude * to.sqrMagnitude);
            if (num < 1E-15f)
            {
                return 0f;
            }

            float num2 = Mathf.Clamp(Vector4.Dot(from, to) / num, -1f, 1f);
            return Mathf.Acos(num2) * Mathf.Rad2Deg;
        }

        /// <inheritdoc cref="Vector2Utils.ProjectOn"/>
        public static Vector4 ProjectOn(this Vector4 vector, Vector4 target)
        {
            return Vector4.Project(vector, target);
        }

        /// <inheritdoc cref="Vector2Utils.ApproximatelyEqualsPoint"/>
        public static bool ApproximatelyEqualsPoint(this Vector4 point, Vector4 other, Vector4 tolerance)
        {
            return
                Math.Abs(point.x - other.x) < tolerance.x &&
                Math.Abs(point.y - other.y) < tolerance.y &&
                Math.Abs(point.z - other.z) < tolerance.z &&
                Math.Abs(point.w - other.w) < tolerance.w;
        }

        /// <inheritdoc cref="Vector2Utils.ApproximatelyEqualsPoint"/>
        public static bool ApproximatelyEqualsPoint(this Vector4 point, Vector4 other, float tolerance = 0.001f)
        {
            return ApproximatelyEqualsPoint(point, other, Vector4.one * tolerance);
        }

        /// <inheritdoc cref="Vector2Utils.ApproximatelyEqualsDir"/>
        public static bool ApproximatelyEqualsDir(this Vector4 dir, Vector4 other, float magnitudeTolerance = 0.001f, float angleTolerance = 0.01f)
        {
            float angle = Vector4Angle(dir, other);
            if (angle > angleTolerance)
            {
                return false;
            }
            return Mathf.Abs(dir.magnitude - other.magnitude) < magnitudeTolerance;
        }

        /// <summary>
        /// Snaps the given vector to a grid of size (<paramref name="snap"/>; <paramref name="snap"/>; <paramref name="snap"/>; <paramref name="snap"/>)
        /// </summary>
        /// <returns>The given vector snapped to the grid</returns>
        public static Vector4 Snap(this Vector4 v, float snap)
        {
            return v.Snap(snap, snap, snap, snap);
        }


        /// <summary>
        /// Snaps the given vector to a grid of size (<paramref name="snapX"/>; <paramref name="snapY"/>; <paramref name="snapZ"/>; <paramref name="snapW"/>)
        /// </summary>
        /// <returns>The given vector snapped to the grid</returns>
        public static Vector4 Snap(this Vector4 v, float snapX, float snapY, float snapZ, float snapW)
        {
            return new Vector4(v.x - v.x % snapX, v.y - v.y % snapY, v.z - v.z % snapZ, v.w - v.w % snapW);
        }

        /// <inheritdoc cref="Vector4.Scale(Vector4, Vector4)"/>
        public static Vector4 CopyScale(this Vector4 v1, Vector4 v2)
        {
            return Vector4.Scale(v1, v2);
        }
    }
    #endregion

    public static class FloatUtils
    {
        /// <summary>
        /// Remaps the value of <paramref name="input"/> 
        /// from the range <paramref name="initialInterval"/> to the range <paramref name="targetInterval"/>
        /// </summary>
        /// <returns>The remapped value of <paramref name="input"/></returns>
        public static float Remap(this float input, Vector2 initialInterval, Vector2 targetInterval)
        {
            return Remap(input, initialInterval.x, initialInterval.y, targetInterval.x, targetInterval.y);
        }

        /// <summary>
        /// Remaps the value of <paramref name="input"/> 
        /// from the range [<paramref name="initialMin"/>, <paramref name="initialMax"/>] 
        /// to the range [<paramref name="targetMin"/>, <paramref name="targetMax"/>]
        /// </summary>
        /// <returns>The remapped value of <paramref name="input"/></returns>
        public static float Remap(this float input, float initialMin, float initialMax, float targetMin, float targetMax)
        {
            float ratio = (input - initialMin) / (initialMax - initialMin);
            return targetMin + ratio * (targetMax - targetMin);
        }
    }

    public static class IntUtils
    {
        public static bool HasFlag(this int val, int flag)
        {
            return (val & flag) == flag;
        }
    }

    #region Misc
    public static class MathUtils
    {
        /// <summary>
        /// Tries to parse a <see cref="string"/> representing an hexadecimal number to an <see cref="int"/>
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="intValue"></param>
        /// <returns>Wether the conversion was a success</returns>
        public static bool TryHexToInt(string hex, out int intValue)
        {
            intValue = 0;
            if (string.IsNullOrEmpty(hex))
            {
                return false;
            }
            hex = hex.ToLower();
            if (hex.All((char c) => c.Between('0', '9') || c.Between('a', 'f')))
            {
                intValue = Convert.ToInt32(hex, 16);
                return true;
            }

            return false;
        }
    }
    #endregion
}
