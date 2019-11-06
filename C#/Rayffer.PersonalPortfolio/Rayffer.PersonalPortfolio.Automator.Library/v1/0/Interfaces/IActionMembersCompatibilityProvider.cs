using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Types;
using System.Collections.Generic;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces
{
    public interface IActionMembersCompatibilityProvider
    {
        IList<ActionDataTransformTypes> GetCompatibleDataTransformTypes(ActionDataProvidersTypes actionDataProvider);
        IList<ActionDataSenderTypes> GetCompatibleDataSenderTypes(ActionDataProvidersTypes actionDataProvider);
    }
}