using Rayffer.PersonalPortfolio.Sorters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rayffer.PersonalPortfolio.Sorters
{
    public class InsertionSorter<SortType> : ISorter<SortType> where SortType : IComparable<SortType>
    {
        public IEnumerable<SortType> SortAscending(IEnumerable<SortType> listToSort)
        {
            List<SortType> sortedList = listToSort.ToList();
            int listSize = listToSort.Count();

            for (int listToSortIndex = 0; listToSortIndex < listSize; listToSortIndex++)
            {
                var elementToSort = listToSort.ElementAt(listToSortIndex);
                sortedList.Remove(elementToSort);
                int sortIndex = sortedList.FindIndex(listElement => listElement.CompareTo(elementToSort) >= 0);
                sortedList.Insert(sortIndex < 0 ? listSize - 1 : sortIndex, elementToSort);
            }

            return sortedList;
        }

        public IEnumerable<SortType> SortDescending(IEnumerable<SortType> listToSort)
        {
            List<SortType> sortedList = listToSort.ToList();
            int listSize = listToSort.Count();

            for (int listToSortIndex = 0; listToSortIndex < listSize; listToSortIndex++)
            {
                var elementToSort = listToSort.ElementAt(listToSortIndex);
                sortedList.Remove(elementToSort);
                int sortIndex = sortedList.FindIndex(listElement => listElement.CompareTo(elementToSort) < 0);
                sortedList.Insert(sortIndex < 0 ? listSize - 1 : sortIndex, elementToSort);
            }

            return sortedList;
        }
    }
}