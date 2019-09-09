using System.Collections.Generic;

namespace Rayffer.PersonalPortfolio.Sorters.Interfaces
{
    public interface ISorter<SortType>
    {
        List<SortType> SortedList { get; }
        int CurrentSortedListIndex { get; }

        IEnumerable<SortType> SortAscending(IEnumerable<SortType> listToSort, int sleep);

        IEnumerable<SortType> SortDescending(IEnumerable<SortType> listToSort, int sleep);
    }
}