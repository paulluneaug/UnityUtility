using System;

using UnityUtility.Extensions;

namespace UnityUtility.Utils
{
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
            };
        }
    }
}
