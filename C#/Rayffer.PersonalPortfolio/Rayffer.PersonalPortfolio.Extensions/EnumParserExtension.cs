using System;
using System.Collections.Generic;
using System.Linq;

namespace Rayffer.PersonalPortfolio.Extensions
{
    public static class EnumParserExtension
    {

        /// <summary>
        /// This method safely parses an enum value using tryparse that, in the case that the parse is not successful it will return the 
        /// enum's default value
        /// </summary>
        /// <typeparam name="T">The enum type to parse</typeparam>
        /// <param name="enumToParse">The enum from which to know which type to parse</param>
        /// <param name="stringToParse">The string that is going to be parsed</param>
        /// <returns>Either the parsed value if the parse is successful or the default value if it is not</returns>
        public static T ParseEnumValueFromString<T>(this T enumToParse, string stringToParse) where T : struct, Enum
        {
            if (Enum.TryParse(stringToParse, true, out T valueToReturn))
                return valueToReturn;
            else
                return default(T);
        }
    }
}