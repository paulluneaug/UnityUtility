using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using UnityEngine;

using UnityUtility.Extensions;

namespace UnityUtility.Utils
{
    public static class StringUtils
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

        /// <summary>
        /// Parse a string to a color 
        /// </summary>
        /// <remarks>
        /// Supported formats : <br/>
        /// - "(r, g, b)" where r, g and b belongs to [0; 255]<br/>
        /// - "(r, g, b, a)" where r, g, b and a belongs to [0; 255]<br/>
        /// - "#rgb" where r, g and b are hexadecimal numbers of 2 characters each<br/>
        /// <br/>
        /// Note : The whitespaces are ignored
        /// </remarks>
        /// <param name="colorString"></param>
        /// <param name="color"></param>
        /// <returns>Wether the conversion was a success</returns>
        public static bool TryParseColor(string colorString, out Color color)
        {
            color = Color.black;
            if (string.IsNullOrEmpty(colorString))
            {
                return false;
            }

            colorString = colorString.Replace(" ", "");
            if (colorString.StartsWith('(') && colorString.EndsWith(')')) // For the formats (r, g, b) and (r, g, b, a)
            {
                string colorValuesString = colorString[1..^1];
                List<(bool parseSuceeded, int parsedValue)> parsedValues = colorValuesString
                    .Split(',')
                    .Select((string s) => (int.TryParse(s, out int parsedValue), parsedValue))
                    .ToList();

                if (parsedValues.All((result) => result.parseSuceeded))
                {
                    int parsedValueCount = parsedValues.Count;
                    if (parsedValueCount == 3)
                    {
                        color = new Color(
                            parsedValues[0].parsedValue / 255.0f,
                            parsedValues[1].parsedValue / 255.0f,
                            parsedValues[2].parsedValue / 255.0f);
                        return true;
                    }
                    if (parsedValueCount == 4)
                    {
                        color = new Color(
                            parsedValues[0].parsedValue / 255.0f,
                            parsedValues[1].parsedValue / 255.0f,
                            parsedValues[2].parsedValue / 255.0f,
                            parsedValues[3].parsedValue / 255.0f);
                        return true;
                    }
                }
            }
            else if (colorString.StartsWith('#')) // For the format #rgb
            {
                colorString = colorString[1..];
                if (colorString.Length == 6)
                {
                    (bool isHex, int intValue)[] hexParseResults =
                        colorString.SplitBySize(2)
                        .Select((string hexSt) => (TryHexToInt(hexSt, out int intValue), intValue))
                        .ToArray();

                    if (hexParseResults.All(result => result.isHex))
                    {
                        color = new Color(
                            hexParseResults[0].intValue / 255.0f,
                            hexParseResults[1].intValue / 255.0f,
                            hexParseResults[2].intValue / 255.0f);
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsInteger(string st)
        {
            for (int iChar = 0; iChar < st.Length; iChar++)
            {
                char c = st[iChar];
                if (!char.IsDigit(c))
                {
                    if (iChar == 0 && c == '-')
                    {
                        continue;
                    }
                    return false;
                }
            }
            return true;
        }
    }
}
