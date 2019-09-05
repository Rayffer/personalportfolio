using Rayffer.PersonalPortfolio.Sorters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Rayffer.PersonalPortfolio.Sorters
{
    public class MergeSorter<SortType> : ISorter<SortType> where SortType : IComparable<SortType>
    {
        private readonly object LockObject = new object();

        private List<SortType> _sortedList;

        public List<SortType> SortedList {
            get
            {
                lock (LockObject)
                {
                    return _sortedList; 
                }
            }
            private set => _sortedList = value; }
        public int CurrentSortedListIndex { get; private set; }

        public IEnumerable<SortType> SortAscending(IEnumerable<SortType> listToSort, int sleep = 0)
        {
            if (SortedList == null)
            {
                SortedList = listToSort.ToList();
            }
            var listItems = listToSort.Count();
            if (listItems == 1)
            {
                return listToSort;
            }

            int listPivot = (int)Math.Ceiling((double)listItems / 2);

            var firstHalfOfList = listToSort.Take(listPivot);
            var secondHalfOfList = listToSort.Skip(listPivot);

            var sortedFirstHalfOfList = SortAscending(firstHalfOfList, sleep);
            var sortedSecondHalfOfList = SortAscending(secondHalfOfList, sleep);
            List<SortType> resultList = new List<SortType>();
            while (sortedFirstHalfOfList.Any() && sortedSecondHalfOfList.Any())
            {
                SortType firstElementOfFirstHalfList = sortedFirstHalfOfList.FirstOrDefault();
                SortType firstElementOfSecondHalfList = sortedSecondHalfOfList.FirstOrDefault();

                if (firstElementOfFirstHalfList.CompareTo(firstElementOfSecondHalfList) < 0)
                {
                    sortedFirstHalfOfList = sortedFirstHalfOfList.Skip(1);
                    resultList.Add(firstElementOfFirstHalfList);
                }
                else
                {
                    sortedSecondHalfOfList = sortedSecondHalfOfList.Skip(1);
                    resultList.Add(firstElementOfSecondHalfList);
                }
            }

            SortedList.RemoveAll(sortedItem => resultList.Any(sortedSubItem => sortedSubItem.Equals(sortedItem)));
            SortedList.InsertRange(0, resultList);
            if (sortedFirstHalfOfList.Any())
            {
                SortedList.RemoveAll(sortedItem => sortedFirstHalfOfList.Any(sortedSubItem => sortedSubItem.Equals(sortedItem)));
                SortedList.InsertRange(resultList.Count(), sortedFirstHalfOfList);
                resultList.AddRange(sortedFirstHalfOfList);
            }
            else
            {
                SortedList.RemoveAll(sortedItem => sortedSecondHalfOfList.Any(sortedSubItem => sortedSubItem.Equals(sortedItem)));
                SortedList.InsertRange(resultList.Count(), sortedSecondHalfOfList);
                resultList.AddRange(sortedSecondHalfOfList);
            }

            Thread.Sleep(sleep);


            return resultList;
        }

        public IEnumerable<SortType> SortDescending(IEnumerable<SortType> listToSort, int sleep = 0)
        {
            Thread.Sleep(sleep);
            var listItems = listToSort.Count();
            if (listItems == 1)
            {
                return listToSort;
            }

            int listPivot = (int)Math.Ceiling((double)listItems / 2);

            var firstHalfOfList = listToSort.Take(listPivot);
            var secondHalfOfList = listToSort.Skip(listPivot);

            var sortedFirstHalfOfList = SortDescending(firstHalfOfList, sleep);
            var sortedSecondHalfOfList = SortDescending(secondHalfOfList, sleep);

            List<SortType> resultList = new List<SortType>();
            while (sortedFirstHalfOfList.Any() && sortedSecondHalfOfList.Any())
            {
                SortType firstElementOfFirstHalfList = sortedFirstHalfOfList.FirstOrDefault();
                SortType firstElementOfSecondHalfList = sortedSecondHalfOfList.FirstOrDefault();

                if (firstElementOfFirstHalfList.CompareTo(firstElementOfSecondHalfList) > 0)
                {
                    sortedFirstHalfOfList = sortedFirstHalfOfList.Skip(1);
                    resultList.Add(firstElementOfFirstHalfList);
                }
                else
                {
                    sortedSecondHalfOfList = sortedSecondHalfOfList.Skip(1);
                    resultList.Add(firstElementOfSecondHalfList);
                }
            }

            if (sortedFirstHalfOfList.Any())
            {
                resultList.AddRange(sortedFirstHalfOfList);
            }
            else
            {
                resultList.AddRange(sortedSecondHalfOfList);
            }

            return resultList;
        }
    }
}