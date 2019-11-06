using System;
using System.Collections.Generic;
using System.Text;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Types
{
    public enum EventTriggerTypes
    {
        NotDefined = 0,
        FileChanged,
        NewFilesInDirectory,
        Interval,
        Scheduled
    }
}
