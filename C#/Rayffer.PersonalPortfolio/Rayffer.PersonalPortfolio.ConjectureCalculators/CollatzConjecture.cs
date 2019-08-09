using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayffer.PersonalPortfolio.ConjectureCalculators
{
    public static class CollatzConjecture
    {
        public static IEnumerable<long> CalculateSteps(long startNumber)
        {
            if (startNumber < 0)
                return new List<long>();

            long conjectureValue = startNumber;

            List<long> conjectureSteps = new List<long>();

            Task conjectureTask = Task.Run(() =>
            {
                while (conjectureValue > 1)
                {
                    conjectureSteps.Add(conjectureValue);
                    conjectureValue = conjectureValue % 2 == 0 ? conjectureValue / 2 : conjectureValue * 3;
                }
            });

            if (Task.WaitAll(new Task[] { conjectureTask }, new TimeSpan(0, 5, 0)))
            {
                return conjectureSteps;
            }
            else
            {
                return new List<long>();
            }

        }
    }
}
