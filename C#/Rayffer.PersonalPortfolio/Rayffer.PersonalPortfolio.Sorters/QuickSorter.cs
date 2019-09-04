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
        private Random randomizer;
        private readonly QuickSortPivotTypes quickSortPivotType;

        public QuickSorter(QuickSortPivotTypes quickSortPivotType)
        {
            this.quickSortPivotType = quickSortPivotType;
            randomizer = new Random();
        }

        public IEnumerable<SortType> SortAscending(IEnumerable<SortType> listToSort, int sleep = 0)
        {
            Thread.Sleep(sleep);
            if (!listToSort.Any())
            {
                return listToSort;
            }

            SortType listPivot = GetPivot(listToSort);

            IEnumerable<SortType> elementsLessOrEqualThanPivot = listToSort.Where(element => element.CompareTo(listPivot) <= 0).Except(new List<SortType> { listPivot});
            IEnumerable<SortType> elementsGreaterThanPivot = listToSort.Where(element => element.CompareTo(listPivot) > 0);

            List<SortType> sortedElementsLessOrEqualThanPivot = SortAscending(elementsLessOrEqualThanPivot).ToList();
            IEnumerable<SortType> sortedElementsGreaterThanPivot = SortAscending(elementsGreaterThanPivot);

            sortedElementsLessOrEqualThanPivot.Add(listPivot);
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

            IEnumerable<SortType> sortedElementsLessOrEqualThanPivot = SortDescending(elementsLessOrEqualThanPivot);
            List<SortType> sortedElementsGreaterThanPivot = SortDescending(elementsGreaterThanPivot).ToList();

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