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
            SelectionSorter<int> selectionSorter = new SelectionSorter<int>();

            var asd = selectionSorter.SortAscending(Enumerable.Range(0, 100), 0);
            var asd2 = selectionSorter.SortDescending(Enumerable.Range(0, 100), 0);

            Console.ReadLine();
        }
    }
}