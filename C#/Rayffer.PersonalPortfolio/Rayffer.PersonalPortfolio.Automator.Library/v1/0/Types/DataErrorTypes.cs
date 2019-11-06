using System;
using System.Collections.Generic;
using System.Text;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Types
{
    public enum DataErrorTypes
    {
        NotDefined = 0,
        ReferenceNull,
        EmptyString,
        FileDoesNotExist,
        DirectoryDoesNotExist,
        ValueCannotBeZero,
        IncompatibleActionMembers
    }
}
