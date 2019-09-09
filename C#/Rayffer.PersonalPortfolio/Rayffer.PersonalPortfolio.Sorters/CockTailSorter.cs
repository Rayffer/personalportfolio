using Rayffer.PersonalPortfolio.Sorters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Rayffer.PersonalPortfolio.Sorters
{
    public class CockTailSorter<SortType> : ISorter<SortType> where SortType : IComparable<SortType>
    {
        public List<SortType> SortedList { get; private set; }
        public int CurrentSortedListIndex { get; private set; }


        public IEnumerable<SortType> SortAscending(IEnumerable<SortType> listToSort, int sleep = 0)
        {
            SortedList = listToSort.ToList();
            int swapOperations = SortedList.Count - 1;
            for (int sortIteration = 0; sortIteration < swapOperations / 2; sortIteration++)
            {
                bool swapped = false;
                for (int sortIndex = sortIteration; sortIndex < swapOperations - sortIteration; sortIndex++)
                {
                    CurrentSortedListIndex = sortIndex;
                    SortType firstComparedElement = SortedList[sortIndex];
                    SortType secondComparedElement = SortedList[sortIndex + 1];
                    if (firstComparedElement.CompareTo(secondComparedElement) > 0)
                    {
                        swapped = true;
                        SortedList[sortIndex] = secondComparedElement;
                        SortedList[sortIndex + 1] = firstComparedElement;
                    }
                    Thread.Sleep(sleep);
                }
                if (!swapped)
                    break;
                swapped = false;
                for (int sortIndex = swapOperations - sortIteration; sortIndex > sortIteration; sortIndex--)
                {
                    CurrentSortedListIndex = sortIndex;
                    SortType firstComparedElement = SortedList[sortIndex];
                    SortType secondComparedElement = SortedList[sortIndex - 1];
                    if (firstComparedElement.CompareTo(secondComparedElement) < 0)
                    {
                        swapped = true;
                        SortedList[sortIndex] = secondComparedElement;
                        SortedList[sortIndex - 1] = firstComparedElement;
                    }
                    Thread.Sleep(sleep);
                }
                if (!swapped)
                    break;
            }
            return SortedList;
        }

        public IEnumerable<SortType> SortDescending(IEnumerable<SortType> listToSort, int sleep = 0)
        {
            SortedList = listToSort.ToList();
            int swapOperations = SortedList.Count - 1;
            for (int sortIteration = 0; sortIteration < swapOperations / 2; sortIteration++)
            {
                bool swapped = false;
                for (int sortIndex = sortIteration; sortIndex < swapOperations - sortIteration; sortIndex++)
                {
                    CurrentSortedListIndex = sortIndex;
                    SortType firstComparedElement = SortedList[sortIndex];
                    SortType secondComparedElement = SortedList[sortIndex + 1];
                    if (firstComparedElement.CompareTo(secondComparedElement) < 0)
                    {
                        swapped = true;
                        SortedList[sortIndex] = secondComparedElement;
                        SortedList[sortIndex + 1] = firstComparedElement;
                    }
                    Thread.Sleep(sleep);
                }
                if (!swapped)
                    break;
                swapped = false;
                for (int sortIndex = swapOperations - sortIteration; sortIndex > sortIteration; sortIndex--)
                {
                    CurrentSortedListIndex = sortIndex;
                    SortType firstComparedElement = SortedList[sortIndex];
                    SortType secondComparedElement = SortedList[sortIndex - 1];
                    if (firstComparedElement.CompareTo(secondComparedElement) > 0)
                    {
                        swapped = true;
                        SortedList[sortIndex] = secondComparedElement;
                        SortedList[sortIndex - 1] = firstComparedElement;
                    }
                    Thread.Sleep(sleep);
                }
                if (!swapped)
                    break;
            }
            return SortedList;
        }
    }
}