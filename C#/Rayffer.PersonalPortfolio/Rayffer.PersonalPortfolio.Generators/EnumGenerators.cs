using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayffer.PersonalPortfolio.Generators
{
    public static class EnumGenerators
    {
        /// <summary>
        /// Generates a list that contains all the members for the given enum, optionally including the default enum member
        /// </summary>
        /// <typeparam name="T">The type from which to generate a list of members</typeparam>
        /// <param name="includeAllValues">If the list should include the default enum member or not</param>
        /// <returns></returns>
        public static List<T> GenerateEnumMembers<T>(bool includeAllValues = false) where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Where(en => includeAllValues || !(en.Equals(default(T))))
                .ToList();
        }

    }
}
