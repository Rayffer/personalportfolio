using Rayffer.PersonalPortfolio.ConjectureCalculators;
using Rayffer.PersonalPortfolio.Generators;
using Rayffer.PersonalPortfolio.QueueManagers;
using Rayffer.PersonalPortfolio.Sorters;
using Rayffer.PersonalPortfolio.UnityFactory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Rayffer.PersonalPortfolio.TestLaboratory
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var conjectureSteps = CollatzConjecture.CalculateSteps(150);

            Console.ReadLine();
        }
    }
}