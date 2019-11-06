using Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces
{
    public interface IEventFactory
    {
        IObserverEvent GetObserverEvent(EventInformation eventInformation);

    }
}
