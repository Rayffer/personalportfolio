using Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs;
using System.Collections.Generic;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces
{
    public interface ICheckFactory
    {
        IObserverCheck GetObserverCheck(CheckInformation checkInformation);

        IObserverCheckManager GetObserverChecksManager(IList<IObserverCheck> observerChecks);
    }
}