using System;
using System.Collections.Generic;
using System.Text;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Types
{
    public enum CheckTypes
    {
        NotDefined = 0,
        None,
        FileDateTimeChanged,
        CheckHash,
        NewFilesInDirectory,
        SQLQueryResultChange,
        IniFileValueChanged,
        XmlFileValueChanged, 
        CheckHashDiferentFile,
    }
}