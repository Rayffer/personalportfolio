using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayffer.PersonalPortfolio.Sorters.Interfaces
{
    public interface ISorter<SortType>
    {
        IEnumerable<SortType> SortAscending(IEnumerable<SortType> listToSort, int sleep);
        IEnumerable<SortType> SortDescending(IEnumerable<SortType> listToSort, int sleep);
    }
}
