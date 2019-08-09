using Rayffer.PersonalPortfolio.Sorters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rayffer.PersonalPortfolio.Sorters
{
    public class MergeSorter<SortType> : ISorter<SortType> where SortType : IComparable<SortType>
    {

        public IEnumerable<SortType> SortAscending(IEnumerable<SortType> listToSort)
        {
            var listItems = listToSort.Count();
            if (listItems == 1)
            {
                return listToSort;
            }

            int listPivot = (int)Math.Ceiling((double)listItems / 2);

            var firstHalfOfList = listToSort.Take(listPivot);
            var secondHalfOfList = listToSort.Skip(listPivot);

            var sortedFirstHalfOfList = SortAscending(firstHalfOfList);
            var sortedSecondHalfOfList = SortAscending(secondHalfOfList);

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

        public IEnumerable<SortType> SortDescending(IEnumerable<SortType> listToSort)
        {
            var listItems = listToSort.Count();
            if (listItems == 1)
            {
                return listToSort;
            }

            int listPivot = (int)Math.Ceiling((double)listItems / 2);

            var firstHalfOfList = listToSort.Take(listPivot);
            var secondHalfOfList = listToSort.Skip(listPivot);

            var sortedFirstHalfOfList = SortDescending(firstHalfOfList);
            var sortedSecondHalfOfList = SortDescending(secondHalfOfList);

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