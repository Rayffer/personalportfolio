using Rayffer.PersonalPortfolio.Sorters.Interfaces;
using Rayffer.PersonalPortfolio.Sorters.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Rayffer.PersonalPortfolio.Sorters
{
    public class QuickSorter<SortType> : ISorter<SortType> where SortType : IComparable<SortType>
    {
        private readonly object LockObject = new object();

        private List<SortType> _sortedList;

        public List<SortType> SortedList
        {
            get
            {
                lock (LockObject)
                {
                    return _sortedList;
                }
            }
            private set => _sortedList = value;
        }
        public int CurrentSortedListIndex { get; private set; }

        private Random randomizer;
        private readonly QuickSortPivotTypes quickSortPivotType;

        public QuickSorter(QuickSortPivotTypes quickSortPivotType)
        {
            this.quickSortPivotType = quickSortPivotType;
            randomizer = new Random();
        }

        public IEnumerable<SortType> SortAscending(IEnumerable<SortType> listToSort, int sleep = 0)
        {
            if (SortedList == null)
            {
                SortedList = listToSort.ToList();
            }
            
            if (!listToSort.Any())
            {
                return listToSort;
            }

            SortType listPivot = GetPivot(listToSort);

            IEnumerable<SortType> elementsLessOrEqualThanPivot = listToSort.Where(element => element.CompareTo(listPivot) <= 0).Except(new List<SortType> { listPivot });
            IEnumerable<SortType> elementsGreaterThanPivot = listToSort.Where(element => element.CompareTo(listPivot) > 0);

            List<SortType> sortedElementsLessOrEqualThanPivot = SortAscending(elementsLessOrEqualThanPivot, sleep).ToList();
            IEnumerable<SortType> sortedElementsGreaterThanPivot = SortAscending(elementsGreaterThanPivot, sleep);

            sortedElementsLessOrEqualThanPivot.Add(listPivot);
            
            SortedList.RemoveAll(sortedItem => sortedElementsLessOrEqualThanPivot.Any(sortedSubItem => sortedSubItem.Equals(sortedItem)));
            SortedList.InsertRange(0, sortedElementsLessOrEqualThanPivot);
            SortedList.RemoveAll(sortedItem => sortedElementsGreaterThanPivot.Any(sortedSubItem => sortedSubItem.Equals(sortedItem)));
            SortedList.InsertRange(sortedElementsLessOrEqualThanPivot.Count(), sortedElementsGreaterThanPivot);

            Thread.Sleep(sleep);

            return sortedElementsLessOrEqualThanPivot.Concat(sortedElementsGreaterThanPivot);
        }

        public IEnumerable<SortType> SortDescending(IEnumerable<SortType> listToSort, int sleep = 0)
        {
            Thread.Sleep(sleep);
            if (!listToSort.Any())
            {
                return listToSort;
            }

            SortType listPivot = GetPivot(listToSort);

            IEnumerable<SortType> elementsLessOrEqualThanPivot = listToSort.Where(element => element.CompareTo(listPivot) < 0);
            IEnumerable<SortType> elementsGreaterThanPivot = listToSort.Where(element => element.CompareTo(listPivot) >= 0).Except(new List<SortType> { listPivot });

            IEnumerable<SortType> sortedElementsLessOrEqualThanPivot = SortDescending(elementsLessOrEqualThanPivot, sleep);
            List<SortType> sortedElementsGreaterThanPivot = SortDescending(elementsGreaterThanPivot, sleep).ToList();

            SortedList.RemoveAll(sortedItem => sortedElementsLessOrEqualThanPivot.Any(sortedSubItem => sortedSubItem.Equals(sortedItem)));
            SortedList.InsertRange(0, sortedElementsLessOrEqualThanPivot);
            SortedList.RemoveAll(sortedItem => sortedElementsGreaterThanPivot.Any(sortedSubItem => sortedSubItem.Equals(sortedItem)));
            SortedList.InsertRange(sortedElementsLessOrEqualThanPivot.Count(), sortedElementsGreaterThanPivot);

            Thread.Sleep(sleep);

            sortedElementsGreaterThanPivot.Add(listPivot);
            return sortedElementsGreaterThanPivot.Concat(sortedElementsLessOrEqualThanPivot);
        }

        private SortType GetPivot(IEnumerable<SortType> listToSort)
        {
            switch (quickSortPivotType)
            {
                case QuickSortPivotTypes.RandomPivot:
                    return listToSort.ElementAt(randomizer.Next(0, listToSort.Count() - 1));

                case QuickSortPivotTypes.LeftmostPivot:
                    return listToSort.First();

                case QuickSortPivotTypes.RightmostPivot:
                    return listToSort.Last();

                case QuickSortPivotTypes.NotDefined:
                    throw new InvalidOperationException($"The pivot value NotDefined is not valid as a QuickSortPivotType");
                default:
                    throw new NotImplementedException($"The pivot {quickSortPivotType.ToString()} has not been implemented");
            }
        }
    }
}