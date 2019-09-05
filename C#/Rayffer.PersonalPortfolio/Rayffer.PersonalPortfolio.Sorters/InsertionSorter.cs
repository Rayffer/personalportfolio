using Rayffer.PersonalPortfolio.Sorters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Rayffer.PersonalPortfolio.Sorters
{
    public class InsertionSorter<SortType> : ISorter<SortType> where SortType : IComparable<SortType>
    {
        public List<SortType> SortedList { get; private set; }
        public int CurrentSortedListIndex { get; private set; }

        public IEnumerable<SortType> SortAscending(IEnumerable<SortType> listToSort, int sleep = 0)
        {
            SortedList = listToSort.ToList();
            int listSize = listToSort.Count();

            for (int listToSortIndex = 0; listToSortIndex < listSize; listToSortIndex++)
            {
                CurrentSortedListIndex = listToSortIndex;
                var elementToSort = listToSort.ElementAt(listToSortIndex);
                Thread.Sleep(sleep);
                SortedList.Remove(elementToSort);
                int sortIndex = SortedList.FindIndex(listElement => listElement.CompareTo(elementToSort) >= 0);
                CurrentSortedListIndex = sortIndex;
                SortedList.Insert(sortIndex < 0 ? listSize - 1 : sortIndex, elementToSort);
                Thread.Sleep(sleep);
            }

            return SortedList;
        }

        public IEnumerable<SortType> SortDescending(IEnumerable<SortType> listToSort, int sleep = 0)
        {
            SortedList = listToSort.ToList();
            int listSize = listToSort.Count();

            for (int listToSortIndex = 0; listToSortIndex < listSize; listToSortIndex++)
            {
                CurrentSortedListIndex = listToSortIndex;
                var elementToSort = listToSort.ElementAt(listToSortIndex);
                Thread.Sleep(sleep / 2);
                SortedList.Remove(elementToSort);
                int sortIndex = SortedList.FindIndex(listElement => listElement.CompareTo(elementToSort) < 0);
                CurrentSortedListIndex = sortIndex;
                SortedList.Insert(sortIndex < 0 ? listSize - 1 : sortIndex, elementToSort);
                Thread.Sleep(sleep / 2);
            }

            return SortedList;
        }
    }
}