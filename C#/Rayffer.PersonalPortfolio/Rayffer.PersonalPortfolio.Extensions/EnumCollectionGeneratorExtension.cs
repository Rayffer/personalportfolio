using System;
using System.Collections.Generic;
using System.Linq;

namespace Rayffer.PersonalPortfolio.Extensions
{
    public static class EnumCollectionGeneratorExtension
    {
        /// <summary>
        /// Generates a list that contains all the members for the given enum, optionally you can include the default enum member
        /// </summary>
        /// <typeparam name="T">The type from which to generate a list of members</typeparam>
        /// <param name="includeAllValues">If the list should include the default enum member or not</param>
        /// <returns></returns>
        public static List<T> GenerateEnumMembers<T>(bool includeAllValues = false) where T : Enum
        {
            return 
                Enum.GetValues(typeof(T))
                .Cast<T>()
                .Where(en => includeAllValues || !(en.Equals(default(T))))
                .ToList();
        }

        public static T ParseEnumValueFromString<T>(this T enumToParse, string stringToParse) where T : struct, Enum
        {
            if (Enum.TryParse(stringToParse, true, out T valueToReturn))
                return valueToReturn;
            else
                return default(T);
        }
    }
}