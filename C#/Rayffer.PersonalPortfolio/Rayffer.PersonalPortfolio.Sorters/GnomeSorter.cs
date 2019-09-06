using Rayffer.PersonalPortfolio.Sorters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Rayffer.PersonalPortfolio.Sorters
{
    public class GnomeSorter<SortType> : ISorter<SortType> where SortType : IComparable<SortType>
    {
        public List<SortType> SortedList { get; private set; }
        public int CurrentSortedListIndex { get; private set; }

        public IEnumerable<SortType> SortAscending(IEnumerable<SortType> listToSort, int sleep)
        {
            SortedList = listToSort.ToList();
            int swapOperations = SortedList.Count - 1;
            for (int sortIteration = 0; sortIteration < swapOperations; sortIteration++)
            {
                CurrentSortedListIndex = sortIteration;
                if (SortedList[sortIteration].CompareTo(SortedList[sortIteration + 1]) > 0)
                {
                    for (int sortIterationDescending = sortIteration; sortIterationDescending >= 0; sortIterationDescending--)
                    {
                        CurrentSortedListIndex = sortIterationDescending;
                        if (SortedList[sortIterationDescending].CompareTo(SortedList[sortIterationDescending + 1]) > 0)
                        {
                            SortType valueToSwapDescending = SortedList[sortIterationDescending + 1];
                            SortedList[sortIterationDescending + 1] = SortedList[sortIterationDescending];
                            SortedList[sortIterationDescending] = valueToSwapDescending;
                        }
                        else
                        {
                            break;
                        }
                        Thread.Sleep(sleep);
                    }
                }
            }

            return SortedList;
        }

        public IEnumerable<SortType> SortDescending(IEnumerable<SortType> listToSort, int sleep)
        {
            SortedList = listToSort.ToList();
            int swapOperations = SortedList.Count - 1;
            for (int sortIteration = 0; sortIteration < swapOperations; sortIteration++)
            {
                if (SortedList[sortIteration].CompareTo(SortedList[sortIteration + 1]) < 0)
                {

                    for (int sortIterationDescending = sortIteration; sortIterationDescending >= 0; sortIterationDescending--)
                    {
                        if (SortedList[sortIterationDescending].CompareTo(SortedList[sortIterationDescending + 1]) < 0)
                        {
                            SortType valueToSwapDescending = SortedList[sortIterationDescending + 1];
                            SortedList[sortIterationDescending + 1] = SortedList[sortIterationDescending];
                            SortedList[sortIterationDescending] = valueToSwapDescending;
                        }
                        else
                        {
                            break;
                        }
                        Thread.Sleep(sleep);
                    }
                }
            }

            return SortedList;
        }
    }
}