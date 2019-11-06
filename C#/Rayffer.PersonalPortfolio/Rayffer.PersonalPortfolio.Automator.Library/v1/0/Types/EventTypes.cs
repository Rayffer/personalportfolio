using System;
using System.Collections.Generic;
using System.Text;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Types
{
    public enum EventTypes
    {
        Notdefined = 0,
        None,
        FileDateTimeChanged,
        NewFilesInDirectory,
        SQLQueryResultChange,
        IniFileValueChanged,
        XmlFileValueChanged
    }
}
