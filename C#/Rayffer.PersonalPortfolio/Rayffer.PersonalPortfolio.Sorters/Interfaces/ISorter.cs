using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayffer.PersonalPortfolio.Sorters.Interfaces
{
    public interface ISorter<T>
    {
        IEnumerable<T> SortAscending(IEnumerable<T> listToSort);
        IEnumerable<T> SortDescending(IEnumerable<T> listToSort);
    }
}
