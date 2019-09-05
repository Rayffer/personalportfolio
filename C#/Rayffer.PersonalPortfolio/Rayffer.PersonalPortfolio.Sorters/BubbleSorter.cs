using Rayffer.PersonalPortfolio.Sorters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Rayffer.PersonalPortfolio.Sorters
{
    public class BubbleSorter<SortType> : ISorter<SortType> where SortType : IComparable<SortType>
    {
        public List<SortType> SortedList { get; private set; }
        public int CurrentSortedListIndex { get; private set; }

        public IEnumerable<SortType> SortAscending(IEnumerable<SortType> listToSort, int sleep = 0)
        {
            SortedList = listToSort.ToList();
            int swapOperations = SortedList.Count - 1;
            for (int sortIteration = 0; sortIteration < swapOperations; sortIteration++)
            {
                for (int sortIndex = 0; sortIndex < swapOperations - sortIteration; sortIndex++)
                {
                    CurrentSortedListIndex = sortIndex;
                    SortType firstComparedElement = SortedList[sortIndex];
                    SortType secondComparedElement = SortedList[sortIndex + 1];
                    if (firstComparedElement.CompareTo(secondComparedElement) > 0)
                    {
                        SortedList[sortIndex] = secondComparedElement;
                        SortedList[sortIndex + 1] = firstComparedElement;
                    }
                    Thread.Sleep(sleep);
                }
            }
            return SortedList;
        }

        public IEnumerable<SortType> SortDescending(IEnumerable<SortType> listToSort, int sleep = 0)
        {
            SortedList = listToSort.ToList();
            int swapOperations = SortedList.Count - 1;
            for (int sortIteration = 0; sortIteration < swapOperations; sortIteration++)
            {
                for (int sortIndex = 0; sortIndex < swapOperations - sortIteration; sortIndex++)
                {
                    CurrentSortedListIndex = sortIndex;
                    SortType firstComparedElement = SortedList[sortIndex];
                    SortType secondComparedElement = SortedList[sortIndex + 1];
                    if (firstComparedElement.CompareTo(secondComparedElement) < 0)
                    {
                        SortedList[sortIndex] = secondComparedElement;
                        SortedList[sortIndex + 1] = firstComparedElement;
                    }
                    Thread.Sleep(sleep);
                }
            }
            return SortedList;
        }
    }
}