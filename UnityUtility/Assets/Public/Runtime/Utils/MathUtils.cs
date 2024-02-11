using System;

namespace UnityUtility.Utils
{
    public static class MathUtils
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
        /// Compares <paramref name="value"/> and <paramref name="threshold"/> 
        /// and returns wether <c><paramref name="value"/> <![CDATA[>]]> <paramref name="threshold"/></c>
        /// </summary>
        /// 
        /// <typeparam name="T">Type of the operandes</typeparam>
        /// <param name="value">Value to compare</param>
        /// <param name="min">Lower bound</param>
        /// <param name="max">Higher bound</param>
        /// <param name="inclusive">Wether the bounds belongs to the interval</param>
        /// <returns>Wether <paramref name="value"/> belongs in the interval [<paramref name="min"/> ; <paramref name="max"/>] </returns>
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
}
