using System;
using System.Collections.Generic;
using System.Text;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces
{
    public interface IObserverEventTrigger
    {
        event EventHandler TriggerEvent;

        void SubscribeAndStartEventTrigger();

        void StopAndUnsuscribeEventTrigger();
    }
}
