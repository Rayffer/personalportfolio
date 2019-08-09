using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayffer.PersonalPortfolio.Generators
{
    /// <summary>
    /// This class intent is to provide a way to generate random numbers from the same seed anywhere in the application or service where it is used.
    /// </summary>
    public static class RandomValueGenerator
    {
        /// <summary>
        /// The instance that generates the random values, it is set to private as it should not be modified after it has been created.
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// Calculates a random number between <seealso cref="int.MinValue"/> and <seealso cref="int.MaxValue"/>.
        /// </summary>
        /// <returns>The calcuated random value</returns>
        public static int GetRandomValue() => random.Next(int.MinValue, int.MaxValue);

        /// <summary>
        /// Calculates a random number between a maximum and an optionally specified minimum values.
        /// </summary>
        /// <param name="maxNumber">The upper bound (not inclusive) of the random value</param>.
        /// <param name="minNumber">The optional lower bound (inclusive) of the random value, when not specified, the minimum value is 0</param>.
        /// <returns>The calcuated random value</returns>
        public static int GetRandomValue(int maxNumber, int minNumber = 0) => random.Next(minNumber, maxNumber);
    }
}
