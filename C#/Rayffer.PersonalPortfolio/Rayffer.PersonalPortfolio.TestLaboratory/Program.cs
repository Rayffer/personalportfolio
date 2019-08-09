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
            for (int conjcetureStartingNumber = 0; conjcetureStartingNumber < 1000000; conjcetureStartingNumber++)
            {
                conjectureStopWatch.Restart();
                IEnumerable<long> conjectureSteps = CollatzConjecture.CalculateSteps(conjcetureStartingNumber);
                conjectureStopWatch.Stop();

                Console.WriteLine($"The collatz conjecture for {conjcetureStartingNumber} value took {conjectureStopWatch.Elapsed.TotalSeconds} seconds and {conjectureSteps.Count()} steps");
            }

            Console.ReadKey();
        }
    }
}