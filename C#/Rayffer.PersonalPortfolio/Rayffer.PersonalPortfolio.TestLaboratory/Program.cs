using Rayffer.PersonalPortfolio.ConjectureCalculators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Rayffer.PersonalPortfolio.TestLaboratory
{
    internal class Program
    {
        private static void Main(string[] args)

        {
            Stopwatch conjectureStopWatch = new Stopwatch();
            List<IEnumerable<long>> conjectures = new List<IEnumerable<long>>();
            for (int conjcetureStartingNumber = 1; conjcetureStartingNumber < 1000001; conjcetureStartingNumber++)
            {
                conjectureStopWatch.Restart();
                IEnumerable<long> conjectureSteps = CollatzConjecture.CalculateSteps(conjcetureStartingNumber);
                conjectureStopWatch.Stop();
                conjectures.Add(conjectureSteps);
                Console.WriteLine($"The collatz conjecture for {conjcetureStartingNumber} value took {conjectureStopWatch.Elapsed.TotalSeconds} seconds and {conjectureSteps.Count()} steps");
            }
            Console.WriteLine($"The maximum steps taken for the conjecture were {conjectures.Max(steps => steps.Count())}, and the minimum was {conjectures.Min(steps => steps.Count())}");
            Console.ReadKey();
        }
    }
}