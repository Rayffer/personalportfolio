using Rayffer.PersonalPortfolio.Sorters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rayffer.PersonalPortfolio.Sorters
{
    public class SelectionSorter<SortType> : ISorter<SortType> where SortType : IComparable<SortType>
    {
        public List<SortType> SortedList { get; private set; }

        public int CurrentSortedListIndex { get; private set; }

        public IEnumerable<SortType> SortAscending(IEnumerable<SortType> listToSort, int sleep)
        {
            SortedList = listToSort.ToList();
            for (int sortIteration = 0; sortIteration < SortedList.Count; sortIteration++)
            {
                CurrentSortedListIndex = sortIteration;
                SortType minimumValue = SortedList.Skip(sortIteration).Min();
                int minimumValueIndex = SortedList.IndexOf(minimumValue);
                CurrentSortedListIndex = minimumValueIndex;
                SortType valueToSwap = SortedList[minimumValueIndex];
                SortedList[minimumValueIndex] = SortedList[sortIteration];
                SortedList[sortIteration] = valueToSwap;
                Thread.Sleep(sleep);
            }
            return SortedList;
        }

        public IEnumerable<SortType> SortDescending(IEnumerable<SortType> listToSort, int sleep)
        {
            SortedList = listToSort.ToList();
            for (int sortIteration = 0; sortIteration < SortedList.Count; sortIteration++)
            {
                SortType minimumValue = SortedList.Skip(sortIteration).Max();
                int minimumValueIndex = SortedList.IndexOf(minimumValue);
                SortType valueToSwap = SortedList[minimumValueIndex];
                SortedList[minimumValueIndex] = SortedList[sortIteration];
                SortedList[sortIteration] = valueToSwap;
                Thread.Sleep(sleep);
            }
            return SortedList;
        }
    }
}
