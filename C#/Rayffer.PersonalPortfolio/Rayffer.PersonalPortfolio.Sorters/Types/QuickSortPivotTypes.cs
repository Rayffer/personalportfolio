using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayffer.PersonalPortfolio.Sorters.Types
{
    public enum QuickSortPivotTypes
    {
        /// <summary>
        /// This element should never be asigned in any flow of the application, it is used to help in aiding when a default
        /// value has been returned either by an unexpected managed exception or any other conditions that prompt the return of a default DTO
        /// </summary>
        NotDefined = 0,
        RandomPivot,
        LeftmostPivot,
        RightmostPivot
    }
}
