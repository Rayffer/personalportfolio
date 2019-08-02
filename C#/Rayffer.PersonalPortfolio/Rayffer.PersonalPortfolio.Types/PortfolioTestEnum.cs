using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayffer.PersonalPortfolio.Types
{
    public enum PortfolioTestEnum
    {
        // Include this enum value, which should not be used explicitly anywhere this enum is referenced
        // because this enum will be used to show that something did not go as expected and an operation
        // might have been aborted or ended unsuccesfully, thus creating the default value for this enum
        NotDefined = 0,
        MemberA, 
        MemberB,
        MemberC
    }
}
