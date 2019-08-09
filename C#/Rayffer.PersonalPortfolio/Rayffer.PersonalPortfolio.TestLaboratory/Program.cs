using Rayffer.PersonalPortfolio.Generators;
using Rayffer.PersonalPortfolio.Sorters;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Rayffer.PersonalPortfolio.TestLaboratory
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<long> testList = new List<long>();

            for (int i = 0; i < 100; i++)
            {
                testList.Add(RandomValueGenerator.GetRandomValue(int.MaxValue, 0));
            }

            BubbleSorter<long> sorter = new BubbleSorter<long>();

            var ascendingSortedList = sorter.SortAscending(testList);
            var descendingSortedList = sorter.SortDescending(testList);
            Console.ReadLine();
        }
    }
}