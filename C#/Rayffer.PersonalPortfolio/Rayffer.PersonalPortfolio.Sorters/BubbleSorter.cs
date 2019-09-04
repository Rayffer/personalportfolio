using Rayffer.PersonalPortfolio.Sorters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Rayffer.PersonalPortfolio.Sorters
{
    public class BubbleSorter<SortType> : ISorter<SortType> where SortType : IComparable<SortType>
    {
        public IEnumerable<SortType> SortAscending(IEnumerable<SortType> listToSort, int sleep = 0)
        {
            List<SortType> sortedList = listToSort.ToList();
            int swapOperations = sortedList.Count - 1;
            for (int sortIteration = 0; sortIteration < swapOperations; sortIteration++)
            {
                for (int sortIndex = sortIteration; sortIndex < swapOperations; sortIndex++)
                {
                    SortType firstComparedElement = sortedList[sortIndex];
                    SortType secondComparedElement = sortedList[sortIndex + 1];
                    if (firstComparedElement.CompareTo(secondComparedElement) > 0)
                    {
                        sortedList[sortIndex] = secondComparedElement;
                        sortedList[sortIndex + 1] = firstComparedElement;
                    }
                    Thread.Sleep(sleep);
                }
            }
            return sortedList;
        }

        public IEnumerable<SortType> SortDescending(IEnumerable<SortType> listToSort, int sleep = 0)
        {
            List<SortType> sortedList = listToSort.ToList();
            int swapOperations = sortedList.Count - 1;
            for (int sortIteration = 0; sortIteration < swapOperations; sortIteration++)
            {
                for (int sortIndex = sortIteration; sortIndex < swapOperations; sortIndex++)
                {
                    SortType firstComparedElement = sortedList[sortIndex];
                    SortType secondComparedElement = sortedList[sortIndex + 1];
                    if (firstComparedElement.CompareTo(secondComparedElement) < 0)
                    {
                        sortedList[sortIndex] = secondComparedElement;
                        sortedList[sortIndex + 1] = firstComparedElement;
                    }
                    Thread.Sleep(sleep);
                }
            }
            return sortedList;
        }
    }
}