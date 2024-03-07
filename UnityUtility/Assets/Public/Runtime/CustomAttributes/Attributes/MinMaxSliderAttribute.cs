using UnityEngine;

namespace UnityUtility.CustomAttributes
{
    /// <summary>
    /// Attribute used to make a <see cref="Vector2"/> variable in a script be restricted to a specific range
    /// </summary>
    public class MinMaxSliderAttribute : PropertyAttribute
    {
        public const string WRONG_TYPE_ERROR = nameof(MinMaxSliderAttribute) +" cannot be applied to variables of type";

        public float MinValue { get; }
        public float MaxValue { get; }
        public bool ShowFields { get; }
        public int RoundDigits { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minValue">Minimum allowed value</param>
        /// <param name="maxValue">Maximum allowed value</param>
        /// <param name="showFields">Wether the <see cref="Vector2"/> values (x,y) are  diplayed arround the slider</param>
        /// <param name="roundDigits">Number of digits used to round the values</param>
        public MinMaxSliderAttribute(float minValue, float maxValue, bool showFields = true, int roundDigits = 2)
		{
            MinValue = minValue;
            MaxValue = maxValue;

			ShowFields = showFields;
			RoundDigits = roundDigits;
		}
	}
}
